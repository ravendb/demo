import { ApiClient, FormFile } from "./ApiClient";
import { DemoContextDto } from "../../models/dtos/context";
import { DemoDto } from "../../models/dtos/demo";
import { DemoParamsDto } from "../../models/dtos/params";

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

    protected async postWithFiles<TInput, TOutput>(url: string, data: TInput, files: FormFile[]): Promise<TOutput> {
        return ApiClient.postWithFiles<TInput, TOutput>(this.fullUrl(url), data, files);
    }
}

export class DemoService extends Service {
    constructor() {
        super("/demo");
    }

    async getDemoContext(): Promise<DemoContextDto> {
        return this.get<DemoContextDto>("get-context");
    }

    async getMetadata(category: string, demo: string): Promise<DemoDto> {
        return this.get<DemoDto>(`get/${category}/${demo}`);
    }

    async resetDatabase(): Promise<any> {
        return this.postEmpty("reset-database");
    }
}

export class RunDemoService extends Service {
    constructor(demoName: string) {
        super(`/execute/${demoName}`);
    }

    async setPrerequisites(): Promise<any> {
        return this.postEmpty("set-prerequisites");
    }

    async run(dto: DemoParamsDto): Promise<object> {
        return this.post<DemoParamsDto, object>("run", dto);
    }

    async runWithFiles(dto: DemoParamsDto, files: FormFile[]): Promise<object> {
        return this.postWithFiles<DemoParamsDto, object>("run", dto, files);
    }
}
