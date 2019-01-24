import { DemoAssetDto, DemoWalkthroughDto } from "../../models/dtos";

export interface WalkthroughEntry extends DemoWalkthroughDto {
    isActive: boolean;
}

export interface DemoEntry {
    slug: string;
    sourceCode: string;
    usingsLastLine: number;
    title: string;
    descriptionHtml: string;
    assets: DemoAssetDto[];
    walkthroughs: WalkthroughEntry[];
    nonInteractive: boolean;
    studioUrl: string;
}