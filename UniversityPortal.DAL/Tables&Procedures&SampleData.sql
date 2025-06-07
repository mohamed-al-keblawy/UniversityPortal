
------------------------- Procedures  -------------------------

CREATE TABLE Users (
    UserId INT PRIMARY KEY IDENTITY,
    Username NVARCHAR(100) UNIQUE,
    PasswordHash NVARCHAR(256),
    Role NVARCHAR(50) -- 'Faculty' or 'Student'
);

CREATE TABLE Assignments (
    AssignmentId INT PRIMARY KEY IDENTITY,
    Title NVARCHAR(200),
    Description NVARCHAR(MAX),
    DueDate DATETIME,
    CreatedBy INT FOREIGN KEY REFERENCES Users(UserId)
);

CREATE TABLE Submissions (
    SubmissionId INT PRIMARY KEY IDENTITY,
    AssignmentId INT FOREIGN KEY REFERENCES Assignments(AssignmentId),
    SubmittedBy INT FOREIGN KEY REFERENCES Users(UserId),
    SubmissionUrl NVARCHAR(MAX),
    SubmissionDate DATETIME
);

CREATE TABLE Assessments (
    AssessmentId INT PRIMARY KEY IDENTITY,
    AssignmentId INT FOREIGN KEY REFERENCES Assignments(AssignmentId),
    StudentId INT FOREIGN KEY REFERENCES Users(UserId),
    AssessedBy INT FOREIGN KEY REFERENCES Users(UserId)
);

CREATE TABLE AssessmentCriteria (
    CriterionId INT PRIMARY KEY IDENTITY,
    AssessmentId INT FOREIGN KEY REFERENCES Assessments(AssessmentId),
    CriterionName NVARCHAR(100),
    Score INT,
    Remarks NVARCHAR(MAX)
);

------------------------- Procedures  -------------------------





------------------------- Sample Data -------------------------