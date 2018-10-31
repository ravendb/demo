import { matchPath } from 'react-router-dom';
import { LocationChangeAction } from 'connected-react-router';

export const demoPath = '/demos/:category/:demo';
export const demoWithWalkthroughPath = '/demos/:category/:demo/wt/:wtSlug';

interface DemoPathParams {
    category: string;
    demo: string;
    wtSlug?: string;
}

function getPathParams(action: LocationChangeAction, pattern: string): any {
    const path = action.payload.location.pathname;
    const match = path && matchPath(path, { path: pattern });
    return match && match.params;
}

function createMatcher<T>(pattern: string) {
    return (action: LocationChangeAction): T => getPathParams(action, pattern);
}

export const matchDemoPath = createMatcher<DemoPathParams>(demoPath);
export const matchDemoWithWalkthroughPath = createMatcher<DemoPathParams>(demoWithWalkthroughPath);