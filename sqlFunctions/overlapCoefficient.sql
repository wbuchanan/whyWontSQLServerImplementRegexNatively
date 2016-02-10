-- Defines a function that returns the Overlap Coefficient between 
-- two strings
CREATE FUNCTION [dbo].overlap(@from AS NVARCHAR(MAX), @to AS NVARCHAR(MAX)) 
RETURNS FLOAT
AS 
EXTERNAL NAME SQLServerRegEx.[SQLServerRegEx.SQLRegex].overlap
