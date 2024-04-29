# HagaDropsIt

##
This is a repository containing the large project in which up to 10 team members worked together in an agile work enviroment, utilizing SCRUM to produce a fully functional Web store.
The team consisted of: 1 product owner, 1 scrum master, 3 .NET fullstack developers, 3 cloud devops and finally 2 testers.
I was one of the .NET fullstack developers.
---
I have edited a large, if not all aspects of cloud integration, so now the databases are only working locally. I also removed an emailSender and BlobStorage. Reason being i wish to showcase an honest representation of
my competenses, not others. What you can see in this project now, is implementation that either i did myself, or had a part in. 
The only aspect in which i take a step back when it comes to taking into account for my involvement is the design, i had very little part in that, however removing the design would be a rather large undertaking.
---
# Current state of project:
Work in progress to restructure the database. Currently i am experiencing some unwanted behaviour with some of the Foreign Keys with some of the SQL entities. 
I have thought of a solution to implement, but have not yet had the time. 

Currently the Cart is part of one of the SQL databases, and it has a Foreign Key to the ApplicationUser. This creates problems when implementing Cart usage to users who are not logged in. 
The cart also has Foreign Keyes to the stores products and Events. This creates a problem when you wish to perform CRUD operations to the Products and Events. The Database won't allow it if there are Products or Events in a Cart during that time, because said Foreign Keyes.

Solution: Break out Cart from the SQL database where Products and Events are. Wether i decide to make the Cart a Collection in the MongoDb or another Sql db is undecided.


---
## Endpoints: Products (Not Up To Date)

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
