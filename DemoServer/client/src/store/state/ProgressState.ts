import { UserProgress } from "../../models/progress";
import { DemoVersionDto } from "../../models/dtos";

export interface ProgressState {
    userProgress: UserProgress;
    demoVersions: DemoVersionDto[];
}