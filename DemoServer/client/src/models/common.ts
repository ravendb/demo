export type Language = "csharp" | "java" | "python" | "go";

export function languageToDisplay(language: Language) {
    switch (language) {
        case "csharp":
            return "C#";
        case "java":
            return "Java";
        case "python":
            return "Python";
        case "go":
            return "Go";
        default:
            return "???";
    }
}
