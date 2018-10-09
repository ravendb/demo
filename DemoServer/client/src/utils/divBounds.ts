export interface DivBounds {
    offsetLeft: number;
    offsetRight: number;
    offsetTop: number;
    offsetBottom: number;
    height: number;
    width: number;
}

export function getBoundsRelatedToOtherElement(element: HTMLElement, relativeTo: HTMLElement): DivBounds {
    const elRect = element.getBoundingClientRect();
    const relativeRect = relativeTo.getBoundingClientRect();

    return {
        offsetLeft: Math.round(elRect.left - relativeRect.left),
        offsetRight: Math.round(relativeRect.right - elRect.right),
        offsetTop: Math.round(elRect.top - relativeRect.top),
        offsetBottom: Math.round(relativeRect.bottom - elRect.bottom),
        height: Math.round(elRect.height),
        width: Math.round(elRect.width)
    }
}