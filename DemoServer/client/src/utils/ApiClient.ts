import * as reqwest from "reqwest";
import { DemoStorage } from "./localStorage/DemoStorage";

const userIdHeader = "DemoUser-ID";

interface RequestOptions {
    method?: string;
    headers?: Object;
    data?: string | Object;
    type?: string;
    contentType?: string;
    crossOrigin?: boolean;
    success?: (response: any) => void;
    error?: (error: any) => void;
    complete?: (response: any) => void;
    jsonpCallback?: string;
}

export class ApiClient {
    private static async toPromise<T>(reqwestPromise: Reqwest.ReqwestPromise<any>): Promise<T> {
        return Promise.resolve(reqwestPromise);
    }

    private static prepareRequest(options: RequestOptions, relativeUrl: string): Reqwest.ReqwestPromise<any> {
        const url = this.getApiUrl(relativeUrl);
        const headers = {
            [userIdHeader]: DemoStorage.getUserId()
        };

        let req = reqwest({
            ...options,
            url,
            headers
        }).then(function (response) {
            const userId = req.request.getResponseHeader(userIdHeader);
            if (!!userId) {
                DemoStorage.setUserId(userId);
            }
            return response;
        });
        return req;
    }

    static getApiUrl(suffix: string) {
        return window.location.origin + suffix;
    }

    static async postEmpty(relativeUrl: string): Promise<any> {
        var req = this.prepareRequest({
            method: 'post',
            contentType: 'application/json'
        }, relativeUrl);

        return this.toPromise(req);
    }

    static async post<TInput, TOutput>(relativeUrl: string, data: TInput): Promise<TOutput> {
        var req = this.prepareRequest({
            method: 'post',
            type: 'json',
            data: data
        }, relativeUrl);

        return this.toPromise<TOutput>(req);
    }

    static async get<TOutput>(relativeUrl: string): Promise<TOutput> {
        var req = this.prepareRequest({
            method: 'get',
            contentType: 'application/json'
        }, relativeUrl);

        return this.toPromise<TOutput>(req);
    }
}