-- Defines a function that returns the 0-based index where the string passed to @functionString
-- is first found in the string passed to @columnString
CREATE FUNCTION [dbo].startIndex(@functionString AS NVARCHAR(MAX), @columnString AS NVARCHAR(MAX)) 
RETURNS INT
AS 
EXTERNAL NAME SQLServerRegEx.[SQLServerRegEx.SQLRegex].startIndex
