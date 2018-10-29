export type AssetType = "Link" | "Downloadable" | "Document";

export interface DemoAssetDto {
    type: AssetType;
    title: string;
    url: string;
}

interface LinesRangeDto {
    start: number;
    end: number;
}

interface DemoWalkthroughDto {
    title: string;
    slug: string;
    descriptionHtml: string;
    lines: LinesRangeDto;
    assets: DemoAssetDto[];
}

export interface DemoDto {
    slug: string;
    sourceCode: string;
    usingsLastLine: number;
    assets: DemoAssetDto[];
    walkthroughs: DemoWalkthroughDto[];
}