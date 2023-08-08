from database import DocumentStoreHolder
from models import Company, Contact
from demo_example import Example, RunParamsBase

class RunParams(RunParamsBase):
    def __init__(
        self,
        company_name: str = None,
        company_phone: str = None,
        contact_name: str = None,
        contact_title: str = None,
    ):
        self.company_name = company_name
        self.company_phone = company_phone
        self.contact_name = contact_name
        self.contact_title = contact_title


class CreateDocument(Example):
    def run(self, run_params: RunParams) -> str:
        company_name = run_params.company_name or "Hibernating Rhinos"
        company_phone = run_params.company_phone or "(+972)52-5486969"
        contact_name = run_params.contact_name or "New Contact Name"
        contact_title = run_params.contact_title or "New Contact Title"

        #region Demo
        #region Step_1
        new_company = Company(
            Name = company_name,
            Phone = company_phone,
            Contact = Contact(contact_name, contact_title),
        )
        #endregion

        with DocumentStoreHolder.store().open_session() as session:
            #region Step_2
            session.store(new_company)
            #endregion

            #region Step_3
            the_new_document_id = new_company.Id
            #endregion

            #region Step_4
            session.save_changes()
            #endregion
        #endregion

        return the_new_document_id
