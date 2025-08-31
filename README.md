# Employee Management API

A .NET 8 Web API application built with Entity Framework Core and Oracle Database using a code-first approach. This project demonstrates modern .NET development practices with Oracle integration, including PL/SQL stored procedures and database triggers.

![Oracle](https://img.shields.io/badge/Oracle-23ai_Free-red)

## 🚀 Features

- **RESTful API** with full CRUD operations for employees and departments
- **Entity Framework Core** with Oracle provider and code-first migrations
- **PL/SQL Integration** with stored procedures and database triggers
- **Clean Architecture** with separation of concerns
- **Swagger/OpenAPI** documentation
- **Oracle Database 23ai Free** compatibility
- **Automatic Timestamps** using Oracle triggers
- **Data Validation** and error handling

## 🏗️ Architecture

```
EmployeeManagement/
├── EmployeeManagement.Api/          # Web API layer
│   ├── Controllers/                 # API controllers
│   ├── Properties/                  # Launch settings
│   └── Program.cs                   # Application entry point
├── EmployeeManagement.Data/         # Data access layer
│   ├── Context/                     # DbContext
│   ├── Entities/                    # Database entities
│   ├── Configurations/              # Entity configurations
│   ├── Migrations/                  # EF Core migrations
│   └── StoredProcedures/           # PL/SQL procedures
└── EmployeeManagement.Models/       # DTOs and models
    └── DTOs/                        # Data transfer objects
```

## 🛠️ Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Oracle Database 23ai Free](https://www.oracle.com/database/free/) or Oracle Express Edition
- [Oracle Instant Client](https://www.oracle.com/database/technologies/instant-client.html)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) or [VS Code](https://code.visualstudio.com/)

## ⚡ Quick Start

### 1. Clone the Repository
```bash
git clone https://github.com/yourusername/employee-management-api.git
cd employee-management-api
```

### 2. Install Dependencies
```bash
dotnet restore
```

### 3. Configure Database Connection
Update `appsettings.json` in the API project:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=localhost:1522/freepdb1;User Id=your_username;Password=your_password;Pooling=true;"
  }
}
```

> **Note**: Oracle 23ai Free typically runs on port **1522**, not the default 1521.

### 4. Run Database Migrations
```bash
dotnet ef database update --project EmployeeManagement.Data --startup-project EmployeeManagement.Api
```

### 5. Start the Application
```bash
dotnet run --project EmployeeManagement.Api
```

### 6. Access the API
- **Swagger UI**: `https://localhost:5001/swagger`
- **API Base**: `https://localhost:5001/api`

## 📊 Database Schema

### Tables Created:
- **DEPARTMENTS** - Department information with auto-generated IDs
- **EMPLOYEES** - Employee data with foreign key to departments

### Key Features:
- **Oracle IDENTITY columns** for auto-incrementing primary keys
- **Triggers** for automatic timestamp updates
- **Foreign key constraints** with cascading rules
- **Unique constraints** on email addresses

## 🔧 API Endpoints

### Employees
| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/employees` | Get all employees |
| GET | `/api/employees/{id}` | Get employee by ID |
| GET | `/api/employees/department/{id}` | Get employees by department |
| POST | `/api/employees` | Create new employee |
| PUT | `/api/employees/{id}` | Update employee |
| DELETE | `/api/employees/{id}` | Delete employee |

### Example Request Bodies:

**Create Employee (POST)**:
```json
{
  "firstName": "John",
  "lastName": "Doe",
  "email": "john.doe@company.com",
  "salary": 75000,
  "hireDate": "2025-01-15T00:00:00",
  "departmentId": 1
}
```

**Update Employee (PUT)**:
```json
{
  "firstName": "John",
  "lastName": "Smith",
  "email": "john.smith@company.com",
  "salary": 80000,
  "departmentId": 2
}
```

## 🗃️ Sample Data

The application seeds the database with sample departments:
- **IT** - Information Technology
- **HR** - Human Resources  
- **Finance** - Finance Department

## 🔍 PL/SQL Features

### Stored Procedures
- **GET_EMPLOYEES_BY_DEPT** - Retrieve employees by department using Oracle cursor

### Database Triggers
- **EMPLOYEE_UPDATE_TRIGGER** - Automatically sets `MODIFIED_DATE` on employee updates

### Usage Example:
```sql
-- Call stored procedure
DECLARE
    emp_cursor SYS_REFCURSOR;
BEGIN
    GET_EMPLOYEES_BY_DEPT(1, emp_cursor);
END;
```

## 🏃‍♂️ Development

### Building the Project
```bash
dotnet build
```

### Running Tests
```bash
dotnet test
```

### Adding New Migrations
```bash
dotnet ef migrations add MigrationName --project EmployeeManagement.Data --startup-project EmployeeManagement.Api
dotnet ef database update --project EmployeeManagement.Data --startup-project EmployeeManagement.Api
```

### Debugging in Visual Studio
1. Set breakpoints in your controllers
2. Press **F5** to start debugging
3. Use **Swagger UI** to test endpoints
4. Visual Studio will pause at breakpoints for inspection

## 🐳 Docker Support (Optional)

### Run Oracle with Docker:
```bash
# Pull and run Oracle Database 23ai Free
docker run -d \
  --name oracle-23ai \
  -p 1521:1521 \
  -e ORACLE_PASSWORD=yourpassword \
  gvenzl/oracle-xe:23-slim

# Update connection string to use port 1521
```

## 🛠️ Troubleshooting

### Common Issues:

**ORA-12541: Cannot connect**
- Check if Oracle services are running
- Verify the correct port (usually 1522 for Oracle 23ai Free)
- Ensure Oracle listener is started: `lsnrctl start`

**ORA-00955: Name already used**
- Drop existing tables before running migrations
- Clear migration history: `DELETE FROM "__EFMigrationsHistory"`

**Connection String Issues**
- Use `/freepdb1` for pluggable database, not `/free`
- Try both `localhost` and `127.0.0.1`
- Ensure Oracle Instant Client is installed and in PATH