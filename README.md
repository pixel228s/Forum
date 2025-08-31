# Blog & Forum Web Application (.NET 8)

A blog web application built with ASP.NET Core 8, featuring secure authentication, content posting, admin controls, and background processing. Includes both a RESTful API and an MVC-based UI.

---

## Features

-**Authentication & Authorization**
  - JWT + Refresh Token system using ASP.NET Core Identity
  - OTP-based password reset via MailKit

- **Admin Controls**
  - Admin users can ban/unban users
  - Banned users cached in **Redis**
  - Access restricted via custom **middleware**

-  **Background Services**
  - Auto-remove expired bans
  - Archive inactive topics

- **Architecture & Design**
  - **Mediator pattern** via MediatR for decoupled logic
  - **AutoMapper** for object mapping
  - Clean separation of layers (API, Services, Data, UI)

---
  Tech Stack
--.NET 8 (ASP.NET Core)
--Entity Framework Core
--MediatR
--AutoMapper
--MailKit
--Redis (StackExchange)
--Amazon S3 (AWS SDK)
--JWT + Refresh Tokens
--MVC + REST API
--xUnit/Moq
---

==Setup Instructions==

 
appsettings.json and other secrets are ignored in version control for security reasons.

1. Clone the Repo
git clone https://github.com/pixel228s/Forum.git
cd Forum

2. Restore Dependencies
dotnet restore

3. Add Configuration Files

### Example `appsettings.json` (structure may vary)

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "YourDatabaseConnectionString"
  },
  "JWT": {
    "SecretKey": "YourSecretKey",
    "Issuer": "YourIssuer",
    "Audience": "YourAudience",
    "TokenExpirationMinutes": 60
  },
  "MailSettings": {
    "UserName": "youremail",
    "Password": "yourpassword",
    "SenderAddress": "senderemailaddress",
    "SenderName": "sendername",
    "SmtpServer": "smtpserver",
    "Port": 587
  },
  "AWS": {
    "Folder": "FoldeerName",
    "AccessKey": "AccessKey",
    "SecretKey": "SecretKey",
    "BucketName": "BucketName",
    "Region": "ap-south-1",
    "Location": "Location"
  },
  "Redis": {
    "Configuration": "localhost:6379"
  }
}

4. Run the App
dotnet run

