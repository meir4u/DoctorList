# Doctor List

A small full-stack application for displaying a curated list of doctors and collecting contact requests.

The backend is built with ASP.NET Core 8 and is divided into API, business-logic, and data-access projects. Doctor, article, and language information is loaded from JSON files, processed according to business rules, and exposed through a REST API.

A lightweight frontend consumes the API and presents the doctor information to users.

> **Project status:** Functional demonstration project.
> The application demonstrates layered architecture, data transformation, dependency injection, caching, validation, and frontend-to-API communication. It is not currently intended as a production medical platform.

## Overview

Doctor List retrieves doctor-related information from several JSON data sources:

* Doctors
* Sponsored articles
* Supported languages

The backend combines this information to produce a simplified doctor response containing:

* Doctor name
* Rating
* Supported languages
* Phone number
* Whether the doctor has sponsored articles

The application also provides a contact form that validates and stores contact-request data.

## Features

### Doctor Directory

* Loads doctor records from JSON files
* Returns active and paid doctors
* Orders doctors according to application rules
* Displays doctor names
* Displays average ratings
* Resolves language identifiers into readable language names
* Normalizes doctor phone numbers
* Indicates whether a doctor has sponsored articles
* Caches doctor-list responses for one minute

### Contact Form

* Accepts a full name
* Accepts an Israeli-style phone number
* Accepts an email address
* Validates request data
* Maps DTOs to persistence entities
* Stores submitted contact information
* Returns an appropriate HTTP result

### API Documentation

* Swagger/OpenAPI support in development
* REST endpoints for doctors and contact requests
* CORS configuration for local frontend development

## Technology Stack

### Backend

* C#
* .NET 8
* ASP.NET Core Web API
* Dependency injection
* Output caching
* Data annotations
* Swagger/OpenAPI
* JSON file storage

### Frontend

* HTML
* CSS
* JavaScript
* REST API communication

### Architecture

* Layered architecture
* API layer
* Business-logic layer
* Data-access layer
* DTOs
* Repository pattern
* Service abstractions

## Architecture

The application separates HTTP concerns, business rules, and data access.

```text
Browser Client
      |
      | HTTP
      v
DoctorList.Api
      |
      v
DoctorList.BL
      |
      v
DoctorList.DL
      |
      v
JSON Data Files
```

The contact flow follows a similar structure:

```text
Contact Form
      |
      v
ContactUsController
      |
      v
ContactUsService
      |
      v
ContactUsRepository
      |
      v
Stored Contact Data
```

## Solution Structure

```text
DoctorList
├── DoctorList.Api
│   ├── Controllers
│   │   ├── DoctorController.cs
│   │   └── ContactUsController.cs
│   ├── Extensions
│   ├── Program.cs
│   ├── appsettings.json
│   └── DoctorList.Api.csproj
├── DoctorList.BL
│   ├── DTOs
│   │   ├── DoctorDto.cs
│   │   └── ContactUsDto.cs
│   ├── Interface
│   │   ├── IDoctorService.cs
│   │   └── IContactUsService.cs
│   ├── Service
│   │   ├── DoctorService.cs
│   │   └── ContactUsService.cs
│   └── DoctorList.BL.csproj
├── DoctorList.DL
│   ├── Entities
│   ├── Interfaces
│   ├── Repositories
│   ├── Settings
│   └── DoctorList.DL.csproj
├── DoctorList.Client
│   ├── HTML
│   ├── CSS
│   ├── JavaScript
│   └── Assets
└── DoctorList.sln
```

The exact names of some frontend folders may differ from this simplified representation.

## Backend Projects

### DoctorList.Api

The API project is the HTTP entry point.

Its responsibilities include:

* Configuring ASP.NET Core
* Registering application services
* Enabling controllers
* Configuring CORS
* Exposing Swagger
* Applying output caching
* Returning HTTP responses

### DoctorList.BL

The business-logic project contains:

* DTOs
* Service interfaces
* Service implementations
* Doctor transformation logic
* Ordering rules
* Phone normalization
* Language resolution
* Sponsored-article detection

