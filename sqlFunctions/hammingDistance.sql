-- Defines a function that returns the Hamming String distance between 
-- two strings
CREATE FUNCTION [dbo].hamming(@from AS NVARCHAR(MAX), @to AS NVARCHAR(MAX)) 
RETURNS FLOAT
AS 
EXTERNAL NAME SQLServerRegEx.[SQLServerRegEx.SQLRegex].hamming
