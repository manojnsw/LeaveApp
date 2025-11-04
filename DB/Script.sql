

--  Tables
CREATE TABLE Users (
    UserId INT IDENTITY(1,1) PRIMARY KEY,
    FullName NVARCHAR(100) NOT NULL,
    IsActive BIT NOT NULL
);

CREATE TABLE LeaveApplications (
    LeaveId INT IDENTITY(1,1) PRIMARY KEY,
    ApplicantId INT NOT NULL,
    ManagerId INT NOT NULL,
    StartDate DATE NOT NULL,
    EndDate DATE NOT NULL,
    ReturnDate DATE NOT NULL,
    NumberOfDays INT NOT NULL,
    GeneralComments NVARCHAR(500),
    LeaveType NVARCHAR(50) NOT NULL,
    CreatedDate DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (ApplicantId) REFERENCES Users(UserId),
    FOREIGN KEY (ManagerId) REFERENCES Users(UserId)
);


CREATE INDEX IX_LeaveApplications_Applicant_Start_End ON LeaveApplications(ApplicantId, StartDate, EndDate);

--Seed data

INSERT INTO Users ( FullName, IsActive)
VALUES
('Alice Johnson',1),
('Bob Manager',1),
('Charlie Employee',1);