graph LR
    User((Student)) -->|Uses| Browser[Web Browser]
    Browser -->|ASP.NET Core| Pages[Razor Pages]
    Pages -->|Stores Data| DB[(SQL Database)]
    Pages -->|Sends Email| Gmail[Gmail SMTP]

    erDiagram
    PRODUCT {
        int Id
        string Name
        decimal Price
    }
    CART_ITEM {
        int ProductId
        int Quantity
    }
    PRODUCT ||--o{ CART_ITEM : "added to"