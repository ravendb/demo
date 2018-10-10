import { DivBounds, getBoundsRelatedToOtherElement } from "./divBounds";

const Prism = window["Prism"] as any;

function getPixels(bounds: DivBounds, propSelect: (b: DivBounds) => number) {
    return propSelect(bounds).toString() + "px";
}

function populateBounds(bounds: DivBounds) {
    const highlightTop = document.getElementById("highlight-top");
    highlightTop.style.height = getPixels(bounds, b => b.offsetTop);

    const highlightBottom = document.getElementById("highlight-bottom");
    highlightBottom.style.height = getPixels(bounds, b => b.offsetBottom);

    const highlightLeft = document.getElementById("highlight-left");
    highlightLeft.style.top = getPixels(bounds, b => b.offsetTop);
    highlightLeft.style.height = getPixels(bounds, b => b.height);
    highlightLeft.style.width = getPixels(bounds, b => b.offsetLeft);

    const highlightRight = document.getElementById("highlight-right");
    highlightRight.style.top = getPixels(bounds, b => b.offsetTop);
    highlightRight.style.height = getPixels(bounds, b => b.height);
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
    Prism.hooks.remove('complete', prismCompleteHook);
}