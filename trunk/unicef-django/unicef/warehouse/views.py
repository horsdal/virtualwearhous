# Create your views here.

from django.contrib.auth import authenticate, login

from warehouse.models import *

from django.template.loader import get_template
from django.template import Context
from django.http import HttpResponse

from django.shortcuts import render_to_response


def index(req):
    q = req.GET.get('q', '')

    if q:
        drugs = Drug.objects.filter(name__icontains=q)
        return render_to_response('index.html', {'drugs': drugs})
    else:
        return render_to_response('index.html')

def login(request):
    username = request.POST['username']
    password = request.POST['password']
    user = authenticate(username=username, password=password)
    if user is not None:
        if user.is_active:
            login(request, user)
            return render_to_response('login.html')
            # Redirect to a success page.
        else:
            pass
            # Return a 'disabled account' error message
    else:
        pass
        # Return an 'invalid login' error message.
