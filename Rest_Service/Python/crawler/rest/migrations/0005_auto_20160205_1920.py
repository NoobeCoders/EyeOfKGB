# -*- coding: utf-8 -*-
from __future__ import unicode_literals

from django.db import migrations, models


class Migration(migrations.Migration):

    dependencies = [
        ('rest', '0004_auto_20160127_2030'),
    ]

    operations = [
        migrations.CreateModel(
            name='PersonRankEveryday',
            fields=[
                ('id', models.AutoField(verbose_name='ID', serialize=False, auto_created=True, primary_key=True)),
                ('rank_day', models.IntegerField(default=0)),
                ('data_scan', models.DateTimeField(verbose_name='\u0414\u0430\u0442\u0430 \u043f\u043e\u0441\u043b\u0435\u0434\u043d\u0435\u0433\u043e \u0441\u043a\u0430\u043d\u0438\u0440\u043e\u0432\u0430\u043d\u0438\u044f')),
            ],
            options={
                'db_table': 'db_personrank_everyday',
            },
        ),
        migrations.AlterField(
            model_name='keywords',
            name='data_join',
            field=models.DateTimeField(verbose_name='\u0414\u0430\u0442\u0430 \u0432\u043d\u0435\u0441\u0435\u043d\u0438\u044f \u0432 \u0431\u0430\u0437\u0443'),
        ),
        migrations.AlterField(
            model_name='keywords',
            name='keywords',
            field=models.CharField(max_length=50, verbose_name='\u041a\u043b\u044e\u0447\u0435\u0432\u043e\u0435 \u0441\u043b\u043e\u0432\u043e'),
        ),
        migrations.AlterField(
            model_name='pages',
            name='founddatetime',
            field=models.CharField(max_length=50, verbose_name='\u0432\u0440\u0435\u043c\u044f \u0441\u043e\u0437\u0434\u0430\u043d\u0438\u044f', blank=True),
        ),
        migrations.AlterField(
            model_name='pages',
            name='lastdatetime',
            field=models.CharField(max_length=50, verbose_name='\u0432\u0440\u0435\u043c\u044f \u043f\u043e\u0441\u043b\u0435\u0434\u043d\u0435\u0433\u043e \u0441\u043a\u0430\u043d\u0438\u0440\u043e\u0432\u0430\u043d\u0438\u044f', blank=True),
        ),
        migrations.AlterField(
            model_name='pages',
            name='url',
            field=models.CharField(max_length=100, verbose_name='url - \u0441\u0430\u0439\u0442\u0430'),
        ),
        migrations.AlterField(
            model_name='persons',
            name='data_join',
            field=models.DateTimeField(verbose_name='\u0414\u0430\u0442\u0430 \u0432\u043d\u0435\u0441\u0435\u043d\u0438\u044f \u0432 \u0431\u0430\u0437\u0443'),
        ),
        migrations.AlterField(
            model_name='persons',
            name='persons',
            field=models.CharField(unique=True, max_length=50, verbose_name='\u041b\u0438\u0447\u043d\u043e\u0441\u0442\u044c'),
        ),
        migrations.AlterField(
            model_name='sites',
            name='data_join',
            field=models.DateTimeField(verbose_name='\u0414\u0430\u0442\u0430 \u0432\u043d\u0435\u0441\u0435\u043d\u0438\u044f \u0432 \u0431\u0430\u0437\u0443'),
        ),
        migrations.AlterField(
            model_name='sites',
            name='sites',
            field=models.CharField(unique=True, max_length=50, verbose_name='\u0421\u0430\u0439\u0442'),
        ),
        migrations.AddField(
            model_name='personrankeveryday',
            name='persons',
            field=models.ForeignKey(to='rest.Persons'),
        ),
        migrations.AddField(
            model_name='personrankeveryday',
            name='sites',
            field=models.ForeignKey(to='rest.Sites'),
        ),
    ]
