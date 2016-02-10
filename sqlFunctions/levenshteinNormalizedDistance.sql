-- Defines a function that returns the Normalized Levenshtein String distance between 
-- two strings
CREATE FUNCTION [dbo].levNormalized(@from AS NVARCHAR(MAX), @to AS NVARCHAR(MAX)) 
RETURNS FLOAT
AS 
EXTERNAL NAME SQLServerRegEx.[SQLServerRegEx.SQLRegex].levenshteinNormalized
