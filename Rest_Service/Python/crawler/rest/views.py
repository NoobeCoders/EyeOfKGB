#-*- coding: utf-8 -*-
from django.contrib.auth.models import User, Group
from django.shortcuts import get_object_or_404
from rest_framework.response import Response
from rest_framework import viewsets
from rest.serializers import UserSerializer, GroupSerializer, PersonsSerializer, PersonPageRankSerializer, SitesSerializer
from rest.models import Persons, Sites, PersonPageRank



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
    queryset = PersonPageRank.objects.all()
    serializer_class = PersonPageRankSerializer

"""
def PersonRank(request, pk):
    rank = PersonPageRank.objects.filter(sites_id=pk)
    return rank
"""
