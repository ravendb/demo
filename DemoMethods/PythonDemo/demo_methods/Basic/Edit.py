from demo.holder import DocumentStoreHolder
from demo.entities import *
from pyravendb.data.document_convention import DocumentConvention


class Edit(object):
    @staticmethod
    def edit(company_id=1):
        with DocumentStoreHolder.get_store().open_session() as session:
            company_id = "{0}/{1}".format(DocumentConvention.default_transform_type_tag_name(Company.__name__), company_id)

            company = session.load(company_id, object_type=Company,
                                   nested_object_types={"Address": AddressC, "Contact": ContactC})

            company.Address.Line2 = "Zip 12345"
            session.save_changes()
            
            return "CompanyId {0} Edited Successfully.".format(company_id)
