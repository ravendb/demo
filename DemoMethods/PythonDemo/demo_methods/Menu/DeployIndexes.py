from demo.indexes import *
from demo.holder import DocumentStoreHolder


class DeployIndexes(object):
    @staticmethod
    def deploy_indexes():
        store = DocumentStoreHolder.get_store()
        ProductSales().execute(store)
        LastFmAnalyzed().execute(store)
        CompaniesAndCountry().execute(store)
        CostlyOrders().execute(store)
        NameAndCountry().execute(store)
        OrderByCompanyAndCountryWithBoost().execute(store)
        OrderByCompanyAndCountry().execute(store)
