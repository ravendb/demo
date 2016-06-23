from pyravendb.data.indexes import IndexDefinition
from pyravendb.data.indexes import FieldIndexing


class ProductSales(object):
    class Result(object):
        def __init__(self, Product, Count, Total):
            self.Product = Product
            self.Count = Count
            self.Total = Total

    def __init__(self):
        index_map = ("from order in docs.Orders "
                     "from line in order.Lines "
                     "select new{"
                     "Product = line.Product,"
                     "Count = 1,"
                     "Total = ((line.Quantity * line.PricePerUnit) * (1 - line.Discount))}"
                     )

        map_reduce = ("from result in results "
                      "group result by result.Product into g "
                      "select new{"
                      "Product = g.Key,"
                      "Count = g.Sum(x => x.Count),"
                      "Total = g.Sum(x => x.Total)}"
                      )

        self.index_definition = IndexDefinition(index_map=index_map, reduce=map_reduce)

    def execute(self, store):
        store.database_commands.put_index(ProductSales.__name__, self.index_definition, True)


class LastFmAnalyzed(object):
    class Result(object):
        def __init__(self, Query):
            self.Query = Query

    def __init__(self):
        index_map = ("from song in docs.LastFms "
                     "select new {"
                     "Query = new object[] {"
                     "song.Artist,"
                     "((object)song.TimeStamp),"
                     "song.Tags,"
                     "song.Title,"
                     "song.TrackId}}")

        indexes = {"Query": FieldIndexing.analyzed}

        self.index_definition = IndexDefinition(index_map=index_map, indexes=indexes)

    def execute(self, store):
        store.database_commands.put_index(LastFmAnalyzed.__name__, self.index_definition, True)


class CompaniesAndCountry(object):
    class Result(object):
        def __init__(self, Company, Address_Country):
            self.Company = Company
            self.Address_Country = Address_Country

    def __init__(self):
        index_map = ("from company in docs.Companies "
                     "select new {"
                     "Company = company,"
                     "Address_Country = company.Address.Country}")

        self.index_definition = IndexDefinition(index_map=index_map)

    def execute(self, store):
        store.database_commands.put_index(CompaniesAndCountry.__name__, self.index_definition, True)


class CostlyOrders(object):
    def __init__(self):
        index_map = ("from order in docs.Orders "
                     "select new {"
                     "OrderId = order.__document_id,"
                     "Delay = order.ShippedAt - ((DateTime?)order.OrderedAt),"
                     "Price = Enumerable.Sum(order.Lines, x => "
                     "((decimal)((((decimal)x.Quantity) * x.PricePerUnit) * (1M - x.Discount))))}")

        self.index_definition = IndexDefinition(index_map=index_map)

    def execute(self, store):
        store.database_commands.put_index(CostlyOrders.__name__, self.index_definition, True)


class NameAndCountry(object):
    class Result(object):
        def __init__(self, Name, Country, Id):
            self.Name = Name
            self.Country = Country
            self.Id = Id

    def __init__(self):
        index_map = ("from e in docs.Employees "
                     "select new {"
                     "Name = (e.FirstName + \" \") + e.LastName,"
                     "Country = e.Address.Country}",
                     "from c in docs.Companies "
                     "select new {"
                     "Name = c.Name,"
                     "Country = c.Address.Country}",
                     "from s in docs.Suppliers "
                     "select new{"
                     "Name = s.Name,"
                     "Country = s.Address.Country}"
                     )

        indexes = {"Country": FieldIndexing.analyzed}
        stores = {"Name": "Yes", "Country": "Yes"}

        self.index_definition = IndexDefinition(index_map=index_map, name=NameAndCountry.__name__, indexes=indexes,
                                                stores=stores)

    def execute(self, store):
        store.database_commands.put_index(NameAndCountry.__name__, self.index_definition, True)


class OrderByCompanyAndCountryWithBoost(object):
    def __init__(self):
        index_map = ("from order in docs.Orders "
                     "select new {"
                     "ShipTo_City = order.ShipTo.City.Boost(10),"
                     "ShipTo_Country = order.ShipTo.Country}")

        self.index_definition = IndexDefinition(index_map=index_map)

    def execute(self, store):
        store.database_commands.put_index(OrderByCompanyAndCountryWithBoost.__name__, self.index_definition, True)


class OrderByCompanyAndCountry(object):
    def __init__(self):
        index_map = ("from order in docs.Orders "
                     "select new {"
                     "ShipTo_City = order.ShipTo.City,"
                     "ShipTo_Country = order.ShipTo.Country}")

        self.index_definition = IndexDefinition(index_map=index_map)

    def execute(self, store):
        store.database_commands.put_index(OrderByCompanyAndCountry.__name__, self.index_definition, True)
