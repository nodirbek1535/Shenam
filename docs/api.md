# API Documentation

## Base URL
Local development base URL is typically:
- `https://localhost:<port>`

Base route convention:
- `api/[controller]`

## Endpoints

### Guests
| Method | Route | Description |
|---|---|---|
| POST | `/api/Guests` | Create guest |
| GET | `/api/Guests/{guestId}` | Get guest by ID |
| GET | `/api/Guests` | List all guests |
| PUT | `/api/Guests` | Update guest |
| DELETE | `/api/Guests/{guestId}` | Delete guest |

### HostEntity
| Method | Route | Description |
|---|---|---|
| POST | `/api/HostEntity` | Create host |
| GET | `/api/HostEntity/{hostEntityId}` | Get host by ID |
| GET | `/api/HostEntity` | List all hosts |
| PUT | `/api/HostEntity` | Update host |
| DELETE | `/api/HostEntity/{hostEntityId}` | Delete host |

### Home
| Method | Route | Description |
|---|---|---|
| POST | `/api/Home` | Create home |
| GET | `/api/Home/{homeId}` | Get home by ID |
| GET | `/api/Home` | List all homes |
| PUT | `/api/Home` | Update home |
| DELETE | `/api/Home/{homeId}` | Delete home |

### HomeRequests
| Method | Route | Description |
|---|---|---|
| POST | `/api/HomeRequests` | Create home request |
| GET | `/api/HomeRequests/{homeRequestId}` | Get home request by ID |
| GET | `/api/HomeRequests` | List all home requests |
| PUT | `/api/HomeRequests` | Update home request |
| DELETE | `/api/HomeRequests/{homeRequestId}` | Delete home request |

## Example Requests

### Create Guest
```bash
curl -X POST "https://localhost:5001/api/Guests" \
  -H "Content-Type: application/json" \
  -d '{
    "id":"4e85f5c3-9f28-4a75-a6e6-5ad1d2011111",
    "firstName":"Ali",
    "lastName":"Karimov",
    "dateOfBirth":"1998-03-10T00:00:00+05:00",
    "email":"ali@example.com",
    "phoneNumber":"+998901234567",
    "address":"Tashkent, Chilonzor",
    "gender":0
  }'
```

### Get All Homes
```bash
curl -X GET "https://localhost:5001/api/Home"
```

### Delete HomeRequest
```bash
curl -X DELETE "https://localhost:5001/api/HomeRequests/<homeRequestId>"
```

## Example Responses

### Success (200 OK)
```json
{
  "id": "4e85f5c3-9f28-4a75-a6e6-5ad1d2011111",
  "firstName": "Ali",
  "lastName": "Karimov",
  "dateOfBirth": "1998-03-10T00:00:00+05:00",
  "email": "ali@example.com",
  "phoneNumber": "+998901234567",
  "address": "Tashkent, Chilonzor",
  "gender": 0
}
```

### Validation Error (400 Bad Request)
```json
{
  "message": "Guest is invalid.",
  "errors": {
    "FirstName": ["FirstName is required"]
  }
}
```

### Not Found (404)
```json
{
  "message": "Resource not found."
}
```

## HTTP Status Codes
| Code | Meaning |
|---|---|
| 200 | OK |
| 201 | Created |
| 400 | Bad Request |
| 404 | Not Found |
| 409 | Conflict |
| 423 | Locked |
| 500 | Internal Server Error |

## Data Models

### Guest
| Field | Type |
|---|---|
| Id | Guid |
| FirstName | string |
| LastName | string |
| DateOfBirth | DateTimeOffset |
| Email | string |
| PhoneNumber | string |
| Address | string |
| Gender | GenderType |

### HostEntity
| Field | Type |
|---|---|
| Id | Guid |
| FirstName | string |
| LastName | string |
| DateOfBirth | DateTimeOffset |
| Email | string |
| PhoneNumber | string |
| Gender | GenderType |

### Home
| Field | Type |
|---|---|
| Id | Guid |
| HostId | Guid |
| Address | string |
| AdditionalInfo | string |
| IsVacant | bool |
| NumberOfBedrooms | int |
| NumberOfBathrooms | int |
| Area | double (>0) |
| IsPAllowed | bool |
| HomeType | TypeHome |
| Price | decimal (>0) |
| IsShared | bool |

### HomeRequest
| Field | Type |
|---|---|
| Id | Guid |
| GuestId | Guid |
| HomeId | Guid |
| Message | string |
| StartDate | DateTimeOffset |
| EndDate | DateTimeOffset |
| CreatedDate | DateTimeOffset |
| UpdatedDate | DateTimeOffset |
