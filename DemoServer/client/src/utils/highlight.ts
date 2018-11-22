import { DivBounds, getBoundsRelatedToOtherElement } from "./divBounds";

const Prism = window["Prism"] as any;

function getPixels(bounds: DivBounds, propSelect: (b: DivBounds) => number) {
    return propSelect(bounds).toString() + "px";
}

function populateBounds(bounds: DivBounds) {
    const highlightTop = document.getElementById("highlight-top");
    highlightTop.style.height = getPixels(bounds, b => b.offsetTop);
    highlightTop.style.left =  getPixels(bounds, b => b.offsetLeft);
    highlightTop.style.right =  getPixels(bounds, b => b.offsetRight);

    const highlightBottom = document.getElementById("highlight-bottom");
    highlightBottom.style.top = `${bounds.offsetTop + bounds.height}px`;
    highlightBottom.style.minHeight = getPixels(bounds, b => b.offsetBottom);

    const highlightLeft = document.getElementById("highlight-left");
    highlightLeft.style.height = `${bounds.offsetTop + bounds.height}px`;
    highlightLeft.style.width = getPixels(bounds, b => b.offsetLeft);

    const highlightRight = document.getElementById("highlight-right");
    highlightRight.style.height = `${bounds.offsetTop + bounds.height}px`;
    highlightRight.style.width = getPixels(bounds, b => b.offsetRight);
}

function exposeLineHighlights() {
    const codeBody = document.getElementById("demo-body-container");
    const lineHighlights = document.getElementsByClassName("line-highlight");

    if (lineHighlights && lineHighlights.length > 0) {
        const firstHighlight = lineHighlights[0];
        const relativeBounds = getBoundsRelatedToOtherElement(firstHighlight as HTMLElement, codeBody as HTMLElement);
        populateBounds(relativeBounds);
    }
}

function prismCompleteHook(env: any) {
    const parentElement = env && env.element && env.element.parentElement;
    const lineHighlights = parentElement && parentElement.getElementsByClassName("line-highlight");
    if (lineHighlights && lineHighlights.length > 0) {
        exposeLineHighlights();
    }
}

export function addHighlightHook() {
    Prism.hooks.add('complete', prismCompleteHook);
}

export function removeHighlightHook() {
    Prism.hooks.remove && Prism.hooks.remove('complete', prismCompleteHook);
}