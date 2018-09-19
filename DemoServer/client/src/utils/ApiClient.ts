import * as reqwest from "reqwest";
import { RoutePaths } from "./RoutePaths";

export class ApiClient {

    private static isUnauthorized(error: XMLHttpRequest) {
        return error && error.status === 401;
    }

    private static goToLoginPage() {
        document.location.href = RoutePaths.LogIn;
    }

    private static toPromise<T>(reqwestPromise: Reqwest.ReqwestPromise<any>) : Promise<T> {
        return Promise.resolve(reqwestPromise).catch((error) => {
            console.log(error);
            if (this.isUnauthorized(error)) {
                this.goToLoginPage();
            } else {
                throw error;
            }
        });
    }

    static getApiUrl(suffix: string) {
        return window.location.origin + suffix;
    }

    static postEmpty(url: string) : Promise<any> {
        var req = reqwest({
            url: this.getApiUrl(url),
            method: 'post',
            contentType: 'application/json'
        });

        return this.toPromise(req);
    }

    static post<TInput, TOutput>(url: string, data: TInput) : Promise<TOutput> {

        var req = reqwest({
            url: this.getApiUrl(url),
            method: 'post',
            type: 'json',
            data: data
        });

        return this.toPromise(req);
    }

    static get<T>(url: string) : Promise<T> {
        
        var req = reqwest({
            url: this.getApiUrl(url),
            method: 'get',
            contentType: 'application/json'
        });

        return this.toPromise(req);
    }
}