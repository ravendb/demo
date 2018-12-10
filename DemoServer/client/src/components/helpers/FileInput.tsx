import * as React from "react";

export interface FileInputStateProps {
    className?: string;
    disabled?: boolean;
}

export interface FileInputDispatchProps {
    onFileChange: (file: File) => void;
}

export type FileInputProps = FileInputStateProps & FileInputDispatchProps;

export class FileInput extends React.Component<FileInputProps, {}> {
    constructor(props) {
        super(props);

        this.handleFileChange = this.handleFileChange.bind(this);
    }

    handleFileChange(event: React.ChangeEvent<HTMLInputElement>) {
        const { onFileChange } = this.props;
        const file = event.target.files && event.target.files.length > 0 && event.target.files[0];
        onFileChange(file);
    }

    render() {
        const { disabled, className } = this.props;

        return <input type="file" className={className} onChange={this.handleFileChange} disabled={disabled} />;
    }
}