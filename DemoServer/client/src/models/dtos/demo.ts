export type AssetType = "Link" | "Downloadable" | "Document" | "Demo";

export interface DemoAssetDto {
    type: AssetType;
    title: string;
    url: string;
}

export interface LinesRangeDto {
    start: number;
    end: number;
}

export interface DemoLinkDto {
    url: string;
    title: string;
}

export interface DemoWalkthroughDto {
    title: string;
    slug: string;
    descriptionHtml: string;
    lines: LinesRangeDto;
    assets: DemoAssetDto[];
    demoLink: DemoLinkDto;
}

export interface DemoDto {
    slug: string;
    sourceCode: string;
    usingsLastLine: number;
    title: string;
    descriptionHtml: string;
    assets: DemoAssetDto[];
    walkthroughs: DemoWalkthroughDto[];
    nonInteractive: boolean;
    studioUrl: string;
}
