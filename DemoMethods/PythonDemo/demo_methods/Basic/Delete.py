from demo.holder import DocumentStoreHolder
from demo.entities import *
from pyravendb.data.document_convention import DocumentConvention


class Delete(object):
    @staticmethod
    def delete(company_id=2):
        with DocumentStoreHolder.get_store().open_session() as session:
            company_id = "{0}/{1}".format(DocumentConvention.default_transform_type_tag_name(Company.__name__),
                                          company_id)
            session.delete(company_id)
            session.save_changes()
            return "CompanyId {0} Deleted  Successfully.".format(company_id)
