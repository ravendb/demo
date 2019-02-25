import { matchPath } from "react-router-dom";
import { LocationChangeAction } from "connected-react-router";
import { CategorySlug, DemoSlug } from "../models/slugs";
import { Language } from "../models/common";

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
    const wtPart = wtSlug ? `#${wtSlug}` : "";

    return language
        ? `/demos/${language}/${category}/${demo}${wtPart}`
        : `/demos/${category}/${demo}${wtPart}`;
}

export function createDemoWithoutWalkthroughPath(pathParams: DemoPathParams): string {
    const { category, demo, language } = pathParams;

    return language
        ? `/demos/${language}/${category}/${demo}#`
        : `/demos/${category}/${demo}#`;
}
