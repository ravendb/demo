import typescriptEslint from "@typescript-eslint/eslint-plugin";
import typescriptParser from "@typescript-eslint/parser";

export default [
  {
    files: ["client/src/**/*.ts", "client/src/**/*.tsx"],
    languageOptions: {
      parser: typescriptParser,
    },
    plugins: {
      "@typescript-eslint": typescriptEslint
    },
    rules: {
      "@typescript-eslint/no-unused-vars": "warn",
    }
  }
];
