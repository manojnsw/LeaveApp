# Leave Application Management System

### Technologies:
- **Backend:** ASP.NET Core Web API 
- **Frontend:** Angular 20+
- **Database:** SQL Server
- **Data Access:** ADO.NET with Stored Procedures 
- **Architecture:** Repository Pattern
- **Communication:** HTTP REST API




## Project Structure

### Backend (Web API)
LeaveApp_api/

### Frontend (Angular)
leave-app/


## Database Setup

1. Open **SQL Server Management Studio**.
2. Create a database named:   LeaveAppDB

3. Run the script - script.sql

4. Create Stored Procedures:


---

## Backend Setup (ASP.NET Core)

1. Open `LeaveApp_API.sln` in Visual Studio.
2. Update the connection string in **appsettings.json**:
   "ConnectionStrings": {
     "DefaultConnection": "Server=YOUR_SERVER_NAME;Database=LeaveAppDB;Trusted_Connection=True;"
   }
3. Run the project:
   dotnet run
   Default URL:  
    https://localhost:5001

4. Test endpoints in browser or Postman:
   - GET https://localhost:5001/api/user/active
   - POST https://localhost:5001/api/leave/submit



##  Frontend Setup (Angular)

1. Navigate to the Angular project folder:
   cd leave-app

2. Install dependencies:
   npm install

3. Update **environment.ts** (API base URL):
   export const environment = {
     production: false,
     apiBaseUrl: 'https://localhost:5001/api'
   };

4. Run the Angular app:
   ng serve
   App URL:  
   http://localhost:4200

---

## API Endpoints


/api/user/active | GET | Get list of active users |
/api/leave/submit | POST | Submit a new leave application |

---

##  Angular Components Overview

- **leaveform.component.ts**
  - Displays the leave form
  - Dropdowns for Applicant and Manager
  - Date pickers for Start, End, and Return dates
  - Submits data using LeaveFormService

- **user.service.ts**
  - Fetches users via /api/user/active

- **leaveform.service.ts**
  - Submits leave forms to /api/leave/submit


##  Key Features

 Repository Pattern (separate data logic)  
Stored Procedures (no EF)  
Async ADO.NET calls  
Angular service integration  
CORS enabled  
Strongly typed models  
Simple UI with dropdowns and date pickers  

---

##  Future Enhancements
- Add authentication (JWT)

