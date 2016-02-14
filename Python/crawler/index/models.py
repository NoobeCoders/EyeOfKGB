#-*- coding: utf-8 -*-
from django.db import models

# Create your models here.
class Comments(models.Model):
    class Meta:
        db_table = 'comments'
    name = models.CharField(max_length=50, verbose_name=u'Имя', blank=True)
    comments_text = models.TextField(verbose_name='Текст комментария')

    def __unicode__(self):
        return self.comments_text