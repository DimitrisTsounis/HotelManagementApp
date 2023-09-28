# Simple Hotel Management Application

## Overview

This is a simple Hotel Management application that provides CRUD (Create, Read, Update, Delete) operations for Hotel and Booking entities. The application is built using C# and .NET 7 in conjuction with EF CORE 7 and follows the Repository pattern for data management.

## Features

- CRUD operations for Hotels and Bookings.
- Utilizes the Repository pattern for data access and management.
- Middleware for error-handling.
- Fluent Validations.
- Unit Tests & Integration Tests.
- Docker Compose file included, in order to set up a SQL Server instance, run the API, apply migrations, and seed initial data to the database.
- Postman collection provided with all available API endpoints as well as environments to choose (debug and docker environments that alter url).
- Supports Swagger for easy API documentation when run in development mode.

## Getting Started

### Prerequisites

- [Docker](https://www.docker.com/get-started) installed on your machine.

### Setup Instructions

1. Clone the repository to your local machine:

    ```
    git clone https://github.com/DimitrisTsounis/HotelManagementApp.git
    ```

2. Navigate to the project directory:

    ```
    cd hotel-management
    ```

3. Run the following command to start the application:

    ```
    docker compose up -d --build
    ```

    This will set up the SQL Server, launch the API, apply migrations, and seed the database.

4. Import the provided Postman collection & environments to your postman and interact with the API (choose DockerEnv environment when running the app with docker compose).

## Debug
### Prerequisites

- [Docker](https://www.docker.com/get-started) installed on your machine.

### Setup Instructions
1. Pull SQL Server Docker Image:

    ```
    docker pull mcr.microsoft.com/mssql/server:2019-latest
    ```

2. Run SQL Server Container:

    ```
    docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=sa12345!" -p 1433:1433 --name sqlserver -d mcr.microsoft.com/mssql/server:2019-latest
    ```

3. Run App through visual studio with "IIS Express" profile.

4. Import the provided Postman collection & environments to your postman and interact with the API (choose DebugEnv environment when running the app through visual studio).


## API Endpoints

|Verb| URL|
|---|---|
|GET |/api/bookings|
|POST |/api/bookings|
|GET|/api/bookings/id/{id}|
|PATCH|/api/bookings/id/{id}|
|DELETE|/api/bookings/id/{id}|
|GET|/api/bookings/hotel?hotelId={hotelId}|
|||
|GET|/api/hotels|
|POST|/api/hotels|
|GET|/api/hotels/id/{id}|
|PATCH|/api/hotels/id/{id}|
|DELETE|/api/hotels/id/{id}|
|GET|/api/hotels/name/{name}|