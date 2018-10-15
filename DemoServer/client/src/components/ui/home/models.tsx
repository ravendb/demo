export interface Demo {
    name: string;
    img: string;
}

export interface DemoWithProgress extends Demo {
    completed?: boolean;
}

export interface DemoCategory {
    name: string;
    demos: DemoWithProgress[];
}