import * as React from "react";
import { WalkthroughAssets } from "./WalkthroughAssets";
import { WalkthroughDescription } from "./WalkthroughDescription";
import { WalkthroughNav } from "./WalkthroughNav";

export function WalkthroughOverlay() {
    return <>
        <div className="walkthrough-top"></div>
        <div id="demo-highlight">
            <div id="highlight-top">
                <WalkthroughNav />
            </div>
            <div id="highlight-right"></div>
            <div id="highlight-bottom">
                <div className="padding">
                    <div className="walkthrough-description">
                    <WalkthroughDescription />
                    <WalkthroughAssets />
                    </div>
                </div>
            </div>
            <div id="highlight-left"></div>
        </div>
    </>;
}
