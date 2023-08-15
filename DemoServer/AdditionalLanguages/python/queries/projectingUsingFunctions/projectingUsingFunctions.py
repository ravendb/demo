
#region Demo
class EmployeeDetails:
    def __init__(self, full_name: str, title: str):
        self.full_name = full_name
        self.title = title
#endregion

def run(self, run_params=None):
    #region Demo
    with self.document_store_holder.store().open_session() as session:
        
        raw_query_string = (
            #region Step_1
            "declare function output(employee) {"
            "   var formatName  = function(employee) { return 'FullName: ' + employee.FirstName + ' ' + employee.LastName; };"
            "   var formatTitle = function(employee) { return 'Title: ' + employee.Title };"
            "   return { title : formatTitle(employee), full_name : formatName(employee) };"
            "}"
            #endregion
            #region Step_2
            "from Employees as employee select output(employee)"
            #endregion
        )

        #region Step_3
        projected_query_with_functions = session.advanced.raw_query(
            raw_query_string, EmployeeDetails
        )
        #endregion

        #region Step_4
        projected_results = list(projected_query_with_functions)
        #endregion
    #endregion

    return projected_results
