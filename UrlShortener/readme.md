# Url Shortener Server
## A Modern server for URL shortening
A lightweight and testable ASP.NET Core Web API that provides short URL generation, redirection, and real-time usage statistics. Built with Entity Framework Core using the **code-first approach** and connected to a PostgreSQL database.

Features

Shorten URLs
- Accepts any valid long URL via a `POST` endpoint
- Generates a unique short code and secret code
- Returns both the shortened URL and a stats link

Redirect via Short Link
- Endpoint `/s/{shortCode}` redirects to the original URL
- Automatically logs each visit

Visit Tracking
- Logs client IPs and access times
- Uses `X-Forwarded-For` when available, falls back to request IP

Statistics API
- Unique visits per day (1 per IP per day)
- Top 10 IPs by total visits

Unit Tested
- Business logic tested using **MSTest + Moq**
- Async service methods mocked and covered

To start a local development server, type in the terminal:

```bash
dontnet run
```

Once the server is running, your browser will automatically open Swagger UI, where you can test the API endpoints.

## Running unit tests

To execute unit tests, type in the terminal
```bash
dotnet test
```
