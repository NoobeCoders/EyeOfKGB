# -*- coding: utf-8 -*-
from __future__ import unicode_literals

from django.db import migrations, models


class Migration(migrations.Migration):

    dependencies = [
        ('rest', '0003_pages_personpagerank'),
    ]

    operations = [
        migrations.AlterField(
            model_name='pages',
            name='founddatetime',
            field=models.CharField(max_length=50, verbose_name=b'\xd0\xb2\xd1\x80\xd0\xb5\xd0\xbc\xd1\x8f \xd1\x81\xd0\xbe\xd0\xb7\xd0\xb4\xd0\xb0\xd0\xbd\xd0\xb8\xd1\x8f', blank=True),
        ),
        migrations.AlterField(
            model_name='pages',
            name='lastdatetime',
            field=models.CharField(max_length=50, verbose_name=b'\xd0\xb2\xd1\x80\xd0\xb5\xd0\xbc\xd1\x8f \xd0\xbf\xd0\xbe\xd1\x81\xd0\xbb\xd0\xb5\xd0\xb4\xd0\xbd\xd0\xb5\xd0\xb3\xd0\xbe \xd1\x81\xd0\xba\xd0\xb0\xd0\xbd\xd0\xb8\xd1\x80\xd0\xbe\xd0\xb2\xd0\xb0\xd0\xbd\xd0\xb8\xd1\x8f', blank=True),
        ),
    ]
