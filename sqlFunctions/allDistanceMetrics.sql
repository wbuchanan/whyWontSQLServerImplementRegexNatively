-- Defines a function that returns a [1, 2] row vector of string distances.
-- The string distance in the first position is an implementation of the Levenshtein Distance
-- The string distance in the first position is an implementation of the Damerau/Levenshtein Distance
/*
This method is still in development and will fail if you attempt to build it.  If you're familiar with 
table valued CLR functions you may be able to help figure out what is causing the issue with the 
methods used to construct this.
CREATE FUNCTION [dbo].stringDist(@string1 AS NVARCHAR(MAX), @string2 AS NVARCHAR(MAX)) 
RETURNS TABLE (	Hamming_Distance FLOAT, 
				Jaccard_Distance FLOAT, 
				Jaro_Distance FLOAT,
				Jaro_Winkler_Distance FLOAT, 
				Levenshtein_Distance FLOAT,
				Levenshtein_Distance_Lower_Bound FLOAT,
				Levenshtein_Distance_Upper_Bound FLOAT,
				Normalized_Levenshtein_Distance FLOAT,
				Overlap_Coefficient FLOAT,
				Ratcliff_Obershelp_Similarity FLOAT,
				Sorensen_Dice_Index FLOAT,
				Sorensen_Dice_Distance FLOAT,
				Tanimoto_Coefficient FLOAT,
				Longest_Common_Substring NVARCHAR(MAX),
				Longest_Common_Subsequence NVARCHAR(MAX))
AS 
EXTERNAL NAME SQLServerRegEx.SQLRegex.allDistanceMetrics
GO

*/