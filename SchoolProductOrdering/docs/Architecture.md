# System Architecture

## Architectural Overview
The School Product Ordering System follows a **layered architecture** design pattern.

The system is divided into the following layers:

1. Presentation Layer (Razor Pages)
2. Business Logic Layer (Page Models)
3. Data Access Layer (Entity Framework Core)
4. Database Layer (SQL Server)

---

## Architecture Description

### 1. Presentation Layer
- Built using Razor Pages
- Responsible for displaying pages to users
- Handles user input and navigation

### 2. Business Logic Layer
- Contains PageModel classes
- Processes user actions
- Communicates between UI and database

### 3. Data Access Layer
- Uses Entity Framework Core
- Maps C# models to database tables
- Handles CRUD operations

### 4. Database Layer
- SQL Server LocalDB
- Stores products and order-related data

---

## Entity Relationship Diagram (ERD)

The following diagram represents the database structure of the system:
```mermaid
erDiagram
    PRODUCT {
        int Id
        string Name
        decimal Price
        string Description
        string ImagePath
    }

    ORDER {
        int Id
        datetime OrderDate
        decimal TotalAmount
    }

    ORDER_ITEM {
        int Id
        int Quantity
        decimal Price
    }

    PRODUCT ||--o{ ORDER_ITEM : contains
    ORDER ||--o{ ORDER_ITEM : includes
