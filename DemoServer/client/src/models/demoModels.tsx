import { DemoParamsDto } from "./dtos";

export interface Demo {
    slug: string;
    title: string;
}

export interface DemoWithProgress extends Demo {
    completed?: boolean;
}

export interface DemoCategory {
    slug: string;
    demos: DemoWithProgress[];
}

export interface ParameterPair {
    name: string;
    value: any;
}

export function toDemoParamsDto(parameters: ParameterPair[]): DemoParamsDto {
    return parameters.reduce((acc, current) => {
        return {...acc, [current.name]: current.value}
    }, {});
}

export interface UserProgressDto {
    completedDemos: DemoProgressDto[];
}

export interface DemoProgressDto {
    categorySlug: string;
    demoSlug: string;
}