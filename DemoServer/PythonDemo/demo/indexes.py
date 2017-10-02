from pyravendb.data.indexes import IndexDefinition, FieldIndexing, IndexFieldOptions, FieldTermVector
from pyravendb.raven_operations.admin_operations import PutIndexesOperation


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

        self.index_definition = IndexDefinition(name=ProductSales.__name__, maps=index_map, reduce=map_reduce)

    def execute(self, store):
        store.admin.send(PutIndexesOperation(self.index_definition))


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

        self.index_definition = IndexDefinition(name=LastFmAnalyzed.__name__, maps=index_map,
                                                fields={"Query": IndexFieldOptions(indexing=FieldIndexing.search)})

    def execute(self, store):
        store.admin.send(PutIndexesOperation(self.index_definition))


class CostlyOrders(object):
    def __init__(self):
        index_map = ("from order in docs.Orders "
                     "select new {"
                     "OrderId = order.__document_id,"
                     "Delay = order.ShippedAt - ((DateTime?)order.OrderedAt),"
                     "Price = Enumerable.Sum(order.Lines, x => "
                     "((decimal)((((decimal)x.Quantity) * x.PricePerUnit) * (1M - x.Discount))))}")

        self.index_definition = IndexDefinition(name=CostlyOrders.__name__, maps=index_map)

    def execute(self, store):
        store.admin.send(PutIndexesOperation(self.index_definition))


class NameAndCountry(object):
    class Result(object):
        def __init__(self, Name, Country, Id):
            self.Name = Name
            self.Id = Id
            self.Country = Country

    def __init__(self):
        maps = [(("from e in docs.Employees "
                  "select new {"
                  "Name = e.FirstName + \" \" + e.LastName,"
                  "Country = e.Address.Country}")),
                ("from c in docs.Companies "
                 "select new {"
                 "Name = c.Name,"
                 "Country = c.Address.Country}"),
                ("from s in docs.Suppliers "
                 "select new{"
                 "Name = s.Name,"
                 "Country = s.Address.Country}")]
        fields = {"Country": IndexFieldOptions(indexing=FieldIndexing.search, storage=True),
                  "Name": IndexFieldOptions(storage=True)}

        self.index_definition = IndexDefinition(name=NameAndCountry.__name__, maps=maps, fields=fields)

    def execute(self, store):
        store.admin.send(PutIndexesOperation(self.index_definition))


class OrderByCompanyAndCountryWithBoost(object):
    def __init__(self):
        index_map = ("from order in docs.Orders "
                     "select new {"
                     "ShipTo_City = order.ShipTo.City.Boost(10),"
                     "ShipTo_Country = order.ShipTo.Country}")

        self.index_definition = IndexDefinition(name=OrderByCompanyAndCountryWithBoost.__name__, maps=index_map)

    def execute(self, store):
        store.admin.send(PutIndexesOperation(self.index_definition))


class OrderByCompanyAndCountry(object):
    def __init__(self):
        index_map = ("from order in docs.Orders "
                     "select new {"
                     "ShipTo_City = order.ShipTo.City,"
                     "ShipTo_Country = order.ShipTo.Country}")

        self.index_definition = IndexDefinition(name=OrderByCompanyAndCountry.__name__, maps=index_map)

    def execute(self, store):
        store.admin.send(PutIndexesOperation(self.index_definition))


class CategoryDescription(object):
    def __init__(self):
        index_map = ("from category in docs.Categories "
                     "select new {"
                     "category.Description}")
        fields = {
            "Description": IndexFieldOptions(storage=True, analyzer="Lucene.Net.Analysis.Standard.StandardAnalyzer")}

        self.index_definition = IndexDefinition(name=CategoryDescription.__name__, maps=index_map, fields=fields)

    def execute(self, store):
        store.admin.send(PutIndexesOperation(self.index_definition))


class CompaniesAndAddresses(object):
    def __init__(self):
        index_map = ("from company in docs.Companies "
                     "select new {"
                     "Company = company,"
                     "Address = new[]{"
                     "company.Address.Line1",
                     "company.Address.Line2",
                     "company.Address.City",
                     "company.Address.Country}}")
        fields = {
            "Address": IndexFieldOptions(indexing=FieldIndexing.search, storage=True,
                                         term_vector=FieldTermVector.with_positions_and_offsets)}

        self.index_definition = IndexDefinition(name=CompaniesAndAddresses.__name__, maps=index_map, fields=fields)

    def execute(self, store):
        store.admin.send(PutIndexesOperation(self.index_definition))


class ProductsAndPriceAndSuplier(object):
    class Result(object):
        def __init__(self, ProductId, PricePerUnit, UnitsInStock):
            self.ProductId = ProductId
            self.PricePerUnit = PricePerUnit
            self.UnitsInStock = UnitsInStock

    def __init__(self):
        index_map = ("from product in docs.Products "
                     "select new {"
                     "ProductId = company,"
                     "PricePerUnit = product.PricePerUnit,"
                     "UnitsInStock = product.UnitsInStock")

        self.index_definition = IndexDefinition(name=ProductsAndPriceAndSuplier.__name__, maps=index_map)

    def execute(self, store):
        store.admin.send(PutIndexesOperation(self.index_definition))
