import Axios, * as axios from "axios";
import { DemoStorage } from "./localStorage/DemoStorage";
import { AxiosResponse } from "axios";

const userIdHeader = "DemoUser-ID";

export interface FormFile {
    name: string;
    file: File;
}

export class ApiClient {
    private static prepareRequest<T>(config: axios.AxiosRequestConfig, relativeUrl: string): Promise<T> {
        const url = this.getApiUrl(relativeUrl);
        let headers = config.headers || {};
        headers = { ...headers, [userIdHeader]: DemoStorage.getUserId() };

        let req = Axios({...config,
            headers,
            url
        }).then((response: AxiosResponse<T>) => {
            const userId = response.headers && response.headers[userIdHeader];
            if (!!userId) {
                DemoStorage.setUserId(userId);
            }
            return response.data;
        });

        return req;
    }

    static getApiUrl(suffix: string) {
        return window.location.origin + suffix;
    }

    static async postEmpty(relativeUrl: string): Promise<any> {
        return this.prepareRequest({
            method: 'post',
            data: this.getFormData({})
        }, relativeUrl);
    }

    private static getFormData(data: any): FormData {
        const formData = new FormData();

        for (const key of Object.keys(data)) {
            formData.append(key, data[key]);
        }

        return formData;
    }

    static async post<TInput, TOutput>(relativeUrl: string, data: TInput): Promise<TOutput> {
        const formData = this.getFormData(data);

        return this.prepareRequest<TOutput>({
            method: 'post',
            data: formData
        }, relativeUrl);
    }

    static async postWithFiles<TInput, TOutput>(relativeUrl: string, data: TInput, files: FormFile[]): Promise<TOutput> {
        const formData = this.getFormData(data);

        files.forEach(x => {
            formData.append(x.name, x.file);
        });

        return this.prepareRequest<TOutput>({
            method: 'post',
            data: formData
        }, relativeUrl);
    }

    static async get<TOutput>(relativeUrl: string): Promise<TOutput> {
        return this.prepareRequest<TOutput>({
            method: 'get'
        }, relativeUrl);
    }
}