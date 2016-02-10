-- Defines a function that returns the Jaro-Winkler String distance between 
-- two strings with adjustment for the prefix scale
CREATE FUNCTION [dbo].jaroWinklerPrefix(@from AS NVARCHAR(MAX), @to AS NVARCHAR(MAX), @scale AS FLOAT) 
RETURNS FLOAT
AS 
EXTERNAL NAME SQLServerRegEx.[SQLServerRegEx.SQLRegex].jaroWinklerPrefix
