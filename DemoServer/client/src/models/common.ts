export type Language = "csharp" | "java" | "nodejs" | "python" | "go";

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
        case "nodejs":
            return "Node.js";
        default:
            return "???";
    }
}

export function toHighlightLanguage(language: Language): string {
    if (language === "nodejs") {
        return "javascript";
    }
    
    return language;
}
