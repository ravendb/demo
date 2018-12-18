import * as React from "react";

export interface FileInputProps {
    className?: string;
    disabled?: boolean;
    onFileChange: (file: File) => void;
}

interface State {
    labelText: string;
}

export class FileInput extends React.Component<FileInputProps, State> {
    constructor(props) {
        super(props);

        this.state = { labelText: "Choose file" };

        this.handleFileChange = this.handleFileChange.bind(this);
    }

    handleFileChange(event: React.ChangeEvent<HTMLInputElement>) {
        const { onFileChange } = this.props;
        const file = event.target.files && event.target.files.length > 0 && event.target.files[0];
        onFileChange(file);

        this.setState({ labelText: file.name });
    }

    render() {
        const { disabled, className } = this.props;
        const { labelText } = this.state;
        
        return <>
            <input type="file" className={className} onChange={this.handleFileChange} disabled={disabled} />
            <label className="fileInput-label">{labelText}</label>
        </>;
    }
}