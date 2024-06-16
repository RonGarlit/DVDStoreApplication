# DVDStoreApplication

The DVDStoreApplication is a project designed to manage a DVD store using ASP.NET Core MVC. The solution is structured to provide a clear separation of concerns and includes multiple projects for data access, web application, and unit tests.

## Project Structure

The solution consists of the following projects:

### DVDStore.DAL
This project contains the data access layer (DAL) components.

- **Context**: Contains the database context classes.
- **Maps**: Holds the entity mapping configurations.
- **Models**: Includes the data models representing the database entities.

### DVDStore.DAL.LiveDbTests
This project includes unit tests for the data access layer, repositories, and controllers using a live database.

### DVDStore.DAL.MockedUnitTests
This project includes unit tests for the data access layer, repositories, and controllers using mocked data.

### DVDStore.Web.MVC
This is the main web application project built with ASP.NET Core MVC.

- **Areas**: Divides the application into functional areas:
  - **FilmCatalog**
    - **Common**: Shared resources and utilities.
    - **Controllers**: MVC controllers for the FilmCatalog area.
    - **Models**: View models specific to the FilmCatalog area.
    - **Repositories**: Data repositories for accessing film-related data.
    - **Views**
      - **FilmsCatalog**: Views related to the film catalog.
  - **Identity**
    - **Data**: Identity-related data models and context.
    - **Pages**
      - **Account**
        - **Manage**: Pages for managing user accounts.
    - **Services**: Identity-related services.
  - **Security**
    - **Common**: Shared security resources and utilities.
    - **Controllers**: MVC controllers for the Security area.
    - **Models**: Security-related view models.
    - **Repositories**: Data repositories for accessing security-related data.
    - **Views**
      - **User**: Views related to user security.
  - **Store**
    - **Common**: Shared resources and utilities for the Store area.
    - **Controllers**: MVC controllers for the Store area.
    - **Models**: View models specific to the Store area.
    - **Repositories**: Data repositories for accessing store-related data.
    - **Views**
      - **Store**: Views related to the store.

- **Common**
  - **Exceptions**: Custom exception classes.
  - **Extensions**: Extension methods.
  - **InduceProblems**: Classes for simulating problems for testing.
  - **Paging**: Paging utilities and classes.
  - **PropertyMapping**: Utilities for property mapping.
  - **ResourceParameters**: Parameter classes for API resources.

- **Controllers**: Main MVC controllers for the web application.
- **Migrations**
  - **SqlServerMigrations**: Entity Framework migrations for SQL Server.
- **Models**: Main application view models.
- **obj**: Build artifacts and temporary files.
- **Properties**: Project properties and settings.
- **Views**
  - **Home**: Views for the home page.
  - **Shared**: Shared views (e.g., layout, error pages).
- **wwwroot**: Static web assets (e.g., CSS, JavaScript, images).

## Functionality

The application is designed to manage various aspects of a DVD store, including:

- Film catalog management
- User identity and security
- Store operations

## Testing

The solution includes comprehensive unit tests to ensure the reliability and correctness of the code. There are separate projects for live database tests and mocked unit tests, covering the data access layer, repositories, and controllers.

## Getting Started

To get started with the DVDStoreApplication, clone the repository and open the solution in Visual Studio or your preferred IDE. Ensure you have the necessary dependencies installed and update the database connection strings as needed.  

**Read these two documents first!!!**  
You can find these in the folder named **Notes and Scripts**. 

"001-GettingSetUp.docx" and "002-BuildDVDStoreDatabase.docx".

## Contributing

Contributions to the project are welcome. Please follow the standard guidelines for pull requests and code reviews.

## License

This project is licensed under the MIT License.

