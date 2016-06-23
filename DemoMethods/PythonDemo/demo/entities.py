from datetime import datetime


class Company(object):
    def __init__(self, Name="", ExternalId="", Phone="", Fax="", Address=None, Contact=None):
        self.Name = Name
        self.ExternalId = ExternalId
        self.Phone = Phone
        self.Fax = Fax
        if not Address:
            Address = AddressC()
        self.Address = Address
        if not Contact:
            Contact = ContactC()
        self.Contact = Contact


class AddressC(object):
    def __init__(self, Line1="", Line2="", City="", Region="", PostalCode="", Country=""):
        self.Country = Country
        self.Line1 = Line1
        self.Line2F = Line2
        self.City = City
        self.Region = Region
        self.PostalCode = PostalCode
        self.Country = Country


class ContactC(object):
    def __init__(self, Name="", Title=""):
        self.Name = Name
        self.Title = Title


class LastFm(object):
    def __init__(self, Artist="", TrackId="", Title="", TimeStamp=None, Tags=None):
        self.Artist = Artist
        self.TrackId = TrackId
        self.Title = Title
        self.TimeStamp = TimeStamp if TimeStamp is not None else datetime.now()
        self.Tags = Tags if Tags is not None else []


class Order(object):
    def __init__(self, Id="", Company="", Employee="", OrderedAt=None, RequireAt=None, ShippedAt=None, ShipTO=None,
                 ShipVia="", Freight=0.0, Lines=None):
        self.Id = Id
        self.Company = Company
        self.Employee = Employee
        self.OrderedAt = OrderedAt if not None else datetime.now()
        self.RequireAt = RequireAt if not None else datetime.now()
        self.ShippedAt = ShippedAt if not None else datetime.now()
        self.ShipTo = ShipTO if not None else AddressC()
        self.ShipVia = ShipVia
        self.Freight = Freight
        self.Lines = Lines if not None else []


class OrderLine(object):
    def __init__(self, Product="", ProductName="", PricePerUnit=0.0, Quantity=0, Discount=0.0):
        self.Product = Product
        self.ProductName = ProductName
        self.PricePerUnit = PricePerUnit
        self.Quantity = Quantity
        self.Discount = Discount
