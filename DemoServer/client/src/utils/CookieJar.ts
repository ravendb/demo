function setCookie(name, value, days) {
    var expires;
    if (days) {
        var date = new Date();
        date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
        expires = "; expires=" + date.toUTCString();
    } else {
        expires = "";
    }
    document.cookie = name + "=" + value + expires + "; path=/";
}

function readCookie(name : string) : string {
    var nameEq = name + "=";
    var ca = document.cookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) == ' ') c = c.substring(1, c.length);
        if (c.indexOf(nameEq) == 0) return c.substring(nameEq.length, c.length);
    }
    return null;
}

class CookieNames {
    static readonly userId = "userId";
}

export class CookieJar {
    private static readonly expireDays = 365;

    private static setCookieValue(cookieName: string, value: string) {
        setCookie(cookieName, value, this.expireDays);
    }

    private static readCookieBoolValue(cookieName: string): boolean {
        const storedCookieValue = readCookie(cookieName);
        return !!storedCookieValue && storedCookieValue === "true";
    }

    public static userId() {
        return readCookie(CookieNames.userId);
    }
}