-- Defines a function that returns the Jaro-Winkler String distance between 
-- two strings
CREATE FUNCTION [dbo].jaroWinkler(@from AS NVARCHAR(MAX), @to AS NVARCHAR(MAX)) 
RETURNS FLOAT
AS 
EXTERNAL NAME SQLServerRegEx.[SQLServerRegEx.SQLRegex].jaroWinkler