### DoctorList.DL

The data layer is responsible for:

* Reading JSON files
* Mapping stored data to entities
* Returning doctors
* Returning articles
* Returning language information
* Storing contact requests

## Doctor Processing

The doctor service retrieves data from several sources:

```text
Doctors
Articles
Languages
```

It then combines the records into a frontend-friendly DTO.

Example response model:

```json
{
  "fullName": "Dr. Jane Smith",
  "rating": 5,
  "supportedLanguages": [
    "English",
    "Hebrew"
  ],
  "hasArticles": true,
  "phone": "050-1234567"
}
```

## Doctor Filtering

The repository method used by the business layer returns doctors who satisfy the application's active and payment requirements.

This keeps filtering logic outside the API controller.

Conceptually:

```text
All doctors
     |
     v
Active doctors
     |
     v
Paid doctors
     |
     v
Ordered doctor list
```

## Doctor Ordering

Doctors are ordered in the business layer before being returned.

The ordering logic can consider application-specific properties such as:

* Doctor status
* Rating
* Sponsored content
* Other configured priority rules

This gives the API a stable place for applying business-defined ranking rules instead of placing them in the frontend.

## Ratings

The API returns the average doctor rating when review information exists.

When rating information is missing, the returned rating defaults to zero.

## Supported Languages

Doctor records contain language identifiers.

The service resolves those identifiers against the language data file:

```text
Doctor language IDs
        +
Language dictionary
        |
        v
Readable language names
```

Example:

```json
{
  "languageIds": ["he", "en"]
}
```

becomes:

```json
{
  "supportedLanguages": [
    "Hebrew",
    "English"
  ]
}
```

## Sponsored Articles

The application checks whether a doctor is connected to any sponsorship entry in the article data.

The API returns this as:

```json
{
  "hasArticles": true
}
```

The frontend can use this value to display a badge or icon.

## Phone Normalization

Phone numbers are cleaned by removing existing hyphens and then applying a normalized format.

Examples:

```text
031234567
```

becomes:

```text
03-1234567
```

and:

```text
0501234567
```

becomes:

```text
050-1234567
```

Numbers that do not match the expected lengths are returned as an empty value.

## API Endpoints

### Get All Doctors

```http
GET /api/Doctor/all
```

Returns the processed list of doctors.

The response is cached for 60 seconds.

Example response:

```json
[
  {
    "fullName": "Dr. Jane Smith",
    "rating": 5,
    "supportedLanguages": [
      "English",
      "Hebrew"
    ],
    "hasArticles": true,
    "phone": "050-1234567"
  }
]
```

### Submit Contact Request

```http
POST /api/ContactUs/Send
```

Example request:

```json
{
  "fullName": "Meir Achildi",
  "phoneNumber": "050-1234567",
  "email": "meir@example.com"
}
```

A successful request returns:

```http
200 OK
```

An unsuccessful request returns:

```http
400 Bad Request
```

## Contact Validation

The contact DTO uses data annotations.

### Full Name

The full name:

* Is required
* Must contain at least three characters

### Phone Number

The expected format is:

```text
XXX-XXXXXXX
```

Example:

```text
050-1234567
```

### Email Address

The email:

* Is required
* Must be a valid email format

Invalid models are automatically rejected by ASP.NET Core API-controller validation.

## Output Caching

The doctor endpoint uses output caching:

```text
Duration: 60 seconds
```

This reduces repeated file reading and business processing when multiple clients request the same doctor list within a short period.

## Data Files

The API configuration defines the paths of the JSON files.

Example configuration:

```json
{
  "DoctorFileData": {
    "DirectoryPath": "Data/",
    "DoctorsFile": "doctors.json",
    "ArticlesFile": "articles.json",
    "LanguagesFile": "language.json"
  }
}
```

The data directory must be available to the running application.

## CORS

The API includes a local-development CORS policy.

It allows configured localhost origins to:

* Send requests
* Include request headers
* Use supported HTTP methods
* Send credentials

Production environments should use an explicit list of trusted frontend origins rather than broad localhost settings.

## Running the Application

### Requirements

* .NET 8 SDK
* Visual Studio 2022, Visual Studio Code, or JetBrains Rider
* A modern web browser

No database server is required because the application uses JSON files for its primary data.

### Clone the Repository

```bash
git clone https://github.com/meir4u/DoctorList.git
cd DoctorList
```

### Restore Dependencies

```bash
dotnet restore
```

Alternatively:

```bash
dotnet restore DoctorList.sln
```

### Build the Solution

```bash
dotnet build
```

### Run the API

```bash
dotnet run --project DoctorList.Api
```

Depending on the repository folder structure, the complete path may be required:

```bash
dotnet run --project src/DoctorList.Api/DoctorList.Api.csproj
```

The development launch profiles use addresses similar to:

```text
http://localhost:5053
https://localhost:7073
```

Use the address displayed in the terminal as the source of truth.

### Open Swagger

In the Development environment, open:

```text
https://localhost:7073/swagger
```

or:

```text
http://localhost:5053/swagger
```

### Run the Client

Open the frontend project through its configured development server or serve its static files locally.

The frontend API URL must match the running backend address.

Avoid opening the HTML directly through `file:///` when browser security restrictions prevent API calls. A simple local web server is preferable.

For example, using Visual Studio Code Live Server:

1. Open the client directory.
2. Start Live Server.
3. Confirm that the generated localhost origin is included in the API CORS policy.
4. Update the frontend API base URL when necessary.

## Configuration

The application uses standard ASP.NET Core configuration sources:

* `appsettings.json`
* `appsettings.Development.json`
* Environment variables
* Command-line configuration

The JSON file paths are configured under:

```text
DoctorFileData
```

Example:

```json
{
  "DoctorFileData": {
    "DirectoryPath": "Data/",
    "DoctorsFile": "doctors.json",
    "ArticlesFile": "articles.json",
    "LanguagesFile": "language.json"
  }
}
```

## Dependency Injection

The API registers the business and data layers through extension methods.

Conceptually:

```csharp
builder.Services.AddApplicationServices(
    builder.Configuration);
```

This registration includes:

* Doctor service
* Contact service
* Doctor repository
* Contact repository
* File-data settings
* Memory caching

The API controllers depend on interfaces rather than concrete implementations.

## Example Service Flow

A doctor-list request follows this path:

```text
GET /api/Doctor/all
        |
        v
DoctorController
        |
        v
IDoctorService
        |
        v
DoctorService
        |
        v
IDoctorRepository
        |
        v
JSON files
        |
        v
DoctorDto collection
```

## What I Learned

This project helped me practice:

* Building an ASP.NET Core 8 Web API
* Dividing a solution into multiple layers
* Using dependency injection
* Using interfaces between layers
* Implementing the repository pattern
* Creating DTOs
* Applying business logic outside controllers
* Reading structured JSON data
* Combining data from multiple sources
* Normalizing phone numbers
* Resolving lookup values
* Applying output caching
* Configuring CORS
* Applying data-annotation validation
* Connecting a JavaScript frontend to a .NET API
* Exposing Swagger documentation

## Current Limitations

This project is a demonstration application and has several limitations.

### File-Based Persistence

JSON files work well for a small exercise, but they are not ideal for concurrent production workloads.

Limitations include:

* No transactions
* Limited concurrency protection
* No advanced querying
* No indexes
* Difficult schema evolution
* Risk of file corruption
* Limited audit history

### Contact Storage

Contact requests should eventually be stored in a database or sent to a durable queue.

A production workflow could:

1. Validate the request.
2. Store it in a database.
3. Create an audit record.
4. Notify an administrator.
5. Retry notification failures.

### Error Handling

The application should include centralized exception handling rather than allowing unexpected exceptions to be exposed directly.

Recommended additions include:

* Problem Details responses
* Structured logging
* Correlation IDs
* Safe error messages

