from django import forms
from index.models import Comments

class FormComments(forms.ModelForm):
    class Meta:
        model = Comments
        fields = ['comments_text']