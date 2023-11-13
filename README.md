# Hacker News API

This is a simple RESTful API built with ASP.NET Core that retrieves details of the "best stories" from the Hacker News API.

## Overview

The API provides an endpoint to fetch the details of the first n "best stories" from the Hacker News API, sorted by their score in descending order.

## Installation

1. Accessing the code
 ```bash
git clone https://github.com/crmd-Gft/Santander-Coding-Test.git
cd HackerNewsApi
```

2. Running the application 
 ```bash
dotnet build
dotnet run
```

The API will be available at http://localhost:7129. This is configurable in Properties/launchSettings.json. You can test it using tools like Postman or curl.

## Usage

### Get Best Stories
Endpoint: GET /api/hackernews/best-stories

Parameters:

- n (required): The number of best stories to retrieve.

Example:

```bash
curl -X GET "http://localhost:7129/api/hackernews/best-stories?n=5"
```

Response:

```json
[
  {
    "title": "A uBlock Origin update was rejected from the Chrome Web Store",
    "uri": "https://github.com/uBlockOrigin/uBlock-issues/issues/745",
    "postedBy": "ismaildonmez",
    "time": "2019-10-12T13:43:01+00:00",
    "score": 1716,
    "commentCount": 572
  },
  // ... other stories ...
]
```

## Asumptions and Enhancements 
1. **Authentication**
    - The code does not include any form of authentication or authorization mechanisms. In a production environment, one would likely need to implement proper authentication to secure this API.
2. **Error Handling**
    - Basic error handling has been added to catch exceptions during HTTP requests. However, the code does not include detailed logging or a comprehensive error response strategy, nor does it differentiate between different types of exceptions. In a production environment, you would want to implement more robust error handling and logging mechanisms.
3. **Input Validation**
    - The code assumes that the input parameter n in the `GetBestStories` action method is a positive integer. It does not perform extensive validation on the input parameters. In a production environment, the endpoint should validate and sanitize input parameters to ensure they meet the expected criteria.
4. **Dependency Injection**
    - The code uses dependency injection for the `HackerNewsService` but assumes that the required configuration for the `HttpClient` is automatically handled by the framework. In a production environment, one may need to configure the HttpClient with appropriate settings, such as timeout values and connection pooling.
5. **Testing**
   -  The code does not include unit tests or integration tests. In a production environment, one should implement thorough testing to ensure the reliability and correctness of an API, including e2e testing.
6. **Datetime Deserialisation**
   - Datetimes are assumed to always be in Unix epoch format and so the `UnixDateTimeConverter` class is used as to deserialise datetimes in the `Story` model.
7. **Naming Contiuity**
   - Naming is assumed to be consistent when deserialising so that a map can be formed allowing `JsonProperty` decorators to be used in the `Story` model.
8. **Concurrency**
   - The code uses asynchronous processing to improve concurrency, but it may not handle large-scale concurrency scenarios optimally. Depending on the expected load and scalability requirements, more concurrency control options can be implemented, like caching, introducing rate limiting or connection pooling.







