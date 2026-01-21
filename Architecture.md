subgraph Application_Layer
    Browser -->|GET/POST| Razor[Razor Pages]
    Razor -->|Logic| ViewModel[Checkout/Product PageModels]
end

subgraph Data_Layer
    ViewModel -->|Entity Framework| DB[(SQL Database)]
    ViewModel -->|SMTP| Mail[Gmail Server]
end