import * as React from "react";

interface LanguageSelectProps {
}

export function LanguageSelect(props: LanguageSelectProps) {
    return <div className="language-select">
        <button className="btn active">C#</button>
        <button className="btn">Python</button>
        <button className="btn">C#</button>
    </div>;
}