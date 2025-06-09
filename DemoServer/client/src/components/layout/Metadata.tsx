import * as React from "react";
import { connect } from "react-redux";
import { Helmet } from "react-helmet-async";
import { AppState } from "../../store/state";
import { DemoHtmlMetadata, selectHtmlMetadata } from "../../store/selectors/metadata";

interface StateProps {
    metadata: DemoHtmlMetadata;
}

class MetadataComponent extends React.Component<StateProps, {}> {

    public render() {
        const { metadata } = this.props;
        const { title, description, image, url } = metadata;

        return <Helmet>
            <title>{title}</title>

            {description && <meta name="description" content={description} />}
            {description && <meta property="og:description" content={description} />}
            {description && <meta property="twitter:description" content={description} />}
                
            <meta property="og:site_name" content="RavenDB Demo" />
            <meta property="og:type" content="website" />
            <meta property="og:title" content={title} />

            <meta property="og:url" content={url} />
            <meta property="og:image" content={image} />

            <meta property="twitter:card" content="summary_large_image" />
            <meta name="twitter:creator" content="@RavenDB" />
            <meta property="twitter:site" content="@RavenDB" />
            <meta property="twitter:title" content={title} />
            <meta property="twitter:image" content={image} />
            <meta property="twitter:url" content={url} />
        </Helmet>;
    }
}

function mapStateToProps({ demos }: AppState): StateProps {
    const metadata = selectHtmlMetadata(demos);

    return {
        metadata
    };
}

export const Metadata = connect<StateProps, {}, {}>(
    mapStateToProps
)(MetadataComponent);
