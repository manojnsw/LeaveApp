

CREATE PROCEDURE sp_GetActiveUsers
AS
BEGIN
    SELECT UserId, FullName, IsActive
    FROM Users
    WHERE IsActive = 1
END