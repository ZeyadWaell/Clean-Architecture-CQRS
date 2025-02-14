 LustBorn Chat Application - CQRS & SignalR

## 🎯 **Design Patterns Used**
This project follows a **scalable software architecture** using industry-standard design patterns:

| **Pattern**            | **Purpose** |
|------------------------|------------|
| **CQRS (Command Query Responsibility Segregation)** | Separates **read** and **write** operations for better scalability. |
| **Mediator Pattern** | Uses `MediatR` to decouple request handling logic. |
| **Repository Pattern** | Abstracts database operations for better maintainability. |
| **Unit of Work Pattern** | Ensures atomic transactions across repositories. |
| **Strategy Pattern** | Used for AI bot integration (`ChatGPTBotStrategy` and `GeminiBotStrategy`). |
| **Factory Pattern** | Manages the selection of bot strategies dynamically (`BotStrategyFactory`). |
| **Singleton Pattern** | Used for `BotStrategyFactory` and external API clients to **reuse instances and reduce memory overhead**. |
| **Result Pattern** | Standardized API responses using `ApiResponse<T>` to ensure **consistency and error handling**. |
| **Adapter Pattern** | Allows **seamless integration of multiple chatbot APIs** by adapting different responses into a unified structure. |
| **Dependency Injection** | Ensures loose coupling of services and repositories. |
| **Generic Pattern** | Generic Type Object |

