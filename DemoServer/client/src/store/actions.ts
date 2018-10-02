import { ExampleAction } from "../features/example/actions";
import { ErrorAction } from "../features/common/actions";

export type DemoAction = ExampleAction | ErrorAction;