import * as markdowndeep from "markdowndeep";

const markdown = new markdowndeep.Markdown();
markdown.ExtraMode = true;
markdown.SafeMode = false;

export function getHtmlFromMarkdown(markdownContent: string): string {
    if (!markdownContent) {
        return markdownContent;
    }
    
    return markdown.Transform(markdownContent);
}

export function sliceIntoTwo(text: string, pivotLineNum: number): string[] {
    if (pivotLineNum === 0) {
        return ["", text];
    }

    const newLineRegex = /\r?\n/gm;

    let i = 1;
    let sliceIndex = 0;
    let match = null;

    while (match = newLineRegex.exec(text)) {
        if (i === pivotLineNum) {
            sliceIndex = match.index;
            break;
        }
        i++;
    }

    const firstResult = text.substr(0, sliceIndex);
    const secondResult = text.substr(sliceIndex + 1).trimLeft();

    return [firstResult, secondResult];
}

export function getPlainTextFromHtml(htmlContent: string): string {
    if (!htmlContent) {
        return htmlContent;
    }

    const doc = new DOMParser().parseFromString(htmlContent, "text/html");
    return doc.body.textContent || "";
}

export function getPlainTextFromMarkdown(markdownContent: string): string {
    const html = getHtmlFromMarkdown(markdownContent);
    const plainText = getPlainTextFromHtml(html);

    return plainText;
}

export function getTextExcerpt(text: string, maxLength: number): string {
    if (!text || text.length <= maxLength) {
        return text;
    }

    const ellipsis: string = "...";
    const shortenedTextLength: number = maxLength - ellipsis.length;
    const shortenedText = text.substring(0, shortenedTextLength);

    return `${shortenedText}${ellipsis}`;
}
