export type Language = "csharp" | "java" | "python";

export function languageToDisplay(language: Language) {
    switch (language) {
        case "csharp":
            return "C#";
        case "java":
            return "Java";
        case "python":
            return "Python";
        default:
            return "???";
    }
}
