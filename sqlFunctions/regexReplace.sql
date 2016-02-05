-- Defines a function that returns the value of the string passed in @columnString 
-- with instances that match the regular expression passed to @functionString are 
-- replaced with the string passed to @replaceWith
CREATE FUNCTION [dbo].regexReplace(@replaceWith AS NVARCHAR(MAX), @functionString AS NVARCHAR(MAX), @columnString AS NVARCHAR(MAX)) 
RETURNS NVARCHAR(MAX)
AS 
EXTERNAL NAME SQLServerRegEx.SQLRegex.regexReplace
