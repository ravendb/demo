export interface DivBounds {
    offsetLeft: number;
    offsetRight: number;
    offsetTop: number;
    offsetBottom: number;
}

export function getBoundsRelatedToOtherElement(element: HTMLElement, relativeTo: HTMLElement): DivBounds {
    const elRect = element.getBoundingClientRect();
    const relativeRect = relativeTo.getBoundingClientRect();

    return {
        offsetLeft: elRect.left - relativeRect.left,
        offsetRight: relativeRect.right - elRect.right,
        offsetTop: elRect.top - relativeRect.top,
        offsetBottom: relativeRect.bottom - elRect.bottom
    }
}