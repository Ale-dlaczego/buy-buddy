import typescriptPlugin from '@typescript-eslint/eslint-plugin';
import tseslint from 'typescript-eslint';
import prettierPlugin from 'eslint-plugin-prettier';
import reactPlugin from 'eslint-plugin-react';
import reactHooksPlugin from 'eslint-plugin-react-hooks';
import jsxA11yPlugin from 'eslint-plugin-jsx-a11y';
import typescriptParser from '@typescript-eslint/parser';

const config = [
  {
    // Globalne ustawienia dla wszystkich plików
    languageOptions: {
      parser: typescriptParser, // Określamy parser TypeScript
      parserOptions: {
        ecmaVersion: 2023,
        sourceType: 'module',
        ecmaFeatures: {
          jsx: true,
        },
      },
      globals: {
        browser: true,
        node: true,
        es2023: true,
      },
    },
    settings: {
      react: {
        version: 'detect',
      },
    },
    plugins: {
      typescript: typescriptPlugin,
      prettier: prettierPlugin,
      react: reactPlugin,
      'react-hooks': reactHooksPlugin,
      'jsx-a11y': jsxA11yPlugin,
    },
  },
  {
    files: ['**/*.ts', '**/*.tsx', '**/*.cts', '**/*.mts'],
    languageOptions: {
      globals: {
        React: 'readonly', // Definiowanie React jako globalnej zmiennej w plikach TS
      },
    },
    rules: {
      ...typescriptPlugin.configs.recommended.rules,
      ...reactPlugin.configs.recommended.rules,
      ...reactHooksPlugin.configs.recommended.rules,
      ...jsxA11yPlugin.configs.recommended.rules,
      'prettier/prettier': 'error',
      'react/react-in-jsx-scope': 'off',
      'react/prop-types': 'off',
      '@typescript-eslint/no-unused-vars': [
        'warn',
        {
          argsIgnorePattern: '^_',
          varsIgnorePattern: '^_',
        },
      ],
    },
  },
  {
    files: ['**/*.js', '**/*.jsx'],
    rules: {
      'prettier/prettier': 'error',
    },
  },
  {
    // Ignorowanie folderów build i node_modules
    ignores: ['node_modules/', 'dist/', 'build/'],
  },
];

export default tseslint.config(
  tseslint.configs.recommended,
  ...config,
);
