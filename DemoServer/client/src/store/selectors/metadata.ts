import { createSelector } from "reselect";
import { DemoState } from "../state/demo";
import { getTextExcerpt, getPlainTextFromMarkdown } from "../../utils/codeParsing";
import { DemoPathParams, createAbsoluteUrl, createCanonicalRelativeUrl } from "../../utils/paths";
import { getDemoImageSrc } from "./demos";

function getDemoImageSrcFromState({ categorySlug, demoSlug }: DemoState): string {
    const isNotDemoImage = !categorySlug || !demoSlug;

    const relativeSrc = isNotDemoImage
        ? "/img/logo.svg"
        : getDemoImageSrc(categorySlug, demoSlug);

    const absoluteUrl = createAbsoluteUrl(relativeSrc);
    return absoluteUrl;
}

function getHtmlMetaTitle({ demo }: DemoState): string {
    const title = demo && demo.title;
    return !!title ? `${title} | RavenDB Demo` : "RavenDB Demo";
}

function getHtmlMetaDescription({ demo }: DemoState): string {
    const maxLength: number = 160;

    const descriptionHtml = demo && demo.descriptionHtml;
    const plainText = getPlainTextFromMarkdown(descriptionHtml);
    return getTextExcerpt(plainText, maxLength);
}

function getHtmlMetaUrl(state: DemoState): string {
    const { language, categorySlug, demoSlug, wtSlug } = state;

    const pathParams: DemoPathParams = {
        category: categorySlug,
        demo: demoSlug,
        language,
        wtSlug
    };

    const relativeUrl = createCanonicalRelativeUrl(pathParams);
    return createAbsoluteUrl(relativeUrl);
}

export interface DemoHtmlMetadata {
    title: string;
    description: string;
    image: string;
    url: string;
}

export const selectHtmlMetadata = createSelector(
    [getHtmlMetaTitle, getHtmlMetaDescription, getDemoImageSrcFromState, getHtmlMetaUrl],
    (title, description, image, url): DemoHtmlMetadata => ({
        title,
        description,
        image,
        url
    })
);
