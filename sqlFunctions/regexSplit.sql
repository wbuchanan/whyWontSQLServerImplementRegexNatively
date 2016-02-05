-- Defines a function that returns the string passed to @columnString with 
-- the datum split and comma delimited based on the regular expression passed to 
-- @functionString
CREATE FUNCTION [dbo].regexSplit(@functionString AS NVARCHAR(MAX), @columnString AS NVARCHAR(MAX)) 
RETURNS NVARCHAR(MAX)
AS 
EXTERNAL NAME SQLServerRegEx.SQLRegex.regexSplit
