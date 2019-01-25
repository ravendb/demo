import { DemoParamsDto } from "./dtos/params";
import { DemoSlug, CategorySlug } from "./slugs";

export interface Demo {
    slug: DemoSlug;
    title: string;
}

export interface DemoWithProgress extends Demo {
    completed?: boolean;
}

export interface DemoCategory {
    slug: CategorySlug;
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
