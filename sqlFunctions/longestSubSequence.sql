-- Defines a function that returns the longest subsequence of characters between two strings
CREATE FUNCTION [dbo].longestSubsequence(@from AS NVARCHAR(MAX), @to AS NVARCHAR(MAX)) 
RETURNS NVARCHAR(MAX)
AS 
EXTERNAL NAME SQLServerRegEx.[SQLServerRegEx.SQLRegex].longestSubSequence
