# -*- coding: utf-8 -*-
from __future__ import unicode_literals

from django.db import migrations, models


class Migration(migrations.Migration):

    dependencies = [
        ('rest', '0002_auto_20160127_1820'),
    ]

    operations = [
        migrations.CreateModel(
            name='Pages',
            fields=[
                ('id', models.AutoField(verbose_name='ID', serialize=False, auto_created=True, primary_key=True)),
                ('url', models.CharField(max_length=100, verbose_name=b'url - \xd1\x81\xd0\xb0\xd0\xb9\xd1\x82\xd0\xb0')),
                ('founddatetime', models.CharField(max_length=50, verbose_name=b'\xd0\xb2\xd1\x80\xd0\xb5\xd0\xbc\xd1\x8f \xd1\x81\xd0\xbe\xd0\xb7\xd0\xb4\xd0\xb0\xd0\xbd\xd0\xb8\xd1\x8f')),
                ('lastdatetime', models.CharField(max_length=50, verbose_name=b'\xd0\xb2\xd1\x80\xd0\xb5\xd0\xbc\xd1\x8f \xd0\xbf\xd0\xbe\xd1\x81\xd0\xbb\xd0\xb5\xd0\xb4\xd0\xbd\xd0\xb5\xd0\xb3\xd0\xbe \xd1\x81\xd0\xba\xd0\xb0\xd0\xbd\xd0\xb8\xd1\x80\xd0\xbe\xd0\xb2\xd0\xb0\xd0\xbd\xd0\xb8\xd1\x8f')),
                ('sites', models.ForeignKey(to='rest.Sites')),
            ],
            options={
                'db_table': 'db_pages_model',
            },
        ),
        migrations.CreateModel(
            name='PersonPageRank',
            fields=[
                ('id', models.AutoField(verbose_name='ID', serialize=False, auto_created=True, primary_key=True)),
                ('rank', models.IntegerField(default=0)),
                ('persons', models.ForeignKey(to='rest.Persons')),
                ('sites', models.ForeignKey(to='rest.Sites')),
            ],
            options={
                'db_table': 'db_personrank_model',
            },
        ),
    ]
