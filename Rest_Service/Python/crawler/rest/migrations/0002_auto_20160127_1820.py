# -*- coding: utf-8 -*-
from __future__ import unicode_literals

from django.db import migrations, models


class Migration(migrations.Migration):

    dependencies = [
        ('rest', '0001_initial'),
    ]

    operations = [
        migrations.AlterField(
            model_name='keywords',
            name='keywords',
            field=models.CharField(max_length=50, verbose_name=b'\xd0\x9a\xd0\xbb\xd1\x8e\xd1\x87\xd0\xb5\xd0\xb2\xd0\xbe\xd0\xb5 \xd1\x81\xd0\xbb\xd0\xbe\xd0\xb2\xd0\xbe'),
        ),
    ]
