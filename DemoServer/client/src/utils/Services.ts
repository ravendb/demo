import { ApiClient } from "./ApiClient";
import { DemoDto, DemoParamsDto, DemoVersionDto } from "../models/dtos";

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

    protected async get<T>(url: string): Promise<T> {
        return ApiClient.get<T>(this.fullUrl(url));
    }

    protected async postEmpty(url: string): Promise<any> {
        return ApiClient.postEmpty(this.fullUrl(url));
    }

    protected async post<TInput, TOutput>(url: string, data: TInput): Promise<TOutput> {
        return ApiClient.post<TInput, TOutput>(this.fullUrl(url), data);
    }
}

export class DemoService extends Service {
    constructor() {
        super("/demo");
    }

    async getVersions(): Promise<DemoVersionDto[]> {
        return this.get<DemoVersionDto[]>("get-versions");
    }

    async getMetadata(category: string, demo: string): Promise<DemoDto> {
        return this.get<DemoDto>(`get/${category}/${demo}`);
    }
}

export class RunDemoService extends Service {
    constructor(demoName: string) {
        super(`/execute/${demoName}`);
    }

    async setPrerequisites(): Promise<any> {
        return this.postEmpty(`set-prerequisites`);
    }

    async run(dto: DemoParamsDto): Promise<object> {
        return this.post<DemoParamsDto, object>("run", dto);
    }
}