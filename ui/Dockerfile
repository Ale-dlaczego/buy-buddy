FROM node:20-alpine

# Set the working directory
WORKDIR /app

# Copy package.json and yarn.lock
COPY package.json ./
COPY yarn.lock ./

# Install dependencies
RUN yarn install

# Copy the rest of the application code
COPY . .

# Build the application
RUN yarn build

# Install serve globally
RUN yarn global add serve

# Expose the port the app runs on
EXPOSE 8090

# Start the application using serve
CMD ["docker/start.sh"]