-- Defines a function that returns the Ratcliff-Obershelp Similarity Coefficient between 
-- two strings
CREATE FUNCTION [dbo].ratcliff(@from AS NVARCHAR(MAX), @to AS NVARCHAR(MAX)) 
RETURNS FLOAT
AS 
EXTERNAL NAME SQLServerRegEx.[SQLServerRegEx.SQLRegex].ratcliff
