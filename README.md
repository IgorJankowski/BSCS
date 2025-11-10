# Backend Shopping Cart Service E-Commerce API

## Design assumptions

"3.) view the contents of the cart"
"4.) display a summary of the current order"
I Assumed that these are logically the same, since cart includes totalItems & totalPrice

- to get the list of all products from ext api
GET /api/products 

- to get details of products
GET /api/products/{id} 

- to browse through products
GET /api/products/search

- to get all categories (just to know what to put into search ;-;)
GET /api/products/categories

### View the contents of the cart

-to see some content
POST /api/cart/add/{productId}

-to unsee something to be unseen (it can't really be unseen)
DELETE /api/cart/remove/{productId}

-to update something (to buy more, should include validation to put more when user tries to reduce the quantity)
PUT /api/cart/update/{productId}

GET /api/cart

### Display a summary of the current order

GET /api/cart/summary

### Design Patterns Used
- **Dependency Injection**: Loose coupling through interfaces (Program.cs configuration)
- **Service Layer Pattern**: Business logic separation from controllers
- **Singleton Pattern**: Cart persistence across application lifetime
- **API Client/Adapter Pattern**: External API integration via ProductApiClient

## Quick Start

### Prerequisites
- .NET 8 SDK or later

### Installation & Running

1. Clone the repository:
```bash
git clone <repository-url>
cd BSCS
```

2. Build the project:
```bash
dotnet build
```

3. Run the application:
```bash
dotnet run
```

4. Open your browser and navigate to:
```
https://localhost:7xxx/swagger
```

The exact port will be shown in the console output.