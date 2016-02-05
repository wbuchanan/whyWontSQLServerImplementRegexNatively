-- Defines a function that returns a boolean if the regular expression passed to @functionString is 
-- found in the contents of @columnString
CREATE FUNCTION [dbo].regexMatch(@functionString AS NVARCHAR(MAX), @columnString AS NVARCHAR(MAX)) 
RETURNS INT
AS 
EXTERNAL NAME SQLServerRegEx.SQLRegex.regexMatch
