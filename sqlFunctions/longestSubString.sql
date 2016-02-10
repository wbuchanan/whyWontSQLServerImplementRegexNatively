-- Defines a function that returns the longest substring between two strings
CREATE FUNCTION [dbo].longestSubstring(@from AS NVARCHAR(MAX), @to AS NVARCHAR(MAX)) 
RETURNS NVARCHAR(MAX)
AS 
EXTERNAL NAME SQLServerRegEx.[SQLServerRegEx.SQLRegex].longestSubString
