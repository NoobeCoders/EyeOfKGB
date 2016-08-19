#-*- coding: utf-8 -*-
from django.contrib import admin
from index.models import Comments

# Register your models here.
class CommentsAdmin(admin.ModelAdmin):
    fieldsets = [
        (None, {"fields":['name', 'comments_text']})
        ]
    list_display = ['name', 'comments_text']
    ordering = ['comments_text']
    

admin.site.register(Comments, CommentsAdmin)