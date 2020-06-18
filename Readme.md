
A single page application (SPA) using Angular9, .Net Core Web API, EF Core 6 and MySQL, deployed using docker.

## Solution

Implemented two tabs (Invited and the Accepted) using Material Design components for Angular. 
- Invited tab displays all leads in the new status. 
- Accepted tab displays all leads in the accepted status.
- On clicking the 'Accept' button in the the Invited tab, the lead moves to Accepted tab.
- On clicking the 'Decline' button in the the Invited tab, the lead gets deleted.

### Features implemented
1. CQRS using MediatR. Simpler implementation using a single database as the command sink and the query sink.
2. Following DDD principles for business logic (Domain Driven Design)

### Design

- Mediator pattern is used to handle the API requests at the controller. For each request, an handler is created, which is resolved to process the request. This patterns supports the CQRS pattern. The command and the query handlers are mediated using the Mediator pattern.
- CQRS pattern uses request/response, commands, queries, notifications and events. 
  - Accept and decline invitation requests are handled as commands. Command objects encapsulate the data for the request, and command handler processes the request.
  - Get all accepted and invited leads requests are handled as queries. Query objects encapsulate the data for the request and query handler process the request.
- Repository pattern is used to persist the data. The lead repository is designed handle domain specific requests, instead of generic functions.
- The design encapsulated the DDD principles in the implementation.

#### Code structure

- WebAPI
  - ClientApp - Angular code base
  - Controllers
- Domain
  - Leads - Contain the logic to handle the lead request/responses
    - Commands - Command objects. Accept and decline invitation command onject
    - EventHandlers - Event Handlers. Email notification handler
    - Events - Event objects. Email notification event
    - Handers - Command and Query handlers
    - Models - Data models
    - Queries - Query objects. Get all accepted and invited leads query object
    - Responses - Response Objects for the command/query handler
  - Repository - Definitions for the interfaces to implement the repository
  - ServiceHandlers - Contains the interface for external service handlers. Ex: email handler
- Domain.Tests - Tests to validate the command/query handlers
- Infrastructure 
  - Repository - Contains the concrete implementations of the repositories defined in the domain
  - Service Handlers - Contains the concrete implementation of the service handlers defined in the domain
- Infrastructure.Tests - Tests the validate the repository and service handler implementation
- Data - Entity models and the DbContext

#### Tech-Stack
1. Angular 9 for the front-end using angular material library for the tab design
2. ASP.Net Core 3.0 for the back-end, with MediatR library for handling CQRS pattern, Serilog for logging
3. Entity Framework 6 as the ORM
4. MySQL as the database
5. Docker
6. Used the provided boiler plate as the base

### Execution steps
1. Copy the contents to a folder 'TechChallenge'
2. Copy the certificate file to the 'cert' folder. 
   - Default settings - certificate file should be 'cert-aspnetcore.pfx' with password 'hipages'
   - Custom settings - Update the docker file "docker-compose.override.yml" with the certificate name and password.
     - ASPNETCORE_Kestrel__Certificates__Default__Path=/root/.dotnet/https/<certificate_file_name>
     - ASPNETCORE_Kestrel__Certificates__Default__Password=<password_for_the_certificate>
2. Within the folder execute 
   - 'docker-compose up'
3. In the browser, open 'https://localhost:8091/dashboard'. (Sometimes, page not found may error, give in a minute and click refresh)
