# Message Driven Application

## Introduction

This repository contains two key services: a **Producer** and a **Consumer**, developed using .NET and C# with RabbitMQ as the message broker. The Producer is responsible for sending messages to a RabbitMQ queue, while the Consumer processes these messages and records the outcomes.

Before running the application, ensure you have the following installed:

- [.NET SDK](https://dotnet.microsoft.com/download) (version 6.0 or later)
- [Docker](https://www.docker.com/get-started) (to run RabbitMQ)
- A code editor like [Visual Studio](https://visualstudio.microsoft.com/) or [Visual Studio Code](https://code.visualstudio.com/)

## How to Run the Application

# Setup RabbitMQ

1. To begin, you need to set up RabbitMQ. You can do this easily using Docker:
   ```sh
   docker run -d --name rabbitmq -p 5672:5672 -p 15672:15672 rabbitmq:3-management
   ```

## Run the Producer Service

1. Open a terminal and navigate to the `ProducerService.API` directory.
2. Start the Producer service by executing the following command:
   ```sh
   dotnet run
   ```

## Run the Consumer Service

1. Open a terminal and navigate to the `ConsumerService.API` directory.
2. Start the Consumer service by executing the following command:
   ```sh
   dotnet run
   ```

## How to Run the Tests

1. For the Producer Service, navigate to the test project directory, i.e. `ProducerService.Tests`.
2. Execute below command:
   ```sh
   dotnet test
   ```
3. For the Producer Service, navigate to the test project directory, i.e. `ConsumerService.Tests`.
4. Execute below command:
   ```sh
   dotnet test
   ```

## Project Structure

### ProducerService

#### Routes

`GET /api/Producer`: Sends a message to process.

### ConsumerService


#### Routes

- `GET api/MessageProcessingResult/Summary`: Retrieves a summary of processed messages, including counts of successfully processed and failed messages.
- `GET api/MessageProcessingResult/Success`: Returns details of messages that were successfully processed.
- `GET api/MessageProcessingResult/Failed`: Returns details of messages that failed during processing.
- `GET api/MessageProcessingResult/Processed-Messages`: Retrieves details of all processed messages within a specified time range, regardless of their success or failure status.

### Testing with Swagger

Swagger provides an interactive interface for testing your API endpoints easily. To access the Swagger UI, follow these steps:

 1. After running both services, open a web browser.
 2. Navigate to:
  - `http://localhost:5132/swagger` for the Producer Service.
  - `http://localhost:5104/swagger` for the Consumer Service.
 3. The Swagger UI will display all available endpoints along with their descriptions.

## Future Enhancements
- **Retry Logic:** Implement retry logic in the Consumer service to handle transient failures during message processing. This can help ensure messages are reprocessed a specified number of times before being marked as failed.
- **Error Handling:** Develop more robust error handling and logging mechanisms to track issues in message processing and provide meaningful feedback.
- **Dynamic Configuration:** Allow dynamic configuration of RabbitMQ settings and service parameters through a configuration file or environment variables.
- **Unit Testing:** Increase test coverage by adding unit tests for critical components of both services.



