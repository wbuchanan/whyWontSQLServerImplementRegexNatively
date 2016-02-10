-- Defines a function that returns the Jaccard String distance between 
-- two strings
CREATE FUNCTION [dbo].jaccard(@from AS NVARCHAR(MAX), @to AS NVARCHAR(MAX)) 
RETURNS FLOAT
AS 
EXTERNAL NAME SQLServerRegEx.[SQLServerRegEx.SQLRegex].jaccard
