-- Defines a function that returns the Jaro String distance between 
-- two strings
CREATE FUNCTION [dbo].jaro(@from AS NVARCHAR(MAX), @to AS NVARCHAR(MAX)) 
RETURNS FLOAT
AS 
EXTERNAL NAME SQLServerRegEx.[SQLServerRegEx.SQLRegex].jaro
