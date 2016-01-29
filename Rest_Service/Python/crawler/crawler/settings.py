#-*- coding: utf-8 -*-
"""
Django settings for crawler project.

Generated by 'django-admin startproject' using Django 1.8.5.

For more information on this file, see
https://docs.djangoproject.com/en/1.8/topics/settings/

For the full list of settings and their values, see
https://docs.djangoproject.com/en/1.8/ref/settings/
"""

# Build paths inside the project like this: os.path.join(BASE_DIR, ...)
import os
SETTINGS_PATH = os.path.dirname(os.path.dirname(__file__))
BASE_DIR = os.path.dirname(os.path.dirname(os.path.abspath(__file__)))
PROJECT_ROOT = os.path.dirname(os.path.dirname(__file__))

# Quick-start development settings - unsuitable for production
# See https://docs.djangoproject.com/en/1.8/howto/deployment/checklist/

# SECURITY WARNING: keep the secret key used in production secret!
SECRET_KEY = 'pp6h+4+1@63cci=@o0fp3t0v^u1hr*_zxwtu2du!m&e8a2n^^y'

# SECURITY WARNING: don't run with debug turned on in production!
DEBUG = True

ALLOWED_HOSTS = ['crawler.firstexperience.ru']


# Application definition

INSTALLED_APPS = (
    'grappelli',
    'django.contrib.admin',
    'django.contrib.auth',
    'django.contrib.contenttypes',
    'django.contrib.sessions',
    'django.contrib.messages',
    'django.contrib.staticfiles',
    'compressor',
    'ckeditor',
    'rest_framework',
    'index',
    'rest',
)

MIDDLEWARE_CLASSES = (
    'django.contrib.sessions.middleware.SessionMiddleware',
    'django.middleware.common.CommonMiddleware',
    'django.middleware.csrf.CsrfViewMiddleware',
    'django.contrib.auth.middleware.AuthenticationMiddleware',
    'django.contrib.auth.middleware.SessionAuthenticationMiddleware',
    'django.contrib.messages.middleware.MessageMiddleware',
    'django.middleware.clickjacking.XFrameOptionsMiddleware',
    'django.middleware.security.SecurityMiddleware',
)

ROOT_URLCONF = 'crawler.urls'

TEMPLATES = [
    {
        'BACKEND': 'django.template.backends.django.DjangoTemplates',
        'DIRS': [
            os.path.join(BASE_DIR, 'templates'),
            ],
        'APP_DIRS': True,
        'OPTIONS': {
            'context_processors': [
                'django.template.context_processors.debug',
                'django.template.context_processors.request',
                'django.contrib.auth.context_processors.auth',
                'django.contrib.messages.context_processors.messages',
                "django.core.context_processors.request", #без него не работает админка  и для ck-editor
                 "django.core.context_processors.i18n",
                 "django.core.context_processors.media",
                 "django.core.context_processors.static",
                 "django.core.context_processors.tz",
                 'django.template.context_processors.csrf',
            ],
        },
    },
]

WSGI_APPLICATION = 'crawler.wsgi.application'


# Database
# https://docs.djangoproject.com/en/1.8/ref/settings/#databases

DATABASES = {
    'default': {
        'ENGINE': 'django.db.backends.mysql',
        'NAME': 'berluskuni0_crawler', # название базы
        'USER': '046368338_100',
        'PASSWORD': '0911142610',
        'HOST': '127.0.0.1',
        'PORT': '3306',
    }
}

# django-cache-mahine
# https://cache-machine.readthedocs.org/en/latest/
CACHES = {
    'default': {
        'BACKEND': 'caching.backends.memcached.MemcachedCache',
        'LOCATION': [
            'server-1:11211',
            'server-2:11211',
        ],
        'KEY_PREFIX': 'weee:',
        'TIMEOUT': None,
    },
}

CACHE_MACHINE_USE_REDIS = True
REDIS_BACKEND = 'redis://localhost:6379'

# end django-cache-mahine


# Internationalization
# https://docs.djangoproject.com/en/1.8/topics/i18n/

LANGUAGE_CODE = 'ru-ru'

TIME_ZONE = 'Europe/Moscow'

USE_I18N = True

USE_L10N = True

USE_TZ = True


# Static files (CSS, JavaScript, Images)
# https://docs.djangoproject.com/en/1.8/howto/static-files/
STATICFILES_FINDERS = (
    "django.contrib.staticfiles.finders.FileSystemFinder",
    "django.contrib.staticfiles.finders.AppDirectoriesFinder",
    'compressor.finders.CompressorFinder',
)

STATIC_URL = '/static/'

STATIC_ROOT = os.path.join(os.path.expanduser('~'), 'domains/crawler.firstexperience.ru/static/')

STATICFILES_DIRS = (
   os.path.join(PROJECT_ROOT, 'assets') ,
   os.path.join(PROJECT_ROOT, 'foundation'),
   os.path.join(PROJECT_ROOT, 'bootstrap'),
)


MEDIA_ROOT = os.path.join(os.path.expanduser('~'), 'domains/crawler.firstexperience.ru/static/media/uploads/')

MEDIA_URL = '/static/media/uploads/'




# Static files (CSS, JavaScript, Images)
# https://docs.djangoproject.com/en/1.8/howto/static-files/

STATIC_URL = '/static/'



# ckeditor
# https://pypi.python.org/pypi/django-ckeditor-updated
# TEMPLATE_CONTEXT_PROCESSORS

CKEDITOR_CONFIGS = {
'default': {
'toolbar': 'Full',
'height': 300,
'width': 300,
},
}
 
"""
CKEDITOR_CONFIGS = {
    'default': {
        'toolbar': 'Full',
        'height': 500,
        'width': '100%',
        'toolbarCanCollapse': False,
        'forcePasteAsPlainText': True
    }
}
"""
CKEDITOR_UPLOAD_PATH = "uploads/"
CKEDITOR_JQUERY_URL = '//ajax.googleapis.com/ajax/libs/jquery/2.1.1/jquery.min.js'
CKEDITOR_RESTRICT_BY_USER = True # поьзователь видит только свои файлы
CKEDITOR_IMAGE_BACKEND = "pillow"

# end ckeditor
REST_FRAMEWORK = {
    # Use Django's standard `django.contrib.auth` permissions,
    # or allow read-only access for unauthenticated users.
    'DEFAULT_RENDERER_CLASSES': (
        'rest.renderers.UTF8CharsetJSONRenderer',
    ),
}