import * as reqwest from "reqwest";

export class ApiClient {
    private static async toPromise<T>(reqwestPromise: Reqwest.ReqwestPromise<any>): Promise<T> {
        return Promise.resolve(reqwestPromise);
    }

    static getApiUrl(suffix: string) {
        return window.location.origin + suffix;
    }

    static async postEmpty(url: string): Promise<any> {
        var req = reqwest({
            url: this.getApiUrl(url),
            method: 'post',
            contentType: 'application/json'
        });

        return this.toPromise(req);
    }

    static async post<TInput, TOutput>(url: string, data: TInput): Promise<TOutput> {
        var req = reqwest({
            url: this.getApiUrl(url),
            method: 'post',
            type: 'json',
            data: data
        });

        return this.toPromise<TOutput>(req);
    }

    static async get<TOutput>(url: string): Promise<TOutput> {
        var req = reqwest({
            url: this.getApiUrl(url),
            method: 'get',
            contentType: 'application/json'
        });

        return this.toPromise<TOutput>(req);
    }
}