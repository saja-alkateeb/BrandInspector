Brand Inspector (.NET Framework 4.8.1 + Backend API)

Overview

Brand Inspector is a Windows Forms application designed to verify PowerPoint (.pptx) files for brand compliance — checking whether fonts, colors, and text sizes match the organization’s approved standards.

It authenticates to a backend API using JWT, retrieves the official brand settings, and performs automated scanning — without requiring Microsoft Office or VSTO.

Architecture

Client (WinForms)

Built on .NET Framework 4.8.1

Uses OpenXML SDK (DocumentFormat.OpenXml) to read .pptx files directly.

Asynchronous UI — scanning does not block the main thread.

Displays results in a DataGridView with errors grouped by slide or issue type.

 Backend (ASP.NET Core Web API)

JWT-based authentication (/auth/login)

Brand endpoints for settings:

/brand/fonts

/brand/colors

/brand/sizes

Configurable in-memory brand data (e.g., fonts, colors, sizes)

Prerequisites
Tools & Frameworks
Component	Version
.NET Framework	4.8.1
ASP.NET Core API	.NET 8
Newtonsoft.Json	13.x
DocumentFormat.OpenXml	2.20

Setup Instructions
1 Clone & Build
git clone repository


Open BrandInspector.sln in Visual Studio 2022
Ensure the following projects build successfully:

BrandInspector.Api (.NET 8)

BrandInspector (WinForms, .NET Framework 4.8.1)

2 Configure Backend (API)

In BrandInspector.Api, open appsettings.json:

{
  "Jwt": {
    "Key": "thisisaverystrongsecretkey123456789",
    "Issuer": "BrandInspector",
    "Audience": "BrandInspectorUsers",
    "ExpiresMinutes": 60
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}

3 Run the Backend

From Visual Studio or CLI:

cd BrandInspector.Api
dotnet run


Swagger will open at:

https://localhost:7003/swagger


Try the following:

POST /auth/login → returns JWT

{ "username": "admin", "password": "1234" }


GET /brand/fonts → returns list of fonts

GET /brand/colors → returns list of colors

GET /brand/sizes → returns list of allowed sizes

Example response:

["Arial", "Times New Roman"]

4 Configure the WinForms App

Open App.config inside BrandInspector.WinForms and update:

<appSettings>
  <add key="ApiBaseUrl" value="https://localhost:7003" />
</appSettings>

5 Run the WinForms Client

Launch BrandInspector.exe (set as startup project)

Login with:

Username: admin
Password: 1234


After successful login:

Click Browse → choose a .pptx file

Click Scan Fonts, Scan Colors, or Scan Sizes

The app will fetch allowed brand rules via the API and show Pass/Fail per slide.

Key Components
 ApiService.cs

Handles all REST API communication using HttpClient:

LoginAsync(username, password) → returns JWT

GetAuthorizedAsync<T>(endpoint, token) → fetches secured data

 PptxScanner.cs

Parses PowerPoint slides using OpenXML SDK, reading:

LatinFont.Typeface → font name

FontSize → text size

SolidFill.RgbColorModelHex → color in hex format
Then compares against backend rules.

 BrandController.cs

Provides mock brand settings:

[HttpGet("fonts")] => ["Arial", "Times New Roman"]
[HttpGet("colors")] => ["#000000", "#FFFFFF"]
[HttpGet("sizes")]  => [14, 32]

🪪 JwtService.cs

Generates JWT tokens with claims:

Issuer, Audience, Expiration, and HMAC SHA256 signing.

 Compliance Logic
Rule Type	Validation
Fonts	Must exist in brand fonts list (case-insensitive)
Colors	Must match hex color (case-insensitive)
Sizes	Must be within ±1 point tolerance of brand list
Example API Flow
POST /auth/login → JWT token  
GET /brand/fonts → ["Arial", "Times New Roman"]  
GET /brand/colors → ["#000000", "#FFFFFF"]  
GET /brand/sizes → [14, 32]


Then the WinForms client applies these for compliance scanning.

 UI Overview

Main Form

Browse .pptx file

Scan Fonts / Colors / Sizes buttons

DataGridView → Results table

ListBox → Error summary

Status bar → Totals & Non-compliant count
