-- Defines a function that returns the value of @columnString with instances of 
-- @functionString replaced with the value passed to @replaceWith
CREATE FUNCTION [dbo].stringReplace(@replaceWith AS NVARCHAR(MAX), 
									@functionString AS NVARCHAR(MAX), 
									@columnString AS NVARCHAR(MAX)) 
RETURNS NVARCHAR(MAX)
AS 
EXTERNAL NAME SQLServerRegEx.[SQLServerRegEx.SQLRegex].stringReplace
