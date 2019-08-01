import * as React from "react";
import { connect } from "react-redux";
import { AppState } from "../../../store/state";
import { CategorySlug, DemoSlug } from "../../../models/slugs";
import { DemoForLanguage, CategoryWithDemoVersions } from "../../../models/dtos/context";
import { getContext } from "../../../store/actions/demo";
import { DemoThunkDispatch } from "../../../store";
import { goToDemoPage } from "../../../store/actions/navigation";

interface SlugPair {
    category: CategorySlug;
    demo: DemoSlug;
}

interface StateProps {
    currentCategory?: CategorySlug;
    currentDemo?: DemoSlug;
    categories: CategoryWithDemoVersions[];
}

interface DispatchProps {
    loadContext: () => void;
    goToDemo: (category: CategorySlug, demo: DemoSlug, withPush: boolean) => void;
}

type Props = StateProps & DispatchProps;

class SelectDemoDropdownComponent extends React.Component<Props, {}> {
    constructor(props) {
        super(props);

        this._goToDemo = this._goToDemo.bind(this);
    }

    public componentDidMount() {
        const { categories, loadContext: loadData } = this.props;

        if (!categories || categories.length === 0) {
            loadData();
        }
    }

    private _fromPathSlugsToValue(category: CategorySlug, demo: DemoSlug): string {
        return `${category}/${demo}`;
    }

    private _fromValueToPathSlugs(value: string): SlugPair {
        const split = value && value.split("/");

        return split && split.length >= 2 && {
            category: split[0] as CategorySlug,
            demo: split[1] as DemoSlug
        };
    }

    private _goToDemo(event: any) {
        const value = event.target.value as string;
        const slugs = this._fromValueToPathSlugs(value);

        const { category, demo } = slugs;
        this.props.goToDemo(category, demo, true);
    }

    private getDemo = (categorySlug: CategorySlug, demo: DemoForLanguage) => {
        const value = this._fromPathSlugsToValue(categorySlug, demo.slug);
        return <option key={demo.slug} value={value}>{demo.title}</option>;
    }

    private getCategory = (category: CategoryWithDemoVersions) => {
        const { title, demos } = category;

        return <React.Fragment key={title}>
            <option disabled>{title}</option>
            {demos.map(x => this.getDemo(category.slug, x))}
        </React.Fragment>;
    }

    public render() {
        const { categories, currentCategory, currentDemo } = this.props;
        const currentValue = this._fromPathSlugsToValue(currentCategory, currentDemo);
        const show = !!categories && categories.length > 0;

        return show && <div>
            <select id="selectDemo" value={currentValue || ""} onChange={this._goToDemo}>
                {categories.map(this.getCategory)}
            </select>
        </div>;
    }
}

function mapStateToProps({ demos }: AppState): StateProps {
    const { categoriesWithVersions, categorySlug, demoSlug } = demos;

    return {
        categories: categoriesWithVersions,
        currentCategory: categorySlug,
        currentDemo: demoSlug
    };
}

function mapDispatchToProps(dispatch: DemoThunkDispatch): DispatchProps {
    return {
        loadContext: () => dispatch(getContext()),
    
        goToDemo: (category: CategorySlug, demo: DemoSlug) => {
            return dispatch(goToDemoPage(category, demo, true));
        }
    };
}

export const SelectDemoDropdown = connect<StateProps, DispatchProps>(
    mapStateToProps,
    mapDispatchToProps
)(SelectDemoDropdownComponent);
