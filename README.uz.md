# Shenam API

[![.NET](https://img.shields.io/badge/.NET-6.0-512BD4?logo=dotnet)](https://dotnet.microsoft.com/)
[![Build](https://img.shields.io/badge/build-passing-brightgreen)](#)
[![Tests](https://img.shields.io/badge/tests-unit-informational)](docs/testing.md)
[![License](https://img.shields.io/badge/license-MIT-blue.svg)](LICENSE)

.NET 6 asosida yozilgan, ijara bozoridagi jarayonlarni boshqaruvchi toza va professional REST API.

## 🌐 Til
- 🇬🇧 Inglizcha → [README.md](README.md)
- 🇺🇿 **O‘zbekcha** (joriy)

## Mundarija
- [Umumiy ma'lumot](#umumiy-malumot)
- [Imkoniyatlar](#imkoniyatlar)
- [Texnologiyalar](#texnologiyalar)
- [Loyiha arxitekturasi](#loyiha-arxitekturasi)
- [Loyiha strukturasi](#loyiha-strukturasi)
- [Boshlash](#boshlash)
- [Tez ishga tushirish](#tez-ishga-tushirish)
- [Hujjatlar](#hujjatlar)
- [Hissa qo‘shish](#hissa-qoshish)
- [Litsenziya](#litsenziya)

## Umumiy ma'lumot
Shenam API quyidagi entitylar bilan ishlaydi:
- Guest
- HostEntity
- Home
- HomeRequest

API RESTful CRUD endpointlar, service darajasida validatsiya va SQL Serverga saqlash imkonini beradi.

## Imkoniyatlar
- Barcha asosiy entitylar uchun CRUD endpointlar
- Service qatlamida validatsiya va exception handling
- Entity Framework Core orqali SQL Serverga saqlash
- Swagger/OpenAPI orqali endpointlarni sinash
- Unit testlar uchun tayyor stack

## Texnologiyalar
- .NET 6
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- Swagger
- RESTFulSense
- xUnit
- Moq
- FluentAssertions

## Loyiha arxitekturasi
Qatlamli yondashuv:
- **Controller**: HTTP boundary va status mapping
- **Service**: biznes logika va validatsiya
- **Broker**: storage va logging abstraksiyasi
- **Database**: SQL Server

Batafsil arxitektura: [docs/architecture.md](docs/architecture.md)

## Loyiha strukturasi
```text
Shenam/
├── README.md
├── README.uz.md
├── LICENSE
└── docs/
    ├── architecture.md
    ├── api.md
    ├── diagrams.md
    ├── testing.md
    ├── deployment.md
    └── troubleshooting.md
```

## Boshlash
1. Reponi clone qiling
2. Connection stringni sozlang
3. EF migrationlarni qo‘llang
4. API ni ishga tushiring
5. Swaggerda tekshiring

## Tez ishga tushirish
```bash
git clone <REPO_URL>
cd Shenam/Shenam.API
dotnet restore Shenam.API.sln
dotnet build Shenam.API.sln
dotnet ef database update --project Shenam.API/Shenam.API.csproj
cd Shenam.API
dotnet run
```

Swagger: `https://localhost:<port>/swagger`

## Hujjatlar
- Arxitektura → [docs/architecture.md](docs/architecture.md)
- To‘liq API hujjati → [docs/api.md](docs/api.md)
- Diagrammalar → [docs/diagrams.md](docs/diagrams.md)
- Test qo‘llanmasi → [docs/testing.md](docs/testing.md)
- Deployment → [docs/deployment.md](docs/deployment.md)
- Muammolarni hal qilish → [docs/troubleshooting.md](docs/troubleshooting.md)

## Hissa qo‘shish
Feature branchlar va kichik PRlar tavsiya etiladi.
1. Fork qiling
2. Branch yarating (`feature/<name>`)
3. Zarur joyda test qo‘shing/yangilang
4. Hujjatlarni yangilang
5. Pull request oching

## Litsenziya
Loyiha [MIT License](LICENSE) asosida tarqatiladi.
