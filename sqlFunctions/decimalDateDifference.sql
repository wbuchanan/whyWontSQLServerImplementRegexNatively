-- Defines a function that returns the decimal valued difference between two dates at the user specified level of granularity
CREATE FUNCTION [dbo].decimalDifference(@from AS DATETIME, @to AS DATETIME, @type AS NVARCHAR(12)) 
RETURNS FLOAT
AS 
EXTERNAL NAME SQLServerDateTime.[SQLServerDateTime.SQLServerDateTime].decimalDifference
