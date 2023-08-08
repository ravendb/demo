from demo_example import Example, RunParamsBase
from models import Company

class RunParams(RunParamsBase):
    def __init__(self, company_name: str):
        self.company_name = company_name

class LoadAndEditDocument(Example):
    def run(self, run_params: RunParams) -> None:
        company_name = run_params.company_name

        #region Demo
        with self.document_store_holder.store().open_session() as session:
            #region Step_1
            company = session.load("companies/5-A", Company)
            #endregion

            #region Step_2
            company.Name = company_name
            #endregion

            #region Step_3
            session.save_changes()
            #endregion
        #endregion
