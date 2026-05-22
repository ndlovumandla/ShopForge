# ShopForge

Full-stack C# ecommerce platform with an ASP.NET Core API, Blazor admin dashboard, SQL Server persistence, and .NET MAUI mobile client.

ShopForge is built as a portfolio-grade commerce system that demonstrates end-to-end .NET engineering: clean API design, EF Core data modeling, role-based admin workflows, responsive Blazor UI, JWT authentication, reporting dashboards, image uploads, testing, and mobile-ready architecture.

![.NET](https://img.shields.io/badge/.NET-10-512BD4)
![ASP.NET Core](https://img.shields.io/badge/API-ASP.NET%20Core-68217A)
![Blazor](https://img.shields.io/badge/Admin-Blazor%20Server-5C2D91)
![MAUI](https://img.shields.io/badge/Mobile-.NET%20MAUI-512BD4)
![SQL Server](https://img.shields.io/badge/Database-SQL%20Server-CC2927)
![Tests](https://img.shields.io/badge/Tests-xUnit-2F7D32)

## Table of Contents

- [Overview](#overview)
- [Screenshots](#screenshots)
- [Core Features](#core-features)
- [Frameworks and Libraries](#frameworks-and-libraries)
- [Architecture](#architecture)
- [Project Structure](#project-structure)
- [Admin Dashboard](#admin-dashboard)
- [API Capabilities](#api-capabilities)
- [Data Model](#data-model)
- [Security](#security)
- [Getting Started](#getting-started)
- [Demo Accounts](#demo-accounts)
- [Testing](#testing)
- [Engineering Highlights](#engineering-highlights)
- [Roadmap](#roadmap)

## Overview

ShopForge models a practical ecommerce business from storefront operations through back-office administration. The platform includes customer authentication, product catalog management, cart and wishlist flows, checkout, mock payment handling, order tracking, inventory reporting, promotional coupons, banner management, and executive-style analytics.

The project is intentionally structured as a multi-project .NET solution to show how a production system can share contracts across API, admin, and mobile clients while keeping persistence, business services, and presentation concerns separated.

## Screenshots

The screenshots below are generated from the running Blazor admin portal and show the current navigation, product management, and mobile-responsive layout.

| Admin Dashboard | Product Management |
| --- | --- |
| ![ShopForge admin dashboard](docs/screenshots/admin-dashboard.png) | ![ShopForge product management](docs/screenshots/admin-products.png) |

| Add Product Workflow | Mobile Admin Layout |
| --- | --- |
| ![ShopForge add product form](docs/screenshots/admin-product-form.png) | ![ShopForge mobile admin dashboard](docs/screenshots/admin-dashboard-mobile.png) |

| Sales Analytics | Revenue Mix |
| --- | --- |
| ![ShopForge sales report](docs/screenshots/admin-report-sales.png) | ![ShopForge revenue report](docs/screenshots/admin-report-revenue.png) |

| Top Products | Inventory Report |
| --- | --- |
| ![ShopForge top products report](docs/screenshots/admin-report-top-products.png) | ![ShopForge inventory report](docs/screenshots/admin-report-inventory.png) |

| Mobile Sales Report |
| --- |
| ![ShopForge mobile sales report](docs/screenshots/admin-report-sales-mobile.png) |

| Customer Sign In | Customer Register |
| --- | --- |
| ![ShopForge customer sign in](docs/screenshots/customer-mobile-home.png) | ![ShopForge customer registration](docs/screenshots/customer-mobile-register.png) |

| Customer Home | Customer Shop |
| --- | --- |
| ![ShopForge customer home](docs/screenshots/customer-mobile-authenticated.png) | ![ShopForge customer shop](docs/screenshots/customer-mobile-shop.png) |

| Customer Cart | Checkout Address |
| --- | --- |
| ![ShopForge customer cart](docs/screenshots/customer-mobile-cart.png) | ![ShopForge checkout address](docs/screenshots/customer-mobile-checkout.png) |

| Checkout Payment |
| --- |
| ![ShopForge checkout payment](docs/screenshots/customer-mobile-payment.png) |

The admin reporting filters default to `1 January 2021` through today so the dashboard loads the full available demo history immediately.

To refresh the browser-based admin images locally:

```bash
npm install --no-save playwright
node scripts/capture-screenshots.js
```

Customer images are captured from the Windows .NET MAUI app after running `dotnet run --project src/ShopForge.Mobile`.

## Core Features

- Customer and admin authentication with JWT access tokens and refresh tokens.
- Role-aware admin portal for Admin and Manager users.
- Product catalog with categories, brands, variants, product images, stock status, pricing, and search/filter support.
- Local product image upload from the admin product form with server-side file validation.
- Cart, wishlist, address book, checkout, mock payment processing, orders, and order status history.
- Admin modules for products, categories, orders, customers, coupons, banners, settings, and profile management.
- Reporting dashboards for sales, revenue by category, top products, and inventory health.
- SignalR-ready notification channel for order and operational updates.
- Mobile client foundation built with .NET MAUI and MVVM patterns.
- Unit and integration test coverage for payment, coupon, cart, and API service behavior.

## Frameworks and Libraries

| Area | Technology |
| --- | --- |
| Backend API | ASP.NET Core 10 Web API |
| Admin UI | Blazor Server, Razor Components, MudBlazor |
| Mobile | .NET MAUI 10, CommunityToolkit.Maui, CommunityToolkit.Mvvm |
| Database | SQL Server, Entity Framework Core |
| Authentication | JWT Bearer authentication, refresh tokens, BCrypt password hashing |
| Validation | FluentValidation |
| Realtime | ASP.NET Core SignalR |
| Logging | Serilog console and file sinks |
| API Documentation | Swagger / OpenAPI |
| Testing | xUnit, FluentAssertions, Moq, ASP.NET Core TestHost |
| DevOps | Docker Compose, GitHub CLI compatible workflow |

## Architecture

```text
.NET MAUI Mobile App
        |
        | HTTPS / JSON DTOs
        v
ASP.NET Core Web API  ---- SignalR ---- Admin notifications
        |
        | EF Core repositories and services
        v
SQL Server

Blazor Server Admin Portal
        |
        | Typed HttpClient services
        v
ASP.NET Core Web API
```

The solution uses shared DTOs and enums in `ShopForge.Shared`, database entities and EF Core configuration in `ShopForge.Database`, business workflows in API services, and client-specific presentation in the Blazor admin and MAUI mobile projects.

## Project Structure

```text
ShopForge/
├── src/
│   ├── ShopForge.Api/          ASP.NET Core API, controllers, services, validation
│   ├── ShopForge.Admin/        Blazor Server admin dashboard and workflows
│   ├── ShopForge.Mobile/       .NET MAUI mobile client
│   ├── ShopForge.Database/     EF Core DbContext, entities, migrations, seed data
│   └── ShopForge.Shared/       Shared DTOs, enums, constants, contracts
├── tests/
│   ├── ShopForge.Api.Tests/    API and service tests
│   └── ShopForge.Mobile.Tests/ Mobile view model and calculation tests
├── scripts/
│   └── capture-screenshots.js  Playwright screenshot automation for README assets
├── docker-compose.yml
└── ShopForge.slnx
```

## Admin Dashboard

The Blazor admin portal is designed around fast ecommerce operations:

- Dashboard KPIs for revenue, order volume, product performance, and business health.
- Product list and add/edit workflow with pricing, SKU, inventory, category, status, image URL, and local image upload.
- Order management with customer details and order lifecycle visibility.
- Customer, category, coupon, banner, and settings management.
- Reporting pages with executive summaries and chart-ready datasets.
- Responsive shell with sidebar navigation, touch-friendly controls, and mobile layout support.

## Customer Journey

The customer side is implemented in the .NET MAUI mobile app. The full buying workflow is represented by dedicated screens and view models:

| Step | Screen | What it demonstrates |
| --- | --- | --- |
| 1 | Register | Customer account creation and form validation |
| 2 | Login | Authenticated customer session flow |
| 3 | Home | Featured products and storefront entry point |
| 4 | Shop and Search | Product browsing, category filtering, and search |
| 5 | Product Detail | Product information, pricing, reviews, and cart actions |
| 6 | Cart | Quantity updates, totals, coupon-ready calculations |
| 7 | Checkout Address | Shipping destination selection |
| 8 | Checkout Shipping | Delivery method selection |
| 9 | Checkout Payment | Mock card payment flow with Luhn validation |
| 10 | Checkout Confirmation | Order review and placement |
| 11 | Orders | Post-purchase order history and detail tracking |

This end-to-end flow shows the platform from both sides of the business: customers can register, browse, buy, and track orders, while admins can manage catalog, inventory, orders, promotions, reporting, and store settings.

## API Capabilities

Key endpoint groups include:

| Area | Example endpoints |
| --- | --- |
| Auth | `POST /api/auth/login`, `POST /api/auth/register`, `POST /api/auth/refresh` |
| Products | `GET /api/products`, `GET /api/products/{id}` |
| Cart | `GET /api/cart`, `POST /api/cart/items`, `DELETE /api/cart/items/{id}` |
| Orders | `POST /api/orders`, `GET /api/orders/{id}` |
| Payments | `POST /api/payments/process` |
| Admin Products | `GET /api/admin/products`, `POST /api/admin/products`, `PUT /api/admin/products/{id}` |
| Admin Uploads | `POST /api/admin/uploads/product-images` |
| Reports | `GET /api/admin/reports/dashboard`, sales, revenue, top product, and inventory endpoints |

Swagger is available at `http://localhost:5002/swagger` in development.

## Data Model

ShopForge includes a commerce-oriented domain model with entities for users, refresh tokens, products, product images, variants, attributes, brands, categories, carts, cart items, wishlists, orders, order items, payments, shipping methods, coupons, reviews, notifications, audit logs, settings, inventory logs, and banner slides.

The model is designed for realistic ecommerce growth: product media is separated from product metadata, inventory changes can be tracked, order status history is retained, and admin/reporting workflows can be built without flattening the domain too early.

## Security

- Passwords are hashed with BCrypt.
- JWT Bearer authentication protects customer and admin API routes.
- Refresh tokens support session renewal.
- Admin APIs are protected by role policies.
- Product image uploads validate content type, extension, and maximum file size.
- Production secrets are expected through environment variables, user secrets, or a managed secret store.

## Getting Started

### Prerequisites

- .NET 10 SDK
- SQL Server LocalDB or Docker Desktop
- Visual Studio 2022 with the .NET MAUI workload for mobile development
- Node.js if you want to regenerate README screenshots

### Run with LocalDB

```bash
dotnet restore ShopForge.slnx
dotnet ef database update --project src/ShopForge.Database --startup-project src/ShopForge.Api
dotnet run --project src/ShopForge.Api
dotnet run --project src/ShopForge.Admin
```

Local URLs:

| Service | URL |
| --- | --- |
| API | `http://localhost:5002` |
| Swagger | `http://localhost:5002/swagger` |
| Admin portal | `http://localhost:5089` |

### Run with Docker

```bash
docker-compose up -d
```

Docker Compose starts SQL Server and the API with seed data.

## Demo Accounts

| Role | Email | Password |
| --- | --- | --- |
| Admin | `admin@shopforge.co.za` | `Admin@123` |
| Manager | `manager@shopforge.co.za` | `Manager@123` |
| Customer | `customer003@example.com` | `Customer@123` |
| Customer | `customer004@example.com` | `Customer@123` |

## Demo Payment Cards

| Card Number | Brand | Result |
| --- | --- | --- |
| `4111 1111 1111 0001` | Visa | Succeeds |
| `5500 0055 5555 5559` | Mastercard | Succeeds |
| `4111 1111 1111 0002` | Visa | Insufficient funds |
| `4111 1111 1111 0003` | Visa | Card expired |

Use any future expiry date and any 3-digit CVV.

## Testing

```bash
dotnet test ShopForge.slnx
```

The test projects cover API services, coupon logic, payment scenarios, cart calculations, and mobile view model behavior.

## Engineering Highlights

- Multi-project .NET solution with shared contracts and clear boundaries.
- Typed service layer between Blazor admin and API.
- EF Core-backed persistence with seeded demo data.
- Portfolio-friendly admin UX with analytics, product operations, and responsive navigation.
- Server-side image upload pipeline suitable for extension to cloud storage.
- Automated screenshot script that keeps README visuals aligned with the actual UI.
- Docker-based local infrastructure path for repeatable development.

## Roadmap

- Add CI with build, test, and format checks on pull requests.
- Add cloud object storage for product images.
- Add production payment provider integration.
- Expand reporting into forecast-ready time-series analytics.
- Add admin audit log screens and exportable CSV/PDF reports.
- Add end-to-end browser tests for critical admin and checkout flows.
