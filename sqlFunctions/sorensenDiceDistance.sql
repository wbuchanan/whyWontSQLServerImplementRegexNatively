-- Defines a function that returns the Sorensen Dice Distance between 
-- two strings
CREATE FUNCTION [dbo].diceDistance(@from AS NVARCHAR(MAX), @to AS NVARCHAR(MAX)) 
RETURNS FLOAT
AS 
EXTERNAL NAME SQLServerRegEx.[SQLServerRegEx.SQLRegex].sorensenDistance
