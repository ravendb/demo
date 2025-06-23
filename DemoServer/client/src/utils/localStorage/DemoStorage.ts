import { UserProgress } from "../../models/progress";

function saveInStorage<T>(key: string, value: T) {
    const text = JSON.stringify(value);
    localStorage.setItem(key, text);
}

function readFromStorage<T>(key: string) {
    const item = localStorage.getItem(key);
    return JSON.parse(item) as T;
}

class Keys {
    static readonly userId = "userId";
    static readonly userProgress = "userProgress";
    static readonly demoVersions = "demoVersions";
}

export class DemoStorage {
    static setUserProgress(progress: UserProgress) {
        saveInStorage(Keys.userProgress, progress);
    }

    static getUserProgress(): UserProgress {
        return readFromStorage(Keys.userProgress);
    }

    static setUserId(id: string) {
        saveInStorage(Keys.userId, id);
    }

    static getUserId() {
        return readFromStorage<string>(Keys.userId);
    }
}
