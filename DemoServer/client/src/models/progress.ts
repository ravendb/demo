export interface UserProgress {
    completedDemos: DemoProgress[];
}

export interface DemoProgress {
    category: string;
    demo: string;
    demoHash: string;
}
