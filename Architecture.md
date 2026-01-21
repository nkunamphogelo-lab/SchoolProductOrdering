subgraph Application_Layer
    Browser -->|GET/POST| Razor[Razor Pages]
    Razor -->|Logic| ViewModel[Checkout/Product PageModels]
end

subgraph Data_Layer
    ViewModel -->|Entity Framework| DB[(SQL Database)]
    ViewModel -->|SMTP| Mail[Gmail Server]
end

PRODUCT {
    int Id PK "Primary Key"
    string Name "Product Name"
    string Description "Details"
    decimal Price "Cost"
    string ImagePath "Url to image"
}

CART_ITEM {
    int ProductId FK "Foreign Key to Product"
    string ProductName "Display Name"
    decimal Price "Price at time of add"
    int Quantity "Number of items"
}

U->>C: Clicks "Complete Order"
C->>C: Validate Form (Name, Email, etc.)
C->>S: Clear "CartItems" & "CartCount"
C->>E: SendRealEmail(userEmail)
E-->>U: Deliver Confirmation Email
C->>U: Redirect to OrderSuccess