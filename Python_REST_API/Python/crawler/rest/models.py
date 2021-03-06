#-*- coding: utf-8 -*-
from unidecode import unidecode
from django.db import models


class Persons(models.Model):
    class Meta:
        db_table = 'db_Persons_model'
    """
    Таблица Личностей!
    
    """
    persons = models.CharField(max_length=50, verbose_name=u'Личность', unique=True)
    data_join = models.DateTimeField(verbose_name=u'Дата внесения в базу')
    
    def __unicode__(self):
        return unidecode(self.persons)
    
class Keywords(models.Model):
    class Meta:
        db_table = 'db_Keywords_Persons_model'
    """
    Таблица ключевых слов привязанных к личностям.
    """
    person_keywords = models.ForeignKey(Persons)
    keywords = models.CharField(max_length=50, verbose_name=u'Ключевое слово')
    data_join = models.DateTimeField(verbose_name=u'Дата внесения в базу')
    
    def __unicode__(self):
        return self.keywords
    
class Sites(models.Model):
    class Meta:
        db_table = 'db_sites_model'
    """
    Таблица сайтов.
    """
    sites = models.CharField(max_length=50, verbose_name=u'Сайт', unique=True)
    data_join = models.DateTimeField(verbose_name=u'Дата внесения в базу')
     
    def __unicode__(self):
        return self.sites
        

class Pages(models.Model):
    class Meta:
        db_table = 'db_pages_model'
    """
    Таблица соответствия названия сайтов и url
    """
    sites = models.ForeignKey(Sites)
    url = models.CharField(max_length=100, verbose_name=u'url - сайта')
    founddatetime = models.CharField(max_length=50, verbose_name=u'время создания', blank=True)
    lastdatetime = models.CharField(max_length=50, verbose_name=u'время последнего сканирования', blank=True)
    
    def __unicode__(self):
        return self.url
        
        
class PersonPageRank(models.Model):
    class Meta:
        db_table = 'db_personrank_model'
    """
    Рейтинг результатов поисков!
    """
    
    sites = models.ForeignKey(Sites)
    persons = models.ForeignKey(Persons)
    rank = models.IntegerField(default=0)


class PersonRankEveryday(models.Model):
    class Meta:
        db_table = 'db_personrank_everyday'
        
    """
    Рейтинг ежедневной статистики.
    """
    sites = models.ForeignKey(Sites)
    persons = models.ForeignKey(Persons)
    rank_day = models.IntegerField(default=0)
    data_scan = models.DateField(verbose_name=u'Дата последнего сканирования')

    


        
        
