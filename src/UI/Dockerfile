FROM node:16.14
 
WORKDIR /usr/src/app
 
COPY src/UI/package*.json ./
 
RUN npm install --silent
 
COPY src/UI/ .
 
EXPOSE 3000
 
CMD [ "npm", "start" ]