export function sliceIntoTwo(text: string, pivotLineNum: number): string[] {
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
    const secondResult = text.substr(sliceIndex + 2);

    return [firstResult, secondResult];
}