function handleResize(callback: () => void) {
    window.requestAnimationFrame(callback);
}

export function addResizeListener(callback: () => void) {
    window.addEventListener("resize", function() { handleResize(callback); });
}

export function removeResizeListener(callback: () => void) {
    window.removeEventListener("resize", function() { handleResize(callback); });
}
