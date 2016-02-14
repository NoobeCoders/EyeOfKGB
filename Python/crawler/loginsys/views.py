#-*- coding: utf-8 -*-
from django.contrib.auth import authenticate, login, logout
from django.contrib import auth
from django.core.context_processors import csrf
from django.shortcuts import redirect, render_to_response
from django.contrib.auth.forms import UserCreationForm
from django.contrib.auth.models import User


def login_in(request):
    args = {}
    args.update(csrf(request))
    if request.POST:
        username = request.POST.get('username', '')
        password = request.POST.get('password', '')
        user = authenticate(username=username, password=password)
        if user is not None:
            if user.is_active:
                login(request, user)
                return redirect('/')
        else:
            args['login_error'] = 'Пользователь не найден!'
            return render_to_response('login.html', args)
    else:
        return render_to_response('login.html', args)


def logout_out(request):
    logout(request)
    return redirect('/')


def register(request):
    args = {}
    args.update(csrf(request))
    args['form'] = UserCreationForm()
    args['username'] = auth.get_user(request).username
    if request.POST:
        newuser_form = UserCreationForm(request.POST)
        if newuser_form.is_valid():
            newuser_form.save()
            newuser = authenticate(username=newuser_form.cleaned_data['username'],
                                   password=newuser_form.cleaned_data['password1'])
            login(request, newuser)
            return redirect('/')
        else:
            args['form'] = newuser_form
            args['username'] = auth.get_user(request).username
    return render_to_response('register.html', args)
