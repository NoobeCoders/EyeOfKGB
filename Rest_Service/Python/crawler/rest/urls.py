from django.conf.urls import url, patterns
from rest import views

urlpatterns = [
    url(r'^(?P<pk>\d+)/$', views.PersonRank)
  
]