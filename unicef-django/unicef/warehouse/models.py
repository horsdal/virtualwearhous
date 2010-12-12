from django.db import models

class Country(models.Model):
    name = models.CharField(max_length=30)
    price_factor = models.IntegerField()

    def __str__(self):
        return self.name

class Contact(models.Model):
    address =  models.CharField(max_length=30)
    zip = models.IntegerField()
    country =  models.ForeignKey(Country)
    city = models.CharField(max_length=30)
    fax =  models.CharField(max_length=30)
    email = models.EmailField()
    website = models.URLField()
    phone = models.CharField(max_length=10)

    def __str__(self):
        return "%s - %s" % (self.address, self.zip)

class Consumer(models.Model):
    name = models.CharField(max_length=30)
    contact = models.ForeignKey(Contact)

    def __str__(self):
        return self.name

class Manufacturer(models.Model):
    name =  models.CharField(max_length=30)
    contact = models.ForeignKey(Contact)

    def __str__(self):
        return self.name

class Category(models.Model):
    name =  models.CharField(max_length=30)

    def __str__(self):
        return self.name

class Drug(models.Model):
    name = models.CharField(max_length=30)
    category = models.ForeignKey(Category)
    
    def __str__(self):
        return self.name

class Package(models.Model):
    manufacturer = models.ForeignKey(Manufacturer, related_name = "manufacturers")
    drug = models.ForeignKey(Drug, related_name = "packages")
    size = models.CharField(max_length=128)
    base_price = models.IntegerField()
    smallest_unit = models.IntegerField()
    licensed = models.BooleanField()

    def __str__(self):
        return "%s - %d" % (self.size, self.smallest_unit)
