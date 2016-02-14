#-*- coding: utf-8 -*-
from unidecode import unidecode
from django.contrib.auth.models import User, Group
from rest_framework import serializers
from rest.models import Persons, Sites, PersonPageRank, Keywords, PersonRankEveryday

class UserSerializer(serializers.HyperlinkedModelSerializer):
    class Meta:
        model = User
        fields = ('url','username','email','groups')
        


class GroupSerializer(serializers.HyperlinkedModelSerializer):
    class Meta:
        model = Group
        fields = ('url', 'name')
        
        
class PersonsSerializer(serializers.HyperlinkedModelSerializer):
    class Meta:
        model = Persons
        fields = ('persons', 'id')
        
class SitesSerializer(serializers.HyperlinkedModelSerializer):
    class Meta:
        model = Sites
        fields = ('sites', 'id')
        
        
class PersonPageRankSerializer(serializers.HyperlinkedModelSerializer):
    site_name = serializers.SerializerMethodField('get_site')
    person_name = serializers.SerializerMethodField('get_person')
    
    def get_site(self, keyword):
        return str(keyword.sites)
        
    def get_person(self, keyword):
        return str(keyword.persons)
        
    class Meta:
        model = PersonPageRank
        fields = ('site_name', 'person_name', 'rank')


class PersonRankEverydaySerializer(serializers.HyperlinkedModelSerializer):
    site_name = serializers.SerializerMethodField('get_site')
    person_name = serializers.SerializerMethodField('get_person')
    
    def get_site(self, keyword):
        return str(keyword.sites)
        
    def get_person(self, keyword):
        return str(keyword.persons)
        
    class Meta:
        model = PersonRankEveryday
        fields = ('site_name', 'person_name', 'rank_day', 'data_scan')


    


class KeywordsSerializer(serializers.HyperlinkedModelSerializer):
    person_name = serializers.SerializerMethodField('get_person')
    
    def get_person(self, keyword):
        return str(keyword.person_keywords)
    
    class Meta:
        model = Keywords
        fields = ('id', 'keywords', 'person_name')

