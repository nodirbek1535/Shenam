# Troubleshooting

## 1) `dotnet` command not found
### Symptoms
- Terminal returns `dotnet: command not found`

### Resolution
- Install .NET SDK 6.x
- Re-open terminal/session
- Validate:
```bash
dotnet --info
```

## 2) EF migration issues
### Symptoms
- Migration command fails
- DB update does not apply

### Resolution
1. Check connection string (`appsettings*.json`)
2. Ensure SQL Server/LocalDB is running
3. Re-run migration command:
```bash
dotnet ef database update --project Shenam.API/Shenam.API.csproj
```

## 3) SQL connection failures
### Symptoms
- Timeout or login errors

### Resolution
- Verify SQL instance name
- Verify credentials/Integrated Security settings
- Confirm firewall/network access (for remote SQL)

## 4) Swagger not loading
### Symptoms
- `/swagger` returns 404 or blank page

### Resolution
- Ensure app runs in `Development` environment
- Check startup URL and append `/swagger`
- Confirm API actually started without runtime errors

## 5) HTTPS certificate problems
### Symptoms
- Browser SSL warnings on localhost

### Resolution
```bash
dotnet dev-certs https --trust
```
Then restart browser and API.
