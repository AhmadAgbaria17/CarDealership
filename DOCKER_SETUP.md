# Docker Setup Instructions

This guide will help you run the Car Dealership project using Docker and Docker Compose.

## Prerequisites

- Docker Desktop installed and running
- Docker Compose (usually included with Docker Desktop)

## Quick Start

1. **Navigate to the project root directory:**
   ```bash
   cd CarDealership
   ```

2. **Build and start all services:**
   ```bash
   docker-compose up --build
   ```

   This will:
   - Build the frontend, backend, and database containers
   - Start all services
   - Set up the network between containers

3. **Access the application:**
   - **Frontend:** http://localhost:3000
   - **Backend API:** http://localhost:5000
   - **Swagger UI:** http://localhost:5000/swagger
   - **Database:** localhost:1433 (SQL Server)

## Useful Commands

### Start services in detached mode (background):
```bash
docker-compose up -d
```

### View logs:
```bash
# All services
docker-compose logs -f

# Specific service
docker-compose logs -f backend
docker-compose logs -f frontend
docker-compose logs -f db
```

### Stop services:
```bash
docker-compose down
```

### Stop and remove volumes (cleans database data):
```bash
docker-compose down -v
```

### Rebuild specific service:
```bash
docker-compose build backend
docker-compose build frontend
```

### Restart a specific service:
```bash
docker-compose restart backend
```

## Database Setup

The database will be automatically created when the backend starts. However, you may need to run migrations:

1. **Access the backend container:**
   ```bash
   docker exec -it car_dealership_backend bash
   ```

2. **Run migrations:**
   ```bash
   dotnet ef database update
   ```

   Or if you prefer to run it from outside:
   ```bash
   docker exec -it car_dealership_backend dotnet ef database update
   ```

## Troubleshooting

### Backend won't start
- Check if the database is healthy: `docker-compose ps`
- Check backend logs: `docker-compose logs backend`
- Ensure the database password matches in the connection string

### Frontend can't connect to backend
- Verify backend is running: `docker-compose ps`
- Check if backend is healthy: The healthcheck should show as "healthy"
- Verify the API URL in the frontend environment variables

### Database connection issues
- Wait for the database to be fully ready (healthcheck passes)
- Check database logs: `docker-compose logs db`
- Verify connection string in docker-compose.yml

### Port conflicts
If ports 3000, 5000, or 1433 are already in use, modify the port mappings in `docker-compose.yml`:
```yaml
ports:
  - "3001:80"  # Change 3000 to 3001
```

## Environment Variables

### Backend
- `ASPNETCORE_ENVIRONMENT`: Set to `Development` or `Production`
- `ConnectionStrings__DefaultConnection`: Database connection string

### Frontend
- `VITE_API_URL`: API base URL (default: `http://localhost:5000/api`)

## Project Structure

```
CarDealership/
├── docker-compose.yml      # Orchestrates all services
├── frontend/               # React + Vite frontend
│   └── Dockerfile
├── dotnetbackend/          # .NET 8 backend
│   └── Dockerfile
└── DOCKER_SETUP.md         # This file
```

## Development Workflow

For development, you might want to:
1. Run only the database in Docker: `docker-compose up db`
2. Run frontend and backend locally with hot reload
3. Or use Docker for everything with volume mounts for live code updates

## Production Considerations

For production:
- Change database password
- Use environment-specific configuration files
- Enable HTTPS
- Set up proper CORS policies
- Use secrets management for sensitive data
