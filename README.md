# dotnetcore-microservices-dummyproject

This is a simple dotnet core solution to study/test microservices

To run the application you can use docker-compose or use individual commands

# Docker-compose commands
`.\docker-scripts\docker-compose run start-dependencies`
`.\docker-scripts\docker-compose up`

# Docker commands

- Run RabbitMq locally using Docker
`docker run -p 5672:5672 rabbitmq`

- Run MongoDb locally using Docker
`docker run -d -p 27017:27017 mongo`

# Application commands
- Run Api
`dotnet run --urls "http://*:5000"`

- Run Activities Service:
`dotnet run --urls "http://*:5050"`

- Run Identity Service:
`dotnet run --urls "http://*:5051"`