from typing import List
from models import Company

#region Usings
from ravendb import QueryData
#endregion

#region Demo
class CompanyDetails:
    def __init__(self, company_name: str = None, city: str = None, country: str = None):
        self.company_name = company_name
        self.city = city
        self.country = country

#endregion

def run(self, run_params=None) -> List[CompanyDetails]:
    #region Demo
    with self.document_store_holder.store().open_session() as session:
        #region Step_1
        projected_query = session.query(object_type=Company)
        #endregion
        
        #region Step_2
        projected_query = projected_query.select_fields_query_data(
            CompanyDetails,
            QueryData(
                ["Name", "Address.City", "Address.Country"],
                ["company_name", "city", "country"],
            ),
        )
        #endregion

        #region Step_3
        projected_results = list(projected_query)
        #endregion
    #endregion
    return projected_results
