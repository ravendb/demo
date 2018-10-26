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