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
        if (current.value) {
            return { ...acc, [current.name]: current.value };
        }
        return acc;
    }, {});
}