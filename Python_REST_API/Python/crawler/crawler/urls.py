#-*- coding: utf-8 -*-

from django.conf.urls import include, url, patterns
from django.contrib import admin
from rest_framework import routers
# from rest import views

# router = routers.DefaultRouter()
# router.register(r'users', views.UserViewSet)
# router.register(r'groups', views.GroupViewSet)
# router.register(r'/persons', views.PersonsViewSet)
# router.register(r'/sites', views.SitesViewSet)
# router.register(r'/PersonPageRank', views.PersonPageRankViewSet)


urlpatterns = [
    url(r'^grappelli/', include('grappelli.urls')), # grappelli URLS
    url(r'^admin/', include(admin.site.urls)),
    url(r'^', include('index.urls', namespace='index')),
    url(r'^api/v1/', include('rest.urls')),
    url(r'^auth/', include('loginsys.urls', namespace='login')),
]


# ckeditor
from django.conf.urls.static import static
from django.conf import settings

urlpatterns += patterns('',
                        url(r'^ckeditor', include('ckeditor.urls'))
       ) + static(settings.MEDIA_URL, document_root=settings.MEDIA_ROOT)
       
# end ckeditor

# rest_framework
