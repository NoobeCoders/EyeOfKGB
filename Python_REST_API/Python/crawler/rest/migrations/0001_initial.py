# -*- coding: utf-8 -*-
from __future__ import unicode_literals

from django.db import migrations, models


class Migration(migrations.Migration):

    dependencies = [
    ]

    operations = [
        migrations.CreateModel(
            name='Keywords',
            fields=[
                ('id', models.AutoField(verbose_name='ID', serialize=False, auto_created=True, primary_key=True)),
                ('keywords', models.CharField(max_length=50, verbose_name=b'\xd0\x9b\xd0\xb8\xd1\x87\xd0\xbd\xd0\xbe\xd1\x81\xd1\x82\xd1\x8c')),
                ('data_join', models.DateTimeField(verbose_name=b'\xd0\x94\xd0\xb0\xd1\x82\xd0\xb0 \xd0\xb2\xd0\xbd\xd0\xb5\xd1\x81\xd0\xb5\xd0\xbd\xd0\xb8\xd1\x8f \xd0\xb2 \xd0\xb1\xd0\xb0\xd0\xb7\xd1\x83')),
            ],
            options={
                'db_table': 'db_Keywords_Persons_model',
            },
        ),
        migrations.CreateModel(
            name='Persons',
            fields=[
                ('id', models.AutoField(verbose_name='ID', serialize=False, auto_created=True, primary_key=True)),
                ('persons', models.CharField(unique=True, max_length=50, verbose_name=b'\xd0\x9b\xd0\xb8\xd1\x87\xd0\xbd\xd0\xbe\xd1\x81\xd1\x82\xd1\x8c')),
                ('data_join', models.DateTimeField(verbose_name=b'\xd0\x94\xd0\xb0\xd1\x82\xd0\xb0 \xd0\xb2\xd0\xbd\xd0\xb5\xd1\x81\xd0\xb5\xd0\xbd\xd0\xb8\xd1\x8f \xd0\xb2 \xd0\xb1\xd0\xb0\xd0\xb7\xd1\x83')),
            ],
            options={
                'db_table': 'db_Persons_model',
            },
        ),
        migrations.CreateModel(
            name='Sites',
            fields=[
                ('id', models.AutoField(verbose_name='ID', serialize=False, auto_created=True, primary_key=True)),
                ('sites', models.CharField(unique=True, max_length=50, verbose_name=b'\xd0\xa1\xd0\xb0\xd0\xb9\xd1\x82')),
                ('data_join', models.DateTimeField(verbose_name=b'\xd0\x94\xd0\xb0\xd1\x82\xd0\xb0 \xd0\xb2\xd0\xbd\xd0\xb5\xd1\x81\xd0\xb5\xd0\xbd\xd0\xb8\xd1\x8f \xd0\xb2 \xd0\xb1\xd0\xb0\xd0\xb7\xd1\x83')),
            ],
            options={
                'db_table': 'db_sites_model',
            },
        ),
        migrations.AddField(
            model_name='keywords',
            name='person_keywords',
            field=models.ForeignKey(to='rest.Persons'),
        ),
    ]
