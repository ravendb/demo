import * as React from "react";
import { AssetType } from "../../models/dtos";

interface AssetIconProps {
    type: AssetType;
}

export class AssetIcon extends React.Component<AssetIconProps, {}> {
    typeToIcon(type: AssetType): string {
        switch (type) {
            case "Document":
                return "document";

            case "Downloadable":
                return "download";

            case "Demo":
                return "demo";

            case "Link":
            default:
                return "link";
        }
    }

    render() {
        const { type } = this.props;
        const icon = this.typeToIcon(type);
        return <i className={`icon-${icon}`}></i>;
    }
}