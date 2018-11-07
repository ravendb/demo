const transitionTime = 350;

function getElementHeight(el: HTMLElement) {
    return el.clientHeight;
}

function getChildrenHeight(el: HTMLElement): number {
    const children = el.childNodes;
    if (!children) {
        return 0;
    }
    let height = 0;
    children.forEach(x => height += getElementHeight(x as HTMLElement));
    return height;
}

function afterTransition(callback: () => void) {
    setTimeout(callback, transitionTime);
}

export function expand(el: HTMLElement) {
    el.classList.add("collapsing", "show");
    const childrenHeight = getChildrenHeight(el);
    el.style.height = childrenHeight + "px";

    afterTransition(() => {
        el.classList.remove("collapsing");
        el.style.height = null;
    });
}

export function collapse(el: HTMLElement) {
    const height = getElementHeight(el);
    el.style.height = height + "px";
    el.classList.add("collapsing", "show");
    setTimeout(() => { el.style.height = "0"; }, 5);

    afterTransition(() => {
        el.classList.remove("show");
        el.classList.remove("collapsing");
        el.style.height = null;
    });
}