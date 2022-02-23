import * as Cookies from "js-cookie";

class CookieNames {
    public static readonly consentGiven = "consent-given";
    public static readonly trackingDisabled = "tracking-disabled";
}

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

    public static isTrackingDisabledCookieDefined(): boolean {
        const cookieValue = this._readCookieValue(CookieNames.trackingDisabled);
        return !!cookieValue;
    }
    
    public static isTrackingDisabled(): boolean {
        const cookieValue = this._readCookieValue(CookieNames.trackingDisabled);
        return cookieValue === "true";
    }

    public static acceptAll() {
        CookieJar._setCookieBoolValue(CookieNames.consentGiven, true);
        CookieJar._setCookieBoolValue(CookieNames.trackingDisabled, false);
    }

    public static acceptWithTrackingDisabled() {
        CookieJar._setCookieBoolValue(CookieNames.consentGiven, true);
        CookieJar._setCookieBoolValue(CookieNames.trackingDisabled, true);
    }

    public static trackingDisabled(): boolean {
        return CookieJar._readCookieBoolValue(CookieNames.trackingDisabled);
    }
}
