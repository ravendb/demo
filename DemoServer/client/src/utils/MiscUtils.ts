import * as moment from "moment";

export function toTitleCase(s: string) {
    if (!s) {
        return s;
    }

    return s.replace(/\w\S*/g, function (txt) {
        return txt.charAt(0).toUpperCase() + txt.substr(1).toLowerCase();
    });
}

export const capitalizeFirstLetter = (s: string): string => {
    if (!s) {
        return s;
    }

    return s.charAt(0).toUpperCase() + s.slice(1);
};

function formatConvertedDate(converter, format = "YYYY-MM-DD HH:mm") {
    return date => formatMomentDate(converter(date), format);
}

export function formatUtcDate(date, format = "YYYY-MM-DD HH:mm") {
    return formatConvertedDate(fromUtcTime, format)(date);
}

const fromUtcTime = date => moment.utc(date).local();

function formatMomentDate(momentDate: moment.Moment, format = "YYYY-MM-DD HH:mm") {
    if (format === "short") {
        format = "YYYY-MM-DD";
    } 

    return momentDate.format(format);
}

export function getQueryString() {
    let a = window.location.search.substr(1).split('&');
    if ((a as any) === "") return {};
    var b = {};
    for (var i = 0; i < a.length; ++i) {
        var p = a[i].split('=', 2);
        if (p.length === 1) {
            b[p[0]] = "";
        } else {
            b[p[0]] = decodeURIComponent(p[1].replace(/\+/g, " "));
        }
    }

    return b;
}

export function validateEmail(email: string) {
    var regex = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    return regex.test(String(email).toLowerCase());
}

export function getEnumTextValues(e: any) {
    let values: string[] = [];
    for (var enumMember in e) {
        var isNumber = parseInt(enumMember, 10) >= 0;
        if (isNumber) {
            values.push(e[enumMember]);
        }
    }
    return values;
}

export function deepCopy<T>(obj: T): T {
    var copy = JSON.parse(JSON.stringify(obj));
    return copy as T;
}