-- Defines a function that returns a boolean if the exact string passed to @functionString is 
-- contained in the contents of @columnString
CREATE FUNCTION [dbo].stringContains(@functionString AS NVARCHAR(MAX), @columnString AS NVARCHAR(MAX)) 
RETURNS INT
AS 
EXTERNAL NAME SQLServerRegEx.[SQLServerRegEx.SQLRegex].stringContains
