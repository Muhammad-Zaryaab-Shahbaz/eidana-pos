# Use Node.js image to build the Angular app
FROM node:16 AS build
WORKDIR /app

# Copy package.json and package-lock.json first (for caching layers)
COPY package*.json ./

# Force install dependencies, ignoring dependency tree conflicts
RUN npm install --force

# Copy the rest of the application
COPY . .

# Build the Angular app
RUN npm run build --configuration=production
# RUN npm run build

# Use Nginx to serve the Angular app
FROM nginx:alpine
COPY --from=build /app/dist/pos /usr/share/nginx/html
EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]
