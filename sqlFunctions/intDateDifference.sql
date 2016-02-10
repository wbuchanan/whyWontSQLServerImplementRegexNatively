-- Defines a function that returns the truncated difference between two dates
CREATE FUNCTION [dbo].intDifference(@from AS DATETIME, @to AS DATETIME, @type AS NVARCHAR(12)) 
RETURNS FLOAT
AS 
EXTERNAL NAME SQLServerDateTime.[SQLServerDateTime.SQLServerDateTime].intervalDifference
