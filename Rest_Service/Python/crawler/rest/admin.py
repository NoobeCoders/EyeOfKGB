# -*- coding: utf-8 -*-
from django.contrib import admin
from .models import Keyword
from .models import Page
from .models import Person
from .models import PersonPageRank
from .models import Site

admin.site.register(Keyword)
admin.site.register(Page)
admin.site.register(Person)
admin.site.register(PersonPageRank)
admin.site.register(Site)
# Register your models here.
