# -*- coding: utf-8 -*-
from django.contrib.auth.models import User
from rest_framework import serializers
from rest.models import Keyword, Page, Person, PersonPageRank, Site


class KeywordSerializer(serializers.ModelSerializer):
    class Meta:
        model = Keyword
        fields = ('id', 'name', 'person')


class PageSerializer(serializers.ModelSerializer):
    class Meta:
        model = Page
        fields = ('id', 'url', 'found_date_time', 'last_scan_date_time', 'site')
