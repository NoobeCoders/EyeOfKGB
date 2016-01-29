#-*- coding: utf-8 -*-
from django.contrib.auth.models import User, Group
from rest_framework import serializers
from rest.models import Persons, Sites, PersonPageRank

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
    class Meta:
        model = PersonPageRank
        fields = ('sites', 'persons', 'rank','id')