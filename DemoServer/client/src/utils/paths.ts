import { matchPath } from "react-router-dom";
import { LocationChangeAction } from "connected-react-router";

export const demoPath = '/demos/:category/:demo';

export interface DemoPathParams {
    category: string;
    demo: string;
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
}

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
    }
}

export function createDemoWithWalkthroughPath(pathParams: DemoPathParams) {
    const { category, demo, wtSlug } = pathParams;
    const wtPart = wtSlug ? `#${wtSlug}` : "";
    return `/demos/${category}/${demo}${wtPart}`;
}