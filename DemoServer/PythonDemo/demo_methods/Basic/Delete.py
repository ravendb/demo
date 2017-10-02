from demo.holder import DocumentStoreHolder
from demo.entities import Company


class Delete(object):
    @staticmethod
    def delete(company_id=2):
        with DocumentStoreHolder.get_store().open_session() as session:
            session.delete(session.load("companies/" + company_id, object_type=Company))
            session.save_changes()
            return "CompanyId {0} Deleted  Successfully.".format(company_id)
