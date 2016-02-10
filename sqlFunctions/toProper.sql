-- Defines a function that returns the value of @columnString with 
-- Proper/Title casing (e.g., first letter of each word is capitalized)
CREATE FUNCTION [dbo].toProper(@columnString AS NVARCHAR(MAX)) 
RETURNS NVARCHAR(MAX)
AS 
EXTERNAL NAME SQLServerRegEx.[SQLServerRegEx.SQLRegex].toProper
