#-*- coding: utf-8 -*-
from django.conf.urls import url
from index.views import index, add_comment


urlpatterns = [
   
    url(r'^$', index , name='index'),
    url(r'^add_comment/$', add_comment, name='add_comment'),
   
    
]