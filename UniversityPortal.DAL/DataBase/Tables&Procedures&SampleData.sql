
------------------------- Tables  -------------------------
/*
CREATE TABLE [dbo].[Roles](
	[RoleId] [int] IDENTITY(1,1) NOT NULL,
	[RoleName] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO
CREATE TABLE Users (
    UserId INT PRIMARY KEY IDENTITY,
    Username NVARCHAR(100) UNIQUE,
    PasswordHash NVARCHAR(256),
    RoleId int -- Faculty or Student or Admin
);

ALTER TABLE [dbo].[Users]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[Roles] ([RoleId])

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
*/

------------------------- Procedures  -------------------------
/*
CREATE PROCEDURE sp_GetUserByUsername
    @Username NVARCHAR(100)
AS
BEGIN
    SELECT * FROM Users WHERE Username = @Username;
END

CREATE PROCEDURE sp_GetAllAssignments
AS
BEGIN
    SELECT * FROM Assignments;
END

CREATE PROCEDURE sp_GetAssignmentById
    @AssignmentId INT
AS
BEGIN
    SELECT * FROM Assignments WHERE AssignmentId = @AssignmentId;
END


CREATE PROCEDURE sp_CreateAssignment
    @Title NVARCHAR(200),
    @Description NVARCHAR(MAX),
    @DueDate DATETIME,
    @CreatedBy INT
AS
BEGIN
    INSERT INTO Assignments (Title, Description, DueDate, CreatedBy)
    VALUES (@Title, @Description, @DueDate, @CreatedBy);

    SELECT SCOPE_IDENTITY();
END


CREATE PROCEDURE sp_SubmitAssignment
    @AssignmentId INT,
    @SubmittedBy INT,
    @SubmissionUrl NVARCHAR(MAX),
    @SubmissionDate DATETIME
AS
BEGIN
    INSERT INTO Submissions (AssignmentId, SubmittedBy, SubmissionUrl, SubmissionDate)
    VALUES (@AssignmentId, @SubmittedBy, @SubmissionUrl, @SubmissionDate);
END


CREATE PROCEDURE sp_GetSubmissionsByStudent
    @StudentId INT
AS
BEGIN
    SELECT * FROM Submissions WHERE SubmittedBy = @StudentId;
END


CREATE PROCEDURE sp_CreateAssessment
    @AssignmentId INT,
    @StudentId INT,
    @AssessedBy INT,
    @AssessmentId INT OUTPUT
AS
BEGIN
    INSERT INTO Assessments (AssignmentId, StudentId, AssessedBy)
    VALUES (@AssignmentId, @StudentId, @AssessedBy);

    SET @AssessmentId = SCOPE_IDENTITY();
END


CREATE PROCEDURE sp_AddAssessmentCriterion
    @AssessmentId INT,
    @CriterionName NVARCHAR(100),
    @Score INT,
    @Remarks NVARCHAR(MAX)
AS
BEGIN
    INSERT INTO AssessmentCriteria (AssessmentId, CriterionName, Score, Remarks)
    VALUES (@AssessmentId, @CriterionName, @Score, @Remarks);
END


CREATE PROCEDURE sp_GetAssessmentByStudent
    @StudentId INT
AS
BEGIN
    SELECT * FROM Assessments WHERE StudentId = @StudentId;
END


CREATE PROCEDURE sp_GetCriteriaByAssessment
    @AssessmentId INT
AS
BEGIN
    SELECT * FROM AssessmentCriteria WHERE AssessmentId = @AssessmentId;
END


CREATE PROCEDURE sp_GetStudentPerformanceSummary
    @StudentId INT
AS
BEGIN
    SELECT 
        AC.CriterionName,
        AVG(AC.Score) AS AverageScore
    FROM Assessments A
    JOIN AssessmentCriteria AC ON A.AssessmentId = AC.AssessmentId
    WHERE A.StudentId = @StudentId
    GROUP BY AC.CriterionName;
END

*/

------------------------- Sample Data -------------------------
/*

INSERT INTO Roles (RoleName)
VALUES ('Admin'),('Faculty'), ('Student');


-- Admin
INSERT INTO Users (FullName, Email, PasswordHash, RoleId)
VALUES 
('Admin', 'admin@gmail.com', 'vHjljVXN4TRuaPjl/liN7fYvpFeqZGpQClM0f6/27iQ=', 1); -- Admin@1234


-- Faculty
INSERT INTO Users (FullName, Email, PasswordHash, RoleId)
VALUES 
('Dr. Mohamed Al Keblawy', 'alkeblawy.m@gmail.com', 'HZWkxtaB7eWxjImyHOtGv+p7jk2PgkEHYVou4pdJNxA=', 2); -- Pass@1234

INSERT INTO Users (FullName, Email, PasswordHash, RoleId)
VALUES 
('Dr. Youssef Mobarak ', 'y.Mobarak@gmail.com', 'DSCRKolh0rDrx+1CcXKVf+v71yZV8WyCnh3Jui7Biwk=', 2); -- Youssef@2025

-- Students
INSERT INTO Users (FullName, Email, PasswordHash, RoleId)
VALUES 
('Asmaa Kabalan', 'asmaa1990@gmail.com', 'JlebtEGyHB4ZFI6ug3tEIalYSl7ixURJ+0ZwxXAM2r8=', 3),  -- Asmaa@1990
('Youssef Hassan', 'youssef2025@gmail.com', 'DSCRKolh0rDrx+1CcXKVf+v71yZV8WyCnh3Jui7Biwk=', 3); --Youssef@2025

*/