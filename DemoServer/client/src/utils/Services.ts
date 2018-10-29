import { ApiClient } from "./ApiClient";
import { ExampleDataDto } from "../models/exampleModels";
import { DemoDto } from "../models/dtos";

export interface PagedList<T> {
    totalResults: number;
    items: T[];
}

abstract class Service {
    constructor(private apiUrl: string) {
    }

    private fullUrl(url: string) {
        return `${this.apiUrl}/${url}`;
    }

    protected get<T>(url: string): Promise<T> {
        return ApiClient.get(this.fullUrl(url));
    }

    protected postEmpty(url: string): Promise<any> {
        return ApiClient.postEmpty(this.fullUrl(url));
    }

    protected post<TInput, TOutput>(url: string, data: TInput): Promise<TOutput> {
        return ApiClient.post(this.fullUrl(url), data);
    }
}

export class ExampleService extends Service {
    constructor() {
        super("/example");
    }

    getData(): Promise<ExampleDataDto> {
        return this.get("get-data");
    }
}

export class DemoService extends Service {
    constructor() {
        super("/demo");
    }

    getMetadata(category: string, demo: string): Promise<DemoDto> {
        return this.get(`get/${category}/${demo}`);
    }
}