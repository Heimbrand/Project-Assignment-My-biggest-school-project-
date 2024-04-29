# HagaDropsIt














## Endpoints: Products

### Get All Products

| Path                  | Method | Request Body | Response Body | Response Codes |
|-----------------------|--------|--------------|---------------|----------------|
| `/api/products`       | GET    | None         | List<Product> | 200            |

### Get Product By ID

| Path                      | Method | Request Body | Response Body | Response Codes |
|---------------------------|--------|--------------|---------------|----------------|
| `/api/products/{id:int}` | GET    | id: int      | Product       | 200, 404       |

### Get Products By Name

| Path                            | Method | Request Body | Response Body | Response Codes |
|---------------------------------|--------|--------------|---------------|----------------|
| `/api/products/search/{name}` | GET    | name: string | List<Product> | 200, 404       |

### Add Product

| Path                  | Method | Request Body | Response Body | Response Codes |
|-----------------------|--------|--------------|---------------|----------------|
| `/api/products`       | POST   | Product      | Product       | 200, 400       |

### Update Product

| Path                  | Method | Request Body | Response Body | Response Codes |
|-----------------------|--------|--------------|---------------|----------------|
| `/api/products`       | PUT    | Product      | Product       | 200, 400, 404 |

### Delete Product

| Path                      | Method | Request Body | Response Body | Response Codes |
|---------------------------|--------|--------------|---------------|----------------|
| `/api/products/{id:int}` | DELETE | id: int      | None          | 200, 404       |


## Endpoints: Orders

### Get All Orders

| Path                   | Method | Request Body | Response Body | Response Codes |
|------------------------|--------|--------------|---------------|----------------|
| `/api/orders`          | GET    | None         | List<Order>   | 200, 404       |

### Get Order By ID

| Path                        | Method | Request Body | Response Body | Response Codes |
|-----------------------------|--------|--------------|---------------|----------------|
| `/api/orders/{id:ObjectId}` | GET    | id: ObjectId | Order         | 200, 404       |

### Get Order By Order Number

| Path                                    | Method | Request Body    | Response Body | Response Codes |
|-----------------------------------------|--------|-----------------|---------------|----------------|
| `/api/orders/ordernumber/{ordernumber}` | GET    | ordernumber: string | Order      | 200, 404       |

### Get Orders By Customer GUID

| Path                                       | Method | Request Body       | Response Body | Response Codes |
|--------------------------------------------|--------|--------------------|---------------|----------------|
| `/api/orders/customer/{customerGuid:Guid}` | GET    | customerGuid: Guid | List<Order>   | 200, 404       |

### Get Orders By Buyer Email

| Path                                | Method | Request Body   | Response Body | Response Codes |
|-------------------------------------|--------|----------------|---------------|----------------|
| `/api/orders/buyeremail/{buyerEmail}` | GET    | buyerEmail: string | List<Order> | 200, 404       |

### Create Order

| Path                   | Method | Request Body | Response Body | Response Codes |
|------------------------|--------|--------------|---------------|----------------|
| `/api/orders`          | POST   | Order        | Order         | 201, 400       |

### Delete Order

| Path                        | Method | Request Body | Response Body | Response Codes |
|-----------------------------|--------|--------------|---------------|----------------|
| `/api/orders/{id:ObjectId}` | DELETE | id: ObjectId | None          | 204, 404       |


## Endpoints: Events

### Get All Events

| Path            | Method | Request Body | Response Body | Response Codes |
|-----------------|--------|--------------|---------------|----------------|
| `/api/events`   | GET    | None         | List<Event>   | 200            |

### Get Event By ID

| Path                  | Method | Request Body | Response Body | Response Codes |
|-----------------------|--------|--------------|---------------|----------------|
| `/api/events/{id:int}` | GET    | id: int      | Event         | 200, 404       |

### Get Event By Name

| Path                    | Method | Request Body | Response Body | Response Codes |
|-------------------------|--------|--------------|---------------|----------------|
| `/api/events/search/{name}` | GET    | name: string | Event         | 200, 404       |

### Add Event

| Path            | Method | Request Body | Response Body | Response Codes |
|-----------------|--------|--------------|---------------|----------------|
| `/api/events`   | POST   | Event        | Event         | 200, 400       |

### Update Event

| Path                  | Method | Request Body | Response Body | Response Codes |
|-----------------------|--------|--------------|---------------|----------------|
| `/api/events`         | PUT    | Event        | Event         | 200, 400       |

### Delete Event

| Path                  | Method | Request Body | Response Body | Response Codes |
|-----------------------|--------|--------------|---------------|----------------|
| `/api/events/{id:int}` | DELETE | id: int      | None          | 200, 404       |
