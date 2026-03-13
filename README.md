# WeatherApp
<img width="1909" height="911" alt="image" src="https://github.com/user-attachments/assets/345a4ba6-f4aa-46eb-be42-5057d1889b63" />

A full-stack weather application built as **Nx monorepo**. The frontend is an Angular SPA that displays weather, timezone, and astronomy data. The backend is a .NET Web API that proxies requests to an external weather API (RapidAPI).

## Tech Stack

### Frontend
- **Angular** 21 with TypeScript
- **PrimeNG** (UI component library)
- **RxJS**
- **Zod** (schema validation)
- **SCSS** (styling)
- **Vitest** (unit testing)
- **ESLint** (linting)

### Backend
- **.NET 10** (C#) Web API
- **MediatR** (CQRS / mediator pattern)
- **xUnit** + **FluentAssertions** + **NSubstitute** (testing)
- **Swashbuckle** (Swagger / OpenAPI)

### Tooling
- **Nx** 22 (monorepo orchestration, caching, task runner)
- **Vite** (frontend bundling)

---

## Prerequisites

- **Node.js** (LTS) and **npm**
- **.NET 10 SDK**
- **Nx CLI** – installed globally (`npm i -g nx`) or use via `npx nx`

---

## User Secrets Setup

The backend requires two secrets to communicate with the external weather API:

| Secret Key               | Description                          |
| ------------------------ | ------------------------------------ |
| `WEATHER_API_BASE_URL`   | Base URL of the weather API          |
| `WEATHER_API_KEY`        | API key for authenticating requests  |

The `UserSecretsId` is already configured in the `.csproj` file — no additional setup on your machine is required. Just set the secret values using one of the options below.

### Option 1 – CLI (dotnet)

```sh
cd apps/backend/Api

dotnet user-secrets set "WEATHER_API_BASE_URL" "<your-base-url>"
dotnet user-secrets set "WEATHER_API_KEY" "<your-api-key>"
```

### Option 2 – JetBrains Rider

1. In the Solution Explorer, right-click the **WeatherApp.Backend.Api** project.
2. Select **Tools → .NET User Secrets**.
3. A `secrets.json` file will open — add the following:

```json
{
  "WEATHER_API_BASE_URL": "<your-base-url>",
  "WEATHER_API_KEY": "<your-api-key>"
}
```

### Option 3 – Visual Studio

1. In the Solution Explorer, right-click the **WeatherApp.Backend.Api** project.
2. Select **Manage User Secrets**.
3. A `secrets.json` file will open — add the same JSON as above.

---

## Running the Applications

### Frontend (Angular)

```sh
npx nx serve weather-app
```

The dev server starts with live-reload enabled (default: `http://localhost:4200`).

### Backend (.NET API)

```sh
npx nx run WeatherApp.Backend.Api:watch
```

This starts the backend with `dotnet watch` so it auto-restarts on code changes.

Alternatively, to run without watch mode:

```sh
npx nx run WeatherApp.Backend.Api:run
```

Swagger UI will be available at `http://localhost:5103/swagger` by default.

### Running Both at Once

```sh
npx nx run-many -t serve watch -p weather-app WeatherApp.Backend.Api
```

---

## Linting

### Lint all projects

```sh
npx nx run-many -t lint
```

### Lint a specific project

```sh
npx nx lint weather-app
```

---

## Testing

### Run all tests (frontend + backend)

```sh
npx nx run-many -t test
```

### Run tests for a specific project

```sh
npx nx test weather-app
```
