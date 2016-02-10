-- Defines a function that returns the Lower Bound on the Levenshtein String distance between 
-- two strings
CREATE FUNCTION [dbo].levLower(@from AS NVARCHAR(MAX), @to AS NVARCHAR(MAX)) 
RETURNS FLOAT
AS 
EXTERNAL NAME SQLServerRegEx.[SQLServerRegEx.SQLRegex].levenshteinLower
