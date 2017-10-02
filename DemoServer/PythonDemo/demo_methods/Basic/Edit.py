from demo.holder import DocumentStoreHolder
from demo.entities import *


class Edit(object):
    @staticmethod
    def edit(company_id=1):
        with DocumentStoreHolder.get_store().open_session() as session:
            company = session.load("companies/" + company_id, object_type=Company)

            company.Address.Line2 = "Zip 12345"
            session.save_changes()

            return "CompanyId {0} Edited Successfully.".format(company_id)
