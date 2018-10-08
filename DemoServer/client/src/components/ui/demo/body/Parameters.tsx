import * as React from "react";

interface ParameterItem {
    type: "text" | "date" | "number";
    name: string;
    placeholder: string;
    datatype?: "integer" | "float"
}

interface ParametersProps {
    items: ParameterItem[];
}

export class Parameters extends React.Component<ParametersProps, {}> {
    displayItem(item: ParameterItem, index: number) {
        const { type, name, placeholder, datatype } = item;

        return <div className="parameter" key={`parameter_${name}_${index}`}>
            <input type={type} datatype={datatype} className="parameter-input" placeholder={placeholder} />
            <div className="parameter-label">
                <div className="name">{name}</div>
                <div className="type"></div>
            </div>
        </div>
    }

    render() {
        const { items } = this.props;

        return <div className="parameters">
            {items.map((x, i) => this.displayItem(x, i))}
        </div>;
    }
}