#-*- coding: utf-8 -*-
from django.shortcuts import render_to_response, redirect
from django.core.context_processors import csrf
from django.contrib.auth.models import User
from django.contrib import auth
from index.models import Comments
from index.forms import FormComments

def index(request):
    args = {}
    args.update(csrf(request))
    args['username'] = auth.get_user(request).username
    args['comments'] = Comments.objects.all()
    if request.user.is_authenticated():
        comments_form = FormComments
        args = {}
        args.update(csrf(request))
        args['form'] = comments_form
        args['username'] = auth.get_user(request).username
        args['comments'] = Comments.objects.all()
        return render_to_response('index.html', args)
    else:
        return render_to_response('index.html', args)


def add_comment(request):
    if request.POST and ('pause' not in request.session):
        args = {}
        args['comments_text'] = request.POST.get('comment', '')
        form = FormComments(args)
        if form.is_valid():
            comment = form.save(commit=False)
            comment.name = auth.get_user(request).username
            comment.save()
            request.session.set_expiry(60)
            request.session['pause'] = True
    return redirect('/')