### Asynchronous Contact Endpoint

The contact endpoint and repository should use asynchronous file or database operations where appropriate.

### Cancellation Tokens

Async endpoints and services should accept and propagate `CancellationToken`.

### CORS Configuration

Production CORS settings should contain only trusted deployment origins.

### Validation

Additional validation could include:

* Maximum full-name length
* Phone normalization before validation
* Email-length limit
* Duplicate contact detection
* Anti-spam protection
* Rate limiting

### Medical Disclaimer

A real doctor directory should clearly state that:

* Information may change
* Ratings require a defined methodology
* Sponsored content must be disclosed
* The directory does not provide medical advice
* Emergency cases should contact emergency services

### Testing

The repository should include:

* Unit tests for doctor ordering
* Unit tests for phone normalization
* Unit tests for language mapping
* Unit tests for article sponsorship detection
* Repository tests
* API integration tests
* Contact-validation tests
* Frontend end-to-end tests

## Recommended Improvements

### 1. Add Automated Tests

Suggested test projects:

```text
DoctorList.BL.Tests
DoctorList.DL.Tests
DoctorList.Api.IntegrationTests
DoctorList.EndToEndTests
```

Important test cases include:

```text
Returns only active and paid doctors
Orders doctors correctly
Returns zero when rating is missing
Normalizes landline phone numbers
Normalizes mobile phone numbers
Rejects malformed phone numbers
Maps language identifiers correctly
Detects sponsored articles
Rejects invalid contact requests
Caches doctor responses
```

### 2. Add Centralized Error Handling

Use ASP.NET Core exception handling and Problem Details:

```json
{
  "type": "https://example.com/errors/internal",
  "title": "An unexpected error occurred.",
  "status": 500,
  "traceId": "..."
}
```

### 3. Move to Database Storage

A future implementation could use:

* PostgreSQL
* SQL Server
* SQLite for lightweight deployments

Possible tables:

```text
Doctors
DoctorPhones
Languages
DoctorLanguages
Reviews
Articles
ArticleSponsors
ContactRequests
```

### 4. Add Filtering and Search

Possible query parameters:

```http
GET /api/doctors?language=Hebrew
GET /api/doctors?minimumRating=4
GET /api/doctors?hasArticles=true
GET /api/doctors?search=Cohen
```

### 5. Add Pagination

For a larger directory:

```http
GET /api/doctors?page=1&pageSize=20
```

### 6. Add Detailed Doctor Pages

A doctor-details response could include:

* Biography
* Specialization
* Clinic address
* Opening hours
* Languages
* Reviews
* Articles
* Appointment link

### 7. Add Observability

Add:

* Structured logging
* Health checks
* Metrics
* Distributed tracing
* Request-duration monitoring

### 8. Add CI

A GitHub Actions workflow could:

1. Restore packages
2. Build the solution
3. Run tests
4. Run formatting checks
5. Collect code coverage
6. Publish build artifacts

### 9. Add Docker Support

A simple deployment could include:

* API Dockerfile
* Static frontend container
* Reverse proxy
* Health checks
* Environment-based API URL

## Possible Future Architecture

```text
src
├── DoctorList.Api
├── DoctorList.Application
├── DoctorList.Domain
├── DoctorList.Infrastructure
├── DoctorList.Web
└── DoctorList.Worker

tests
├── DoctorList.UnitTests
├── DoctorList.IntegrationTests
└── DoctorList.EndToEndTests
```

This is a possible modernization direction and does not describe the exact current repository structure.

## Project Status

Doctor List is a compact full-stack demonstration project.

It demonstrates several useful development concepts:

* Modern .NET 8
* ASP.NET Core APIs
* Layered architecture
* Dependency injection
* Repository abstractions
* Business-layer data transformation
* File-based data access
* Output caching
* DTO validation
* CORS
* Frontend API consumption

It is suitable as a learning and code-organization example, but it requires additional security, persistence, testing, observability, and deployment work before being used in production.

## License

This project is available for educational and demonstration purposes.
