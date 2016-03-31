-- ACCESS WIDA Growth Look up Function shell
CREATE FUNCTION F_ACCESS_GROWTH(@schyr SMALLINT, @grade TINYINT, @sproflev DOUBLE PRECISION, @scalediff INT) 

DECLARE @growthBand TINYINT;

BEGIN

SELECT INTO @growthBand = pctilegrowth
FROM ACCESS_PERCENTILE_BANDS
WHERE @schyr BETWEEN pschyr AND cschyr AND 
	  @grade BETWEEN pgrade AND cgrade AND 
	  @sproflev BETWEEN minlev AND maxlev AND 
	  @scalediff BETWEEN mindiff AND maxdiff;

RETURN @growthBand;

END;	  
