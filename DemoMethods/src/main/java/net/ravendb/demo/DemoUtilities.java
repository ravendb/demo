package net.ravendb.demo;

import net.ravendb.abstractions.data.FacetResult;
import net.ravendb.abstractions.data.FacetValue;
import net.ravendb.demo.indexes.FacetRangeCreation;

import java.util.ArrayList;
import java.util.List;
import java.util.Map;

public class DemoUtilities {
    private static String serverInfo;

    public static String getServerInfo() {
        return serverInfo;
    }

    public static void setServerInfo(String serverInfo) {
        DemoUtilities.serverInfo = serverInfo;
    }

    public static List<FacetRangeCreation.FacetsRangesResults> formatRangeResults(Map<String, FacetResult> results) {
        ArrayList<FacetRangeCreation.FacetsRangesResults> lst = new ArrayList<>();

        for (String key : results.keySet()) {
            FacetResult value = results.get(key);
            for (FacetValue facetValue : value.getValues()) {
                FacetRangeCreation.FacetsRangesResults result = new FacetRangeCreation.FacetsRangesResults();
                result.setKey(key);
                result.setValue(facetValue.getRange());
                result.setCount(facetValue.getCount());
                lst.add(result);
            }
        }

        return lst;
    }
}
