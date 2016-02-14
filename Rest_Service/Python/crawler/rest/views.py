#-*- coding: utf-8 -*-
from django.contrib.auth.models import User, Group
from django.shortcuts import get_object_or_404, get_list_or_404
from rest_framework.response import Response
from rest_framework import viewsets
from rest.serializers import UserSerializer, GroupSerializer, PersonsSerializer, PersonPageRankSerializer, SitesSerializer, KeywordsSerializer, PersonPageRankSerializer, PersonRankEverydaySerializer 
from rest.models import Persons, Sites, PersonPageRank, Keywords, PersonRankEveryday
from django.http import Http404
from rest_framework.views import APIView
from rest_framework import status
from rest_framework import filters
import django_filters
from rest_framework.decorators import detail_route



class UserViewSet(viewsets.ModelViewSet):
    """
    API endpoint that allows users to be viewed or edited
    """
    queryset = User.objects.all().order_by('-date_joined')
    serializer_class = UserSerializer
    
    
class GroupViewSet(viewsets.ModelViewSet):
    """
    API endpoint that allows groups to be viewed or edited
    """
    queryset = Group.objects.all()
    serializer_class = GroupSerializer
    
    
class PersonsViewSet(viewsets.ModelViewSet):
    """
    API endpoint that allows persons to be viewedor edited
    """
    queryset = Persons.objects.all()
    serializer_class = PersonsSerializer
    
    
class SitesViewSet(viewsets.ModelViewSet):
    """
    API endpoint that allows sites to be viewedor edited
    """
    queryset = Sites.objects.all()
    serializer_class = SitesSerializer

class PersonPageRankViewSet(viewsets.ModelViewSet):
    
    
    """
    A simple ViewSet for Listing or retrieving persons.
    
    """
    def list(self, request):
        queryset = PersonPageRank.objects.all()
        serializer = PersonPageRankSerializer(queryset, many=True)
        if self.request.query_params.get("name"):
            persons = self.request.query_params.get("name")
            person_id = Persons.objects.filter(persons=persons).values('id')[0]['id']
            queryset = PersonPageRank.objects.filter(persons_id=person_id)
            serializer = PersonPageRankSerializer(queryset, many=True)
            return Response(serializer.data)
        else:
            return Response(serializer.data)
        
    def retrieve(self, request, pk=None):
        queryset = PersonPageRank.objects.filter(persons_id='person_id')
        serializer = PersonPageRankSerializer(queryset, many=True)
        return Response(serializer.data)
    
    
                
    
class CrawlerFilter(django_filters.FilterSet):
    dt_range = django_filters.MethodFilter(action='date_range_filter')
    class Meta:
        model = PersonRankEveryday
        fields = ('dt_range',)
        
    def date_range_filter(queryset, value):
        return queryset.filter(date__range[value.split(",")])
    

class PersonRankEverydayViewSet(viewsets.ModelViewSet):
    
    """
    A simple ViewSet for Listing or retrieving personrankeveryday.
    
    """
    def list(self, request):
        queryset = PersonRankEveryday.objects.all()
        serializer = PersonRankEverydaySerializer(queryset, many=True)
        if self.request.query_params.get("dt_persons_range"):
            pers_, min_, max_ = self.request.query_params.get("dt_persons_range").split(",")
            person_id = Persons.objects.filter(persons=pers_).values('id')[0]['id']
            queryset = PersonRankEveryday.objects.filter(persons_id=person_id).filter(data_scan__range=(min_, max_))
            serializer = PersonRankEverydaySerializer(queryset, many=True)
            return Response(serializer.data)
        else:
            return Response(serializer.data)
        
    def retrieve(self, request , pk=None):
        queryset = PersonRankEveryday.objects.filter(sites_id=pk)
        if self.request.query_params.get("dt_range"):
            min_, max_ = self.request.query_params.get("dt_range").split(",")
            queryset = queryset.filter(data_scan__range=(min_, max_))
        serializer = PersonRankEverydaySerializer(queryset, many=True)
        return Response(serializer.data)
    
    
    def create(self, request):
        if request.method == 'POST':
            serializer = PersonPageRankSerializer(data=request.data)
            if serializer.is_valid():
                data = serializer.save(commit=False)
                data.save()
                return Response({'status': 'OK'})
            else:
                return Response(serializer.errors,
                        status=status.HTTP_400_BAD_REQUEST)
    
    filter_backends = (filters.DjangoFilterBackend,)
    filter_class = CrawlerFilter
        
    
        

class KeywordsViewSet(viewsets.ModelViewSet):
    """
    API endpoint that allows keywords to be viewedor edited
    """
    queryset = Keywords.objects.all()
    serializer_class = KeywordsSerializer


    




    
