-- Defines a function that returns the Upper Bound of the Levenshtein String distance between 
-- two strings
CREATE FUNCTION [dbo].levUpper(@from AS NVARCHAR(MAX), @to AS NVARCHAR(MAX)) 
RETURNS FLOAT
AS 
EXTERNAL NAME SQLServerRegEx.[SQLServerRegEx.SQLRegex].levenshteinUpper
