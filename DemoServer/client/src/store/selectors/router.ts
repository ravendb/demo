import { createSelector } from "reselect";
import { RouterState } from "connected-react-router";

function getLocationPathName(state: RouterState): string {
    const { location } = state;
    return location.pathname;
}

export const selectIsOnMainPage = createSelector(
    [ getLocationPathName ],
    (pathname) => pathname === "/"
);
