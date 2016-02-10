-- Defines a function that returns the Sorensen Dice Index between 
-- two strings
CREATE FUNCTION [dbo].diceIndex(@from AS NVARCHAR(MAX), @to AS NVARCHAR(MAX)) 
RETURNS FLOAT
AS 
EXTERNAL NAME SQLServerRegEx.[SQLServerRegEx.SQLRegex].sorensenIndex
