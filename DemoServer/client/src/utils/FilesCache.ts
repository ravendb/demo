export interface FilesCacheEntry {
    key: string;
    value: File;
}

export class FilesCache {
    private static files: FilesCacheEntry[] = [];

    static addOrUpdate(key: string, file: File) {
        const registeredFile = this.files.find(x => x.key == key);

        if (registeredFile) {
            registeredFile.value = file;
        } else {
            this.files.push({ key, value: file });
        }
    }

    static remove(key: string) {
        this.files = this.files.filter(x => x.key !== key);
    }

    static getForKeys(keys: string[]): FilesCacheEntry[] {
        return this.files.filter(x => keys.indexOf(x.key) >= 0);
    }

    static clear() {
        this.files = [];
    }
}