-- Defines a function that returns a comma delimited list of 0-based indices 
-- that identify the positions from start to end of the string @functionString 
-- in the values passed to @columnString
CREATE FUNCTION [dbo].indexList(@functionString AS NVARCHAR(MAX), @columnString AS NVARCHAR(MAX)) 
RETURNS NVARCHAR(MAX)
AS 
EXTERNAL NAME SQLServerRegEx.SQLRegex.indexList
