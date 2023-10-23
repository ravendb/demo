<?php

namespace RavenDB\Demo\spatial\spatialQuery;

//region Usings
use RavenDB\Documents\Indexes\Spatial\SpatialRelation;
use RavenDB\Documents\Indexes\Spatial\SpatialUnits;
use RavenDB\Documents\Queries\Spatial\PointField;
//endregion

use RavenDB\Demo\common\DocumentStoreHolder;
use RavenDB\Demo\common\models\Employee;

class SpatialQuery
{
    public function __invoke(RunParams $runParams): array
    {
        $radius = $runParams->getRadius() ?? 2;

        $queryResults = [];
        //region Demo
        $employeesWithinCircle = [];

        $session = DocumentStoreHolder::getStore()->openSession();
        try {
            $centerPointLng = -122.3150148;
            $centerPointLat = 47.63016419999999;

            //region Step_1
            $wktCircle = "CIRCLE(" . $centerPointLng . " " . $centerPointLat . " d=" . $radius . ")";
            //endregion

            //region Step_2
            $employeesWithinCircle = $session->query(Employee::class)
                ->spatial(
            //endregion
                    //region Step_3
                    new PointField("Address.Location.Latitude", "Address.Location.Longitude"),
                    //endregion

                    //region Step_4
                    function($spatialCriteria) use ($wktCircle) {
                        return $spatialCriteria->relatesToShape($wktCircle, SpatialRelation::within(), SpatialUnits::miles(), 0.025);
                    }
                    //endregion
                )
                //region Step_5
                ->orderByDistance(new PointField("Address.Location.Latitude", "Address.Location.Longitude"), $centerPointLat, $centerPointLng)
                //endregion

                //region Step_6
                ->toList();
                //endregion
                
        } finally {
            $session->close();
        }
        //endregion

        /** @var Employee $item */
        foreach ($employeesWithinCircle as $item) {
            $detailedItem = new EmployeeDetails();
            $detailedItem->setEmployeeName($item->getFirstName() . " " . $item->getLastName());
            $detailedItem->setLongitude($item->getAddress()->getLocation()->getLongitude());
            $detailedItem->setLatitude($item->getAddress()->getLocation()->getLatitude());

            $queryResults[] = $detailedItem;
        }

        return $queryResults;
    }
}

class EmployeeDetails
{
    private ?string $employeeName = null;
    private ?float $longitude = null;
    private ?float $latitude = null;

    public function getEmployeeName(): ?string
    {
        return $this->employeeName;
    }

    public function setEmployeeName(?string $employeeName): void
    {
        $this->employeeName = $employeeName;
    }

    public function getLongitude(): ?float
    {
        return $this->longitude;
    }

    public function setLongitude(?float $longitude): void
    {
        $this->longitude = $longitude;
    }

    public function getLatitude(): ?float
    {
        return $this->latitude;
    }

    public function setLatitude(?float $latitude): void
    {
        $this->latitude = $latitude;
    }
}
