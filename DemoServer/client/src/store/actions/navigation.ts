import { push } from "connected-react-router";
import * as actionTypes from "./actionTypes";
import { createDemoWithWalkthroughPath } from "../../utils/paths";
import { DemoSlug, CategorySlug } from "../../models/slugs";
import { DemoThunkAction } from ".";
import { DemoThunkDispatch } from "..";

interface WentToMainPage {
    type: actionTypes.NAVIGATION_WENT_TO_MAIN_PAGE;
}

interface WentToDemoPage {
    type: actionTypes.NAVIGATION_WENT_TO_DEMO_PAGE;
    category: string;
    demo: string;
}

export type NavigationAction = WentToMainPage | WentToDemoPage;

const wentToMainPage = (): WentToMainPage => ({
    type: "NAVIGATION_WENT_TO_MAIN_PAGE"
});

const wentToDemoPage = (category: CategorySlug, demo: DemoSlug): WentToDemoPage => ({
    type: "NAVIGATION_WENT_TO_DEMO_PAGE",
    category,
    demo
});

export function goToMainPage(): DemoThunkAction {
    return (dispatch: DemoThunkDispatch) => {
        dispatch(push("/"));
        dispatch(wentToMainPage());
    };
}

export function goToDemoPage(category: CategorySlug, demo: DemoSlug): DemoThunkAction {
    return (dispatch: DemoThunkDispatch) => {
        const url = createDemoWithWalkthroughPath({
            category,
            demo
        });
    
        dispatch(push(url));
        dispatch(wentToDemoPage(category, demo));
    };
}
