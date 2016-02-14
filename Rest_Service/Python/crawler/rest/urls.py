# -*- coding: utf-8 -*-
from django.conf.urls import url, include
from rest_framework.urlpatterns import format_suffix_patterns
from rest_framework.routers import DefaultRouter
from rest import views

router = DefaultRouter()
router.register(r'persons', views.PersonsViewSet)
router.register(r'sites', views.SitesViewSet)
router.register(r'keywords', views.KeywordsViewSet)
router.register(r'personrank', views.PersonPageRankViewSet, base_name='PersonPageRank')
router.register(r'rankeveryday', views.PersonRankEverydayViewSet, base_name='PersonRankEverydey')


urlpatterns = [
    url(r'^', include(router.urls)),
    url(r'^api-auth/', include('rest_framework.urls', namespace='rest_framework')),
    
]


