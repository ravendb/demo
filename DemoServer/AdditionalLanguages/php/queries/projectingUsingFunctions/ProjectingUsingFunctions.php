<?php

namespace RavenDB\Demo\queries\projectingUsingFunctions;

//region Usings
use Symfony\Component\Serializer\Annotation\SerializedName;
//endregion

use RavenDB\Demo\common\DocumentStoreHolder;

//region Demo
class EmployeeDetails
{
    #[SerializedName("FullName")]
    public ?string $fullName = null;
    #[SerializedName("Title")]
    public ?string $title = null;
}
//endregion

class ProjectingUsingFunctions
{
    public function __invoke(): array
    {
        $projectedResults = [];
        //region Demo
        $session = DocumentStoreHolder::getStore()->openSession();
        
        try {
            $rawQueryString =
                //region Step_1
                "declare function output(employee) {" .
                "   var formatName  = function(employee) { return 'FullName: ' + employee.FirstName + ' ' + employee.LastName; };" .
                "   var formatTitle = function(employee) { return 'Title: ' + employee.Title };" .
                "   return { Title : formatTitle(employee), FullName : formatName(employee) };" .
                "}".
                //endregion
                //region Step_2
                "from Employees as employee select output(employee)";
                //endregion

            //region Step_3
            $projectedQueryWithFunctions = $session->advanced()
                ->rawQuery(EmployeeDetails::class, $rawQueryString);
            //endregion

            //region Step_4
            $projectedResults = $projectedQueryWithFunctions->toList();
            //endregion
        //endregion
        
        } finally {
            $session->close();
        }
        
        return $projectedResults;
    }
}
