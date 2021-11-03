# .NET Software Engineer Technical Assignment

This repository contains the result of the technical assignment.

## Dependencies

The solution has been implemented in a Docker container that starts an instance of the application running in .Net 5 and the database SQL Server.

## Installation

Clone de package from the repository

```bash
git clone https://github.com/dimortizp/albelli.git
```
At the root of the downloaded project, you can run docker-compose to build and start the application

```bash
docker-compose build
docker-compose up
```
## Usage

Once you've completed the previous steps, the application will start in [http://localhost:8000/swagger/index.html](http://localhost:8000/swagger/index.html).
If for some reason, there's already an application running on the same port, you can always change the port in the [docker-compose.yml](https://github.com/dimortizp/albelli/blob/8e787d1c1090cca2eb406aa1f773337ed46a4433/docker-compose.yml#L6) file to the port that fits you better.
