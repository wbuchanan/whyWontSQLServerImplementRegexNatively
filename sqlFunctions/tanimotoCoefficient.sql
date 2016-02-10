-- Defines a function that returns the Tanimoto Coefficient between 
-- two strings
CREATE FUNCTION [dbo].tanimoto(@from AS NVARCHAR(MAX), @to AS NVARCHAR(MAX)) 
RETURNS FLOAT
AS 
EXTERNAL NAME SQLServerRegEx.[SQLServerRegEx.SQLRegex].tanimotoCoefficient
