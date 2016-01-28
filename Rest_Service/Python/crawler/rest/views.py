# -*- coding: utf-8 -*-
from django.contrib.auth.models import User
from rest.models import Keyword, Page
from rest.serializers import KeywordSerializer, PageSerializer
from rest_framework import generics, permissions


class KeywordList(generics.ListCreateAPIView):
    queryset = Keyword.objects.all()
    serializer_class = KeywordSerializer