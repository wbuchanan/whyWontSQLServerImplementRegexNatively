-- Defines a function that returns the truncated interval between two dates at the user specified level of granularity
-- This method includes the end time in the span.
CREATE FUNCTION [dbo].intInterval(@from AS DATETIME, @to AS DATETIME, @type AS NVARCHAR(12)) 
RETURNS FLOAT
AS 
EXTERNAL NAME SQLServerDateTime.[SQLServerDateTime.SQLServerDateTime].intervalInterval
