# Playwright SauceDemo Automation Framework

Automated end-to-end UI test framework for the SauceDemo application using:

- C#
- Microsoft Playwright
- NUnit
- Page Object Model (POM)

---

## Project Overview

This framework performs a complete purchase flow:

1. Login with valid credentials
2. Add products to cart
3. Remove product from cart
4. Verify cart items
5. Complete checkout
6. Verify order confirmation

---

## Project Structure

PlaywrightAssg/
│── Pages/
│ ├── LoginPage.cs
│ ├── ProductsPage.cs
│ ├── CartPage.cs
│ └── CheckoutPage.cs
│
│── Tests/
│ └── SauceDemoTest.cs
│
│── Screenshots/
│── Traces/
│── PlaywrightAssg.csproj

---

## Features

✔ Page Object Model design  
✔ Screenshot capture on failure  
✔ Trace generation on failure  
✔ Assertions using NUnit  
✔ Clean reusable methods  
✔ End-to-end workflow validation  

---

## Prerequisites

Install:

- .NET SDK 6 or later
- Visual Studio / VS Code
- Playwright browsers

Install Playwright browsers:

```bash
playwright install
