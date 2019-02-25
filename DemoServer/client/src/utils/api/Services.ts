import { ApiClient, FormFile } from "./ApiClient";
import { DemoContextDto } from "../../models/dtos/context";
import { DemoDto } from "../../models/dtos/demo";
import { DemoParamsDto } from "../../models/dtos/params";
import { Language } from "../../models/common";

export interface PagedList<T> {
    totalResults: number;
    items: T[];
}

abstract class Service {
    constructor(private apiUrl: string) {
    }

    private _fullUrl(url: string) {
        return `${this.apiUrl}/${url}`;
    }

    protected async _get<T>(url: string): Promise<T> {
        return ApiClient.get<T>(this._fullUrl(url));
    }

    protected async _postEmpty(url: string): Promise<any> {
        return ApiClient.postEmpty(this._fullUrl(url));
    }

    protected async _post<TInput, TOutput>(url: string, data: TInput): Promise<TOutput> {
        return ApiClient.post<TInput, TOutput>(this._fullUrl(url), data);
    }

    protected async _postWithFiles<TInput, TOutput>(url: string, data: TInput, files: FormFile[]): Promise<TOutput> {
        return ApiClient.postWithFiles<TInput, TOutput>(this._fullUrl(url), data, files);
    }
}

export class DemoService extends Service {
    constructor() {
        super("/demo");
    }

    public async getDemoContext(): Promise<DemoContextDto> {
        return this._get<DemoContextDto>("get-context");
    }

    public async getMetadata(language: Language, category: string, demo: string): Promise<DemoDto> {
        return this._get<DemoDto>(`get/${language}/${category}/${demo}`);
    }

    public async resetDatabase(): Promise<any> {
        return this._postEmpty("reset-database");
    }
}

export class RunDemoService extends Service {
    constructor(demoName: string) {
        super(`/execute/${demoName}`);
    }

    public async setPrerequisites(): Promise<any> {
        return this._postEmpty("set-prerequisites");
    }

    public async run(dto: DemoParamsDto): Promise<object> {
        return this._post<DemoParamsDto, object>("run", dto);
    }

    public async runWithFiles(dto: DemoParamsDto, files: FormFile[]): Promise<object> {
        return this._postWithFiles<DemoParamsDto, object>("run", dto, files);
    }
}
