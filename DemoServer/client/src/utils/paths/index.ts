import { matchPath } from "react-router-dom";
import { LocationChangeAction } from "connected-react-router";
import { CategorySlug, DemoSlug } from "../../models/slugs";
import { Language } from "../../models/common";
import { DemoUrlBuilder } from "./DemoUrlBuilder";

export const demoPath = "/demos/:language?/:category/:demo";

export interface DemoPathParams {
    language?: Language;
    category: CategorySlug;
    demo: DemoSlug;
    wtSlug?: string;
}

function getPathParams(action: LocationChangeAction, pattern: string): any {
    const path = action.payload.location.pathname;
    const match = path && matchPath(path, { path: pattern });
    return match && match.params;
}

const getHash = (action: LocationChangeAction): string => {
    const hash = action.payload.location.hash;
    return hash && hash.substr(1);
};

function createMatcher<T>(pattern: string) {
    return (action: LocationChangeAction): T => getPathParams(action, pattern);
}

export const matchDemoPath = createMatcher<DemoPathParams>(demoPath);

export const matchDemoWithWalkthroughPath = (action: LocationChangeAction): DemoPathParams => {
    const urlMatch = matchDemoPath(action);
    const hash = getHash(action);

    return urlMatch && {
        ...urlMatch,
        wtSlug: hash
    };
};

export function createDemoWithWalkthroughPath(pathParams: DemoPathParams): string {
    const { category, demo, wtSlug, language } = pathParams;

    const url = DemoUrlBuilder.init()
        .withLanguage(language)
        .withCategory(category)
        .withDemo(demo)
        .withWtSlug(wtSlug)
        .build();

    return url;
}

export function createDemoWithoutWalkthroughPath(pathParams: DemoPathParams): string {
    const { category, demo, language } = pathParams;

    const url = DemoUrlBuilder.init()
        .withLanguage(language)
        .withCategory(category)
        .withDemo(demo)
        .withEmptyHash()
        .build();

    return url;
}
