-- Defines a function that returns the 0-based index where the string passed to @functionString
-- ends in the string passed to @columnString
CREATE FUNCTION [dbo].endIndex(@functionString AS NVARCHAR(MAX), @columnString AS NVARCHAR(MAX)) 
RETURNS INT
AS 
EXTERNAL NAME SQLServerRegEx.SQLRegex.endIndex
