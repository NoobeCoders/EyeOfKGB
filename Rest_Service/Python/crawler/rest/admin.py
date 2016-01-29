#-*- coding: utf-8 -*-
from django.contrib import admin
from rest.models import Persons, Keywords, Sites, Pages, PersonPageRank

class KeywordsAdmin(admin.ModelAdmin):
    fieldsets = [
        (None, {"fields": ['person_keywords', 'keywords']})
    ]
    list_display = ['person_keywords', 'keywords']
    ordering = ['person_keywords']
    
class PersonPageRankAdmin(admin.ModelAdmin):
    fieldsets = [
        (None, {"fields": ['sites','persons','rank']})
        ]
    list_display = ['persons', 'sites', 'rank']
    ordering = ['persons']
    
class PagesAdmin(admin.ModelAdmin):
    fieldsets = [
        (None, {"fields":['sites', 'url', 'founddatetime', 'lastdatetime']})
        ]
    list_display = ['sites', 'url', 'founddatetime', 'lastdatetime']
    ordering = ['sites']


admin.site.register(Persons)
admin.site.register(Keywords, KeywordsAdmin)
admin.site.register(Sites)
admin.site.register(Pages, PagesAdmin)
admin.site.register(PersonPageRank, PersonPageRankAdmin )

# Register your models here.
