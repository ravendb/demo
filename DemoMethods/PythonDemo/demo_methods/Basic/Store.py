from demo.holder import DocumentStoreHolder
from demo.entities import Company


class Store(object):
    @staticmethod
    def store():
        with DocumentStoreHolder.get_store().open_session() as session:
            company = Company(Name="Hibernating Rhinos",
                              ExternalId="HR",
                              Phone="+972 4 622 7811",
                              Fax="+972 153 4 622 7811")

            session.store(company)
            session.save_changes()

            return "New Company Created Successfully"
