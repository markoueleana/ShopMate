
# 🛒 ShopMate Microservices
A  microservices based e-shop application built with ASP.NET Core, Docker, and event-driven architecture using RabbitMQ.
---

## 📦 Architecture Overview
![Screenshot 2025-04-29 183431](https://github.com/user-attachments/assets/99425d9a-5794-4091-820c-cf34b567470b)




This ShopMate consists of the following microservices:

- **Catalog API** – Manages product information
- **Cart API** – Manages shopping cart functionality
- **Order Service** – Manages orders using DDD & Clean Architecture principles

Each service is containerized using Docker.

---
## 📧 Monthly Email Notifications

The application includes a background job to send monthly emails to customers using **Hangfire** and **MailKit**.

---
##  Tech Stack

- **.NET Core**
- **Minimal APIs (Carter)**
- **CQRS (MediatR)**
- **Object Mapping (Mapster)**
- **MassTransit + RabbitMQ** for asynchronous communication
- **PostgreSQL** for Catalog & Cart services
- **SQL Server** for the Order service
- **Docker Compose** for container orchestration
---

## 🔄 Microservice Communication

- The **Cart API** publishes a `CheckoutCartEvent` when the user checks out.
- The **Ordering API** consumes this event using **MassTransit** via **RabbitMQ**, initiating the order workflow.

---

## 🐳 Running the Application

```bash
docker-compose up --build
```
