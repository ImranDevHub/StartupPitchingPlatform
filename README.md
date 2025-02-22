![project sample](https://github.com/user-attachments/assets/97750ed3-f841-41ea-8eed-c36133f1deae)
---

# Startup Pitching Platform

The **Startup Pitching Platform** is an ASP.NET Core MVC web application that allows users to create, share, and explore startup ideas. Users can register, log in, create posts, and search for posts by category or keyword.

---

## Table of Contents

1. [Features](#features)
2. [Prerequisites](#prerequisites)
3. [Setup Instructions](#setup-instructions)
4. [Running the Application with Docker](#running-the-application-with-docker)
5. [Project Structure](#project-structure)
6. [Database Configuration](#database-configuration)
7. [Testing](#testing)
8. [Contributing](#contributing)
9. [License](#license)

---

## Features

- **User Authentication**: Register, log in, and manage user accounts.
- **Create Posts**: Users can create posts with a title, description, category, image URL, and social links (GitHub, LinkedIn).
- **Edit and Delete Posts**: Users can edit or delete their own posts.
- **Search Posts**: Search for posts by title or description.
- **View Posts**: Browse all posts or filter by category.
- **Responsive Design**: Built with Bootstrap for a mobile-friendly experience.

---

## Prerequisites

Before setting up the project, ensure you have the following installed:

1. **Docker**:
   - Download and install from [Docker Desktop](https://www.docker.com/products/docker-desktop).

2. **.NET SDK** (optional, for local development without Docker):
   - Download and install from [.NET Downloads](https://dotnet.microsoft.com/download).

3. **Git** (optional):
   - Download and install from [Git Downloads](https://git-scm.com/downloads).

---

## Setup Instructions

### 1. Clone the Repository

Clone the repository to your local machine:

```bash
git clone https://github.com/your-username/startup-pitching-platform.git
cd startup-pitching-platform
```

### 2. Build and Run with Docker

1. **Build the Docker Image**:
   Run the following command to build the Docker image for the application:

   ```bash
   docker-compose build
   ```

2. **Run the Application**:
   Start the application and database using Docker Compose:

   ```bash
   docker-compose up
   ```

   The application will be available at `http://localhost:5000` (or `https://localhost:5001` for HTTPS).

3. **Apply Database Migrations**:
   Run the following command to apply database migrations:

   ```bash
   docker-compose run web dotnet ef database update
   ```

---

## Running the Application with Docker

1. **Home Page**:
   - Browse all startup posts.
   - Use the search bar to find posts by title or description.

2. **Register/Login**:
   - Click "Register" to create a new account or "Login" to access your account.

3. **Create a Post**:
   - After logging in, click "Create New Post" to add a new startup idea.

4. **Edit/Delete Posts**:
   - Only the author of a post can edit or delete it.

5. **Search Posts**:
   - Use the search bar to filter posts by keyword.

---

## Project Structure

The project is organized as follows:

```
StartupPitchingPlatform/
├── Controllers/          # MVC controllers
├── Data/                # Database context and migrations
├── Models/              # Data models (e.g., StartupPost)
├── Views/               # Razor views
├── wwwroot/             # Static files (CSS, JS, images)
├── Dockerfile           # Docker configuration for the app
├── docker-compose.yml   # Docker Compose configuration
├── Program.cs           # Application entry point
├── appsettings.json     # Configuration file
└── README.md            # Project documentation
```

---

## Database Configuration

The application uses **Entity Framework Core** with **SQL Server** for data storage. The database schema is defined in the `Models` folder and applied using migrations.

### Docker Compose Configuration

The `docker-compose.yml` file defines two services:

1. **web**: The ASP.NET Core application.
2. **db**: A SQL Server database container.

Example `docker-compose.yml`:

```yaml
version: '3.8'

services:
  db:
    image: mcr.microsoft.com/mssql/server:2019-latest
    environment:
      SA_PASSWORD: "YourStrong!Passw0rd"
      ACCEPT_EULA: "Y"
    ports:
      - "1433:1433"
    volumes:
      - sql-data:/var/opt/mssql

  web:
    build: .
    ports:
      - "5000:80"
      - "5001:443"
    environment:
      - ConnectionStrings__DefaultConnection=Server=db;Database=StartupPitchingPlatform;User=sa;Password=YourStrong!Passw0rd;
    depends_on:
      - db

volumes:
  sql-data:
```

---

## Testing

To run unit tests (if any), use the following command:

```bash
docker-compose run web dotnet test
```

---

## Contributing

Contributions are welcome! Follow these steps to contribute:

1. Fork the repository.
2. Create a new branch for your feature or bugfix.
3. Commit your changes and push to your fork.
4. Submit a pull request with a detailed description of your changes.

---

## License

This project is licensed under the **MIT License**. See the [LICENSE](LICENSE) file for details.

---

## Support

If you encounter any issues or have questions, feel free to open an issue on the [GitHub repository](https://github.com/imrandevhub/startup-pitching-platform/issues).

---

