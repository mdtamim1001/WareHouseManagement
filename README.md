⚠️ This project is currently under active development.

---

## 🛠 Tech Stack

- **Framework:** ASP.NET (C#)
- **Architecture:** Web API
- **Database:** Microsoft SQL Server (MSSQL)
- **API Style:** RESTful

---

## ⚙️ Architecture Overview

- Implements a **3-Tier Architecture**:
  - **Presentation Layer**: Handles HTTP requests and responses
  - **Business Logic Layer**: Core logic and service rules
  - **Data Access Layer (DAL)**: Handles database interaction
- Applies **SOLID Principles** for maintainable and scalable backend design
- Follows **RESTful design** for API endpoints

---
## ✅ What’s Already Done in This Project

### 🔧 RESTful API Development
- Built using **ASP.NET Framework Web API** with clean routing and HTTP method conventions.
  
### 📦 Full CRUD Functionality
- Create, Read, Update, and Delete operations for Products, Shipments, Sections, Managers

### 📁 File Handling & Export
- Generates and downloads section-wise product reports as `.txt` files via a dedicated API endpoint.

### ⚠️ Expiry Alert System
- Identifies and returns products expiring within a specified number of days.

### ✉️ SMTP Email Notifications
- Sends automated emails to a fixed recipient when a new shipment is created.

### 🔁 Entity Relationships
- Modeled and implemented:
  - **One-to-many** (e.g., Section ↔ Product)
  - **Many-to-many** (e.g., Shipment ↔ Product) relationships.

### ✅ Input Validation & Exception Handling
- Uses **Data Annotations** for validation.
- Implements **try-catch blocks** for robust error handling.
