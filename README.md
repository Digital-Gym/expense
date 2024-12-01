This application was developed for the Web Application module, as coursework portfolio project @ WIUT by student ID: 00015641

## Expense Manager

- [Introduction](#introduction)
- [Technologies Used](#technologies-used)
- [Prerequisites](#prerequisites)
- [Run locally](#run)

## Introduction

The **Expense Manager** is a web application that allows users to track their expenses by categorizing them, viewing detailed statistics, and managing their financial data. The app is designed to provide a smooth user experience, with both backend and frontend capabilities. The backend is built using **ASP.NET Core**, with **Entity Framework** for database interactions, while the frontend is built with **Angular**.

## Technologies Used

- **Backend**: 
  - ASP.NET Core (C#)
  - Entity Framework Core (ORM)
  - SQL Server (or any supported relational database)
  - ASP.NET Web API

- **Frontend**:
  - Angular (TypeScript)
  - PrimeNg (UI components)

## Prerequisites

Before you can run the application locally, you need to have the following installed:

- **.NET 6 SDK** or later: [Download Link](https://dotnet.microsoft.com/download)
- **Node.js** (for Angular frontend): [Download Link](https://nodejs.org/)
- **Angular CLI**: Install globally using npm:

  ```bash
  npm install -g @angular/cli
  ```

- **MS SQL Server**

## Run
1. Open backend project with Visual Studio (Web components installed)
2. Migrate the models to your MS SQL server database
3. Build and run
4. Go to client directory
5. Install dependencies (`npm i`)
5. Start dev server `npm start`
6. Go to http://localhost:4200
7. Enjoy!