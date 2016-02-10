-- Defines a function that returns a bit value indicating whether the two strings are approximately the same
CREATE FUNCTION [dbo].isApproximatelySame(@from AS NVARCHAR(MAX), @to AS NVARCHAR(MAX), 
										  @compOptions AS NVARCHAR(MAX), @toleranceOptions AS NVARCHAR(MAX)) 
RETURNS BIT
AS 
EXTERNAL NAME SQLServerRegEx.[SQLServerRegEx.SQLRegex].approximatelySame
