# -*- coding: utf-8 -*-
from unidecode import unidecode
from django.db import models


class Person(models.Model):
    name = models.CharField(max_length=100)

    class Meta:
        db_table = 'Person'

    def __unicode__(self):
        return unidecode(self.name)


class Site(models.Model):
    name = models.CharField(max_length=100)

    class Meta:
        db_table = 'Site'

    def __unicode__(self):
        return self.name


class Keyword(models.Model):
    name = models.CharField(max_length=200)
    person = models.ForeignKey(Person)

    class Meta:
        db_table = 'Keyword'

    def __unicode__(self):
        return self.name


class Page(models.Model):
    url = models.CharField(max_length=100)
    found_date_time = models.DateTimeField(auto_now_add=True)
    last_scan_date_time = models.DateTimeField(auto_now=True)
    site = models.ForeignKey(Site)

    class Meta:
        db_table = 'Page'
        ordering = ('found_date_time', )

    def __unicode__(self):
        return self.url


class PersonPageRank(models.Model):
    person = models.ForeignKey(Person)
    page = models.ForeignKey(Page)
    rank = models.IntegerField()

    class Meta:
        db_table = 'PersonPageRank'
        unique_together = (('person', 'page'), )

    def __unicode__(self):
        page_name = str(self.page)
        page = page_name + '...' if len(page_name) > 17 else page_name[:20]
        return str(self.person) + '-' + page + '-' + str(self.rank)
