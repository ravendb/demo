from demo.holder import DocumentStoreHolder
from demo.entities import *


class BoostingEnabled(object):
    @staticmethod
    def boosting_enabled(city="London", country="Denmark"):
        with DocumentStoreHolder.get_store().open_session() as session:
            orders = list(session.query(object_type=Order, index_name="OrderByCompanyAndCountryWithBoost",
                                        wait_for_non_stale_results=True,
                                        nested_object_types={"OrderedAt": datetime, "RequireAt": datetime,
                                                             "ShippedAt": datetime, "ShipTo": AddressC,
                                                             "Lines": OrderLine}).
                          where(ShipTo_City=city, ShipTo_Country=country))
						  
            return orders
