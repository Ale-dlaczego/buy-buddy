/* eslint-disable @typescript-eslint/no-require-imports */
const fs = require('fs');
const path = require('path');
const dotenv = require('dotenv');

dotenv.config();

const templatePath = path.join(__dirname, '../env.js');
const outputPath = path.join(__dirname, '../public/env.js');
const outputBuildPath = path.join(__dirname, '../build/env.js');

const envVariables = {
  ...process.env,
};

fs.readFile(templatePath, 'utf8', (err, data) => {
  if (err) {
    console.error('Error reading template file:', err);
    process.exit(1);
  }

  let result = data;
  for (const [key, value] of Object.entries(envVariables)) {
    const placeholder = `__${key}__`;
    result = result.replace(new RegExp(placeholder, 'g'), value);
  }

  fs.writeFile(outputPath, result, 'utf8', (err) => {
    if (err) {
      console.error('Error writing config file:', err);
      process.exit(1);
    }
    console.log('Config file (/public) generated successfully.');
  });

  // write to build folder if exists
  if (fs.existsSync(path.join(__dirname, '../build'))) {
    fs.writeFile(outputBuildPath, result, 'utf8', (err) => {
      if (err) {
        console.error('Error writing config file:', err);
        process.exit(1);
      }
      console.log('Config file (/build) generated successfully.');
    });
  }
});
