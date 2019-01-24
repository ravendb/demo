import * as React from "react";
import { connect } from "react-redux";
import { AppState } from "../../../store/state";
import { CategorySlug, DemoSlug } from "../../../models/slugs";
import { CategoryHeaderDto, DemoHeaderDto } from "../../../models/dtos/context";
import { getDemoUrlForType } from "../../../store/selectors/urlGetters";
import { push } from "connected-react-router";
import { getContext } from "../../../store/actions/demoActions";
import { DemoThunkDispatch } from "../../../store";

interface SlugPair {
    category: CategorySlug;
    demo: DemoSlug;
}

interface StateProps {
    currentCategory?: CategorySlug;
    currentDemo?: DemoSlug;
    categories: CategoryHeaderDto[];
}

interface DispatchProps {
    loadContext: () => void;
    goToDemo: (category: CategorySlug, demo: DemoSlug) => void;
}

type Props = StateProps & DispatchProps;

class SelectDemoDropdownComponent extends React.Component<Props, {}> {
    constructor(props) {
        super(props);

        this.goToDemo = this.goToDemo.bind(this);
    }

    componentDidMount() {
        const { categories, loadContext: loadData } = this.props;

        if (!categories || categories.length === 0) {
            loadData();
        }
    }

    private fromPathSlugsToValue(category: CategorySlug, demo: DemoSlug): string {
        return `${category}/${demo}`;
    }

    private fromValueToPathSlugs(value: string): SlugPair {
        const split = value && value.split("/");

        return split && split.length >= 2 && {
            category: split[0] as CategorySlug,
            demo: split[1] as DemoSlug
        };
    }

    private goToDemo(event: any) {
        const value = event.target.value as string;
        const slugs = this.fromValueToPathSlugs(value);

        const { category, demo } = slugs;
        this.props.goToDemo(category, demo);
    }

    private getDemo = (categorySlug: CategorySlug, demo: DemoHeaderDto) => {
        const value = this.fromPathSlugsToValue(categorySlug, demo.slug);
        return <option key={demo.slug} value={value}>{demo.title}</option>;
    }

    private getCategory = (category: CategoryHeaderDto) => {
        const { title, demos } = category;

        return <React.Fragment key={title}>
            <option disabled>{title}</option>
            {demos.map(x => this.getDemo(category.slug, x))}
        </React.Fragment>;
    }

    render() {
        const { categories, currentCategory, currentDemo } = this.props;
        const currentValue = this.fromPathSlugsToValue(currentCategory, currentDemo);
        const show = !!categories && categories.length > 0;

        return show && <div>
            <select id="selectDemo" value={currentValue || ""} onChange={this.goToDemo}>
                {categories.map(this.getCategory)}
            </select>
        </div>;
    }
}

function mapStateToProps({ demos }: AppState): StateProps {
    const { categories, categorySlug, demoSlug } = demos;

    return {
        categories,
        currentCategory: categorySlug,
        currentDemo: demoSlug
    };
}

function mapDispatchToProps(dispatch: DemoThunkDispatch): DispatchProps {
    return {
        loadContext: () => dispatch(getContext()),
    
        goToDemo: (category: CategorySlug, demo: DemoSlug) => {
            const url = getDemoUrlForType(category, demo);
            return dispatch(push(url));
        }
    };
}

export const SelectDemoDropdown = connect<StateProps, DispatchProps>(
    mapStateToProps,
    mapDispatchToProps
)(SelectDemoDropdownComponent);