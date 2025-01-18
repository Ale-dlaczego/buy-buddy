## Creating .env variables

To set up environment variables for your project, follow these steps:

1. **Create a `.env` File:**
  - In the root directory of your project, create a file named `.env`.
  - Add your environment-specific variables in the format `KEY=VALUE`.

  ```plaintext
  ENV_VARIABLE=variable_value
  ```

2. **Update `env.js`:**
  - Locate the `env.js` file in project.
  - Add the necessary configuration to read the variables from the `.env` file using `window._env_`.
  - You can map key inside react code to key used in `.env` file with `__` prefix than `key` and `__` suffix.

  ```javascript
  window._env_ = {
    ENV_VARIABLE: "__ENV_VARIABLE__",
  };
  ```

3. **Use Environment Variables in React files:**
- In your React components, you can access the environment variables using `window._env_`.

```javascript
const envVariable = window._env_.ENV_VARIABLE;
console.log('Environment Variable:', envVariable);

// LOG: Environment Variable: "variable_value"
```

This will allow you to use the environment variables defined in your `.env` file within your React code.