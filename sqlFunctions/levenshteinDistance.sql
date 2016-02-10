-- Defines a function that returns the Levenshtein String distance between 
-- two strings
CREATE FUNCTION [dbo].lev(@from AS NVARCHAR(MAX), @to AS NVARCHAR(MAX)) 
RETURNS FLOAT
AS 
EXTERNAL NAME SQLServerRegEx.[SQLServerRegEx.SQLRegex].levenshtein
