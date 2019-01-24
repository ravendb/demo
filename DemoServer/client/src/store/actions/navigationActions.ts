import { push } from "connected-react-router";
import { createDemoWithWalkthroughPath } from "../../utils/paths";
import { DemoSlug, CategorySlug } from "../../models/slugs";

export const goToMainPage = () => push("/");

export function goToDemoPage(category: string, demo: string) {
    const url = createDemoWithWalkthroughPath({
        category: category as CategorySlug,
        demo: demo as DemoSlug
    });

    return push(url);
};

export function goToDemoAssetPage(url: string) {
    if (!url || !url.length) {
        return;
    }

    const effectiveUrl = url[0] === "/"
        ? `/demos${url}`
        : `/demos/${url}`;
        
    return push(effectiveUrl);
}