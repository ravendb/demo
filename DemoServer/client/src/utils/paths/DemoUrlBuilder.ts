import { Language } from "../../models/common";
import { CategorySlug, DemoSlug } from "../../models/slugs";
import { defaultLanguage } from "../../store/state/demo";

export class DemoUrlBuilder {
    private _language: Language = defaultLanguage;
    private _category: CategorySlug;
    private _demo: DemoSlug;
    private _wtSlug?: string;
    private _emptyHash: boolean = false;

    public static init(): DemoUrlBuilder {
        return new DemoUrlBuilder();
    }
    
    public withLanguage(language: Language): DemoUrlBuilder {
        this._language = language;
        return this;
    }

    public withCategory(category: CategorySlug): DemoUrlBuilder {
        this._category = category;
        return this;
    }

    public withDemo(demo: DemoSlug): DemoUrlBuilder {
        this._demo = demo;
        return this;
    }

    public withWtSlug(wtSlug: string): DemoUrlBuilder {
        this._wtSlug = wtSlug;
        return this;
    }

    public withEmptyHash(): DemoUrlBuilder {
        this._emptyHash = true;
        return this;
    }

    public build(): string {

        const hasDefaultLanguage = !this._language || this._language === defaultLanguage;

        const languagePart = hasDefaultLanguage
            ? ""
            : `/${this._language}`;

        const demoPath = `/${this._category}/${this._demo}`;

        const url = `/demos${languagePart}${demoPath}`;

        if (this._emptyHash) {
            return `${url}#`;
        }

        if (this._wtSlug) {
            return `${url}#${this._wtSlug}`;
        }

        return url;
    }
}
