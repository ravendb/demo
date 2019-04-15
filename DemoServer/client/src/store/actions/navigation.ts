import { push } from "connected-react-router";
import * as actionTypes from "./actionTypes";
import { createDemoWithWalkthroughPath } from "../../utils/paths";
import { DemoSlug, CategorySlug } from "../../models/slugs";
import { DemoThunkAction } from ".";
import { DemoThunkDispatch } from "..";
import { Language } from "../../models/common";
import { trackPageView } from "./tracking";

interface WentToMainPage {
    type: actionTypes.NAVIGATION_WENT_TO_MAIN_PAGE;
}

interface WentToDemoPage {
    type: actionTypes.NAVIGATION_WENT_TO_DEMO_PAGE;
    category: string;
    demo: string;
    language?: Language;
}

export type NavigationAction = WentToMainPage | WentToDemoPage;

const wentToMainPage = (): WentToMainPage => ({
    type: "NAVIGATION_WENT_TO_MAIN_PAGE"
});

const wentToDemoPage = (category: CategorySlug, demo: DemoSlug, language?: Language): WentToDemoPage => ({
    type: "NAVIGATION_WENT_TO_DEMO_PAGE",
    language,
    category,
    demo
});

export function goToMainPage(): DemoThunkAction {
    return (dispatch: DemoThunkDispatch) => {
        const url = "/";

        dispatch(push(url));
        dispatch(wentToMainPage());
        dispatch(trackPageView(url));
    };
}

export function goToDemoPage(category: CategorySlug, demo: DemoSlug, language?: Language): DemoThunkAction {
    return (dispatch: DemoThunkDispatch) => {

        const url = createDemoWithWalkthroughPath({
            category,
            demo,
            language
        });

        dispatch(push(url));
        dispatch(wentToDemoPage(category, demo, language));
        dispatch(trackPageView(url));
    };
}

export function changeDemoLanguage(language: Language): DemoThunkAction {
    return (dispatch: DemoThunkDispatch, getState) => {
        const { demos } = getState();
        const { categorySlug, demoSlug } = demos;

        dispatch(goToDemoPage(categorySlug, demoSlug, language));
    };
}
