# Ecommerce Application

Full Stack Ecommerce Application built using ASP.NET Core Web API and React.

## Features

### Authentication
- User Registration
- Admin Registration
- JWT Login
- Protected Routes

### Product Management
- Create Product
- View Products
- Update Product
- Delete Product
- Update Stock
- Bulk Product Upload

### Orders
- Create Order
- Checkout Order
- View Orders
- Cancel Order
- Update Order Status

### Payments
- Create Payment
- Get Payment Details

## Tech Stack

### Backend
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- JWT Authentication
- Repository Pattern
- Service Layer
- Swagger

### Frontend
- React
- React Router
- Axios

## Project Structure

```text
Ecommerce/
├── Ecommerce/              Backend API
├── Ecommerce.UI/           React Frontend
├── Ecommerce.UnitTests/    Unit Tests
```

## Run Backend

```bash
dotnet restore
dotnet build
dotnet run
```

Backend URL:

```text
https://localhost:7064
```

## Run Frontend

```bash
cd Ecommerce.UI
npm install
npm run dev
```

Frontend URL:

```text
http://localhost:5173
```

## Current Status

- Authentication Complete
- Product Listing Complete
- Frontend Integration In Progress
- Cart & Checkout In Progress

## Author

Sunaila Amin
