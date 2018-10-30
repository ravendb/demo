import { matchPath } from 'react-router-dom';
import { LocationChangeAction } from 'connected-react-router';

export const demoPath = '/demos/:category/:demo';

interface DemoPathParams {
    category: string;
    demo: string;
}

function getPathParams(action: LocationChangeAction, pattern: string): any {
    const path = action.payload.location.pathname;
    const match = path && matchPath(path, { path: pattern });
    return match && match.params;
}

export function matchDemoPath(action: LocationChangeAction): DemoPathParams {
    const params = getPathParams(action, demoPath);
    return {
        category: params && params.category,
        demo: params && params.demo
    };
};