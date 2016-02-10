-- Defines a function that returns a boolean if the string passed to @columnString
-- is null or an empty string
CREATE FUNCTION [dbo].nullOrEmpty(@columnString AS NVARCHAR(MAX)) 
RETURNS INT
AS 
EXTERNAL NAME SQLServerRegEx.[SQLServerRegEx.SQLRegex].nullOrEmpty
