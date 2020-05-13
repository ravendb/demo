import * as Cookies from "js-cookie";

class CookieNames {
    public static readonly consentGiven = "consent-given";
    public static readonly trackingDisabled = "tracking-disabled";
}

export type ConstentStatus = "given" | "withdrawn" | "no info";

export class CookieJar {
    private static readonly expireDays = 365;

    private static _setCookieValue(cookieName: string, value: string) {
        Cookies.set(cookieName, value, { expires: this.expireDays });
    }

    private static _readCookieValue(cookieName: string): string {
        return Cookies.get(cookieName);
    }

    private static _setCookieBoolValue(cookieName: string, value: boolean) {
        const textValue = !value ? "false" : "true";
        CookieJar._setCookieValue(cookieName, textValue);
    }

    private static _readCookieBoolValue(cookieName: string): boolean {
        const storedCookieValue = CookieJar._readCookieValue(cookieName);

        switch (storedCookieValue) {
            case "true":
                return true;
            case "false":
                return false;
            default:
                return null;
        }
    }
    
    public static isTrackingDisabledCookieSet(): boolean {
        const cookieValue = this._readCookieValue(CookieNames.trackingDisabled);
        return !!cookieValue;
    }

    public static acceptAll() {
        CookieJar._setCookieBoolValue(CookieNames.consentGiven, true);
        CookieJar._setCookieBoolValue(CookieNames.trackingDisabled, false);
    }

    public static acceptWithTrackingDisabled() {
        CookieJar._setCookieBoolValue(CookieNames.consentGiven, true);
        CookieJar._setCookieBoolValue(CookieNames.trackingDisabled, true);
    }

    public static consentGiven(): boolean {
        return CookieJar._readCookieBoolValue(CookieNames.consentGiven);
    }

    public static trackingDisabled(): boolean {
        return CookieJar._readCookieBoolValue(CookieNames.trackingDisabled);
    }

    public static getConsentStatus(): ConstentStatus {
        const consentGiven = CookieJar.consentGiven();
        const trackingDisabled = CookieJar.trackingDisabled();

        if (!consentGiven) {
            return "no info";
        }

        if (!trackingDisabled) {
            return "given";
        }

        return "withdrawn";
    }
}
