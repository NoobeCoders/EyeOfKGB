#-*- coding: utf-8 -*-
import loginsys
__author__ = 'berluskuni'
from django.conf.urls import patterns, url
from loginsys import views

urlpatterns = patterns('',
                       url(r'^login/', views.login_in, name='login'),
                       url(r'^logout/', views.logout_out),
                       url(r'^register/', views.register, name='register'),
                       )
