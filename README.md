# LegacySQL Support System

A .NET 8 Web API designed to modernize support ticket processing. This application integrates AI to analyze legacy database interactions, featuring a flexible architecture that supports both live OpenAI integration and local development modes.

## 🚀 Key Features
* **AI-Powered Analysis:** Leverages Generative AI to interpret support tickets and suggest SQL-based solutions.
* **Flexible Service Architecture:** Implements the **Strategy Pattern** to seamlessly switch between:
    * `OpenAIService`: For live, production-grade responses.
    * `MockAIService`: For zero-cost local testing and rapid development.
* **Ticket Management:** Handles lifecycle operations using Entity Framework Core.
* **Clean Architecture:** Built with strict Dependency Injection principles (`IAIService`).

## 🛠 Tech Stack
* **Core:** C# / .NET 8 Web API
* **Data:** Entity Framework Core & SQL Server
* **AI Integration:** OpenAI API (configurable)
* **Testing:** Custom Mocking Services
