

CREATE PROCEDURE sp_InsertLeaveApplication
    @ApplicantId INT,
    @ManagerId INT,
    @StartDate DATE,
    @EndDate DATE,
    @ReturnDate DATE,
    @NumberOfDays INT,
    @GeneralComments NVARCHAR(500),
    @LeaveType NVARCHAR(50)
AS
BEGIN
    -- Check overlapping leave
    IF EXISTS (
        SELECT 1 FROM LeaveApplications
        WHERE ApplicantId = @ApplicantId
          AND ((StartDate BETWEEN @StartDate AND @EndDate)
          OR (EndDate BETWEEN @StartDate AND @EndDate))
    )
    BEGIN
        RAISERROR('Overlapping leave request exists',16,1)
        RETURN
    END

    -- Insert leave
    INSERT INTO LeaveApplications
        (ApplicantId, ManagerId, StartDate, EndDate, ReturnDate, NumberOfDays, GeneralComments, LeaveType)
    VALUES
        (@ApplicantId, @ManagerId, @StartDate, @EndDate, @ReturnDate, @NumberOfDays, @GeneralComments, @LeaveType)
END