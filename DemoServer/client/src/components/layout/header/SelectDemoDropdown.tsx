import * as React from "react";
import { connect } from "react-redux";
import { AppState } from "../../../store/state";
import { categoryList, Category, DemoInfo } from "../../demos/categories";
import { DemoType } from "../../demos/demoTypes";
import { selectDemoType } from "../../../store/selectors/demos";
import { DemoThunkDispatch } from "../../../store";
import { goToDemo } from "../../../store/actions/demoActions";

interface StateProps {
    currentValue?: DemoType;
}

interface DispatchProps {
    goToDemo: (type: DemoType) => void;
}

type Props = StateProps & DispatchProps;

class SelectDemoDropdownComponent extends React.Component<Props, {}> {
    constructor(props) {
        super(props);

        this.goToDemo = this.goToDemo.bind(this);
    }

    private goToDemo(event: any) {
        const value = event.target.value as DemoType;
        this.props.goToDemo(value);
    }

    private getDemo = (demo: DemoInfo) => {
        return <option key={demo.type} value={demo.type}>{demo.title}</option>;
    }

    private getCategory = (category: Category) => {
        const { title, demos } = category;

        return <React.Fragment key={title}>
            <option disabled>{title}</option>
            {demos.map(this.getDemo)}
        </React.Fragment>;
    }

    render() {
        const { currentValue } = this.props;

        return <div>
            <select id="selectDemo" value={currentValue || ""} onChange={this.goToDemo}>
                {categoryList.map(this.getCategory)}
            </select>
        </div>;
    }
}

export const SelectDemoDropdown = connect<StateProps, DispatchProps>(
    ({ demos }: AppState): StateProps => {
        const demoType = selectDemoType(demos);

        return {
            currentValue: demoType
        };
    },
    (dispatch: DemoThunkDispatch): DispatchProps => {
        return {
            goToDemo: (type: DemoType) => dispatch(goToDemo(type))
        }
    }
)(SelectDemoDropdownComponent);