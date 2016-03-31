/*
The following SQL IS NOT READY FOR PRODUCTION DEPLOYMENT
It is a sketch to try building out lag functionality required to classify students' change 
in ACCESS scores into percentile growth bands.

This will eventual need to be incorporated into a larger procedure that can be triggered by updates
to the score fact table that will cause a growth fact table to be populated/updated.
*/

CREATE TABLE ACCESS_GROWTH_FACT(stdid INT NOT NULL, schyr SMALLINT NOT NULL, overall_diff INT, overall_growth TINYINT,
read_diff INT, read_growth TINYINT, listen_diff INT, listen_growth TINYINT, write_diff INT, write_growth TINYINT, 
speak_diff INT, speak_growth TINYINT, oral_diff INT, oral_growth TINYINT, literacy_diff INT, literacy_growth TINYINT,
comprehension_diff INT, comprehension_growth TINYINT, CONSTRAINT pk_access_growth_fact PRIMARY KEY (stdid, schyr));


WITH source AS (
SELECT * 
FROM ACCESS_SCORE_FACT),
lagleadids ASv(
SELECT stdid, schyr, 
ROW_NUMBER() OVER (PARTITION BY stdid ORDER BY stdid, schyr) rn,
(ROW_NUMBER() OVER (PARTITION BY stdid ORDER BY stdid, schyr))/2 rndiv2,
(ROW_NUMBER() OVER (PARTITION BY stdid ORDER BY stdid, schyr) + 1)/2 rnplus1div2,
FROM source
),
lagAndCurrentScores AS (
SELECT a.stdid, a.schyr, a.grade, a.overallsc, a.readsc, a.listensc, a.writesc, a.speaksc, a.oralsc, a.literacysc, a.comprehensionsc,
CASE 
	WHEN b.rn%2 = 1 THEN MAX(CASE WHEN b.rn%2 = 0 THEN a.grade END) OVER (PARTITION BY a.stdid, a.schyr) 
	ELSE MAX(CASE WHEN b.rn%2 = 1 THEN a.grade END) OVER (PARTITION BY a.stdid, a.schyr)
END AS starting_grade,
CASE 
	WHEN b.rn%2 = 1 THEN MAX(CASE WHEN b.rn%2 = 0 THEN a.overallsc END) OVER (PARTITION BY a.stdid, a.schyr) 
	ELSE MAX(CASE WHEN b.rn%2 = 1 THEN a.overallsc END) OVER (PARTITION BY a.stdid, a.schyr)
END AS prior_overallsc,
CASE 
	WHEN b.rn%2 = 1 THEN MAX(CASE WHEN b.rn%2 = 0 THEN a.overalllev END) OVER (PARTITION BY a.stdid, a.schyr) 
	ELSE MAX(CASE WHEN b.rn%2 = 1 THEN a.overalllev END) OVER (PARTITION BY a.stdid, a.schyr)
END AS prior_overalllev,
CASE 
	WHEN b.rn%2 = 1 THEN MAX(CASE WHEN b.rn%2 = 0 THEN a.readsc END) OVER (PARTITION BY a.stdid, a.schyr) 
	ELSE MAX(CASE WHEN b.rn%2 = 1 THEN a.readsc END) OVER (PARTITION BY a.stdid, a.schyr)
END AS prior_readsc,
CASE 
	WHEN b.rn%2 = 1 THEN MAX(CASE WHEN b.rn%2 = 0 THEN a.readlev END) OVER (PARTITION BY a.stdid, a.schyr) 
	ELSE MAX(CASE WHEN b.rn%2 = 1 THEN a.readlev END) OVER (PARTITION BY a.stdid, a.schyr)
END AS prior_readlev,
CASE 
	WHEN b.rn%2 = 1 THEN MAX(CASE WHEN b.rn%2 = 0 THEN a.writsc END) OVER (PARTITION BY a.stdid, a.schyr) 
	ELSE MAX(CASE WHEN b.rn%2 = 1 THEN a.writsc END) OVER (PARTITION BY a.stdid, a.schyr)
END AS prior_writsc,
CASE 
	WHEN b.rn%2 = 1 THEN MAX(CASE WHEN b.rn%2 = 0 THEN a.writlev END) OVER (PARTITION BY a.stdid, a.schyr) 
	ELSE MAX(CASE WHEN b.rn%2 = 1 THEN a.writlev END) OVER (PARTITION BY a.stdid, a.schyr)
END AS prior_writlev,
CASE 
	WHEN b.rn%2 = 1 THEN MAX(CASE WHEN b.rn%2 = 0 THEN a.speaksc END) OVER (PARTITION BY a.stdid, a.schyr) 
	ELSE MAX(CASE WHEN b.rn%2 = 1 THEN a.speaksc END) OVER (PARTITION BY a.stdid, a.schyr)
END AS prior_speaksc,
CASE 
	WHEN b.rn%2 = 1 THEN MAX(CASE WHEN b.rn%2 = 0 THEN a.speaklev END) OVER (PARTITION BY a.stdid, a.schyr) 
	ELSE MAX(CASE WHEN b.rn%2 = 1 THEN a.speaklev END) OVER (PARTITION BY a.stdid, a.schyr)
END AS prior_speaklev,
CASE 
	WHEN b.rn%2 = 1 THEN MAX(CASE WHEN b.rn%2 = 0 THEN a.listensc END) OVER (PARTITION BY a.stdid, a.schyr) 
	ELSE MAX(CASE WHEN b.rn%2 = 1 THEN a.listensc END) OVER (PARTITION BY a.stdid, a.schyr)
END AS prior_listensc,
CASE 
	WHEN b.rn%2 = 1 THEN MAX(CASE WHEN b.rn%2 = 0 THEN a.listenlev END) OVER (PARTITION BY a.stdid, a.schyr) 
	ELSE MAX(CASE WHEN b.rn%2 = 1 THEN a.listenlev END) OVER (PARTITION BY a.stdid, a.schyr)
END AS prior_listenlev,
CASE 
	WHEN b.rn%2 = 1 THEN MAX(CASE WHEN b.rn%2 = 0 THEN a.oralsc END) OVER (PARTITION BY a.stdid, a.schyr) 
	ELSE MAX(CASE WHEN b.rn%2 = 1 THEN a.oralsc END) OVER (PARTITION BY a.stdid, a.schyr)
END AS prior_oralsc,
CASE 
	WHEN b.rn%2 = 1 THEN MAX(CASE WHEN b.rn%2 = 0 THEN a.orallev END) OVER (PARTITION BY a.stdid, a.schyr) 
	ELSE MAX(CASE WHEN b.rn%2 = 1 THEN a.orallev END) OVER (PARTITION BY a.stdid, a.schyr)
END AS prior_orallev,
CASE 
	WHEN b.rn%2 = 1 THEN MAX(CASE WHEN b.rn%2 = 0 THEN a.comprehensionsc END) OVER (PARTITION BY a.stdid, a.schyr) 
	ELSE MAX(CASE WHEN b.rn%2 = 1 THEN a.comprehensionsc END) OVER (PARTITION BY a.stdid, a.schyr)
END AS prior_comprehensionsc,
CASE 
	WHEN b.rn%2 = 1 THEN MAX(CASE WHEN b.rn%2 = 0 THEN a.comprehensionlev END) OVER (PARTITION BY a.stdid, a.schyr) 
	ELSE MAX(CASE WHEN b.rn%2 = 1 THEN a.comprehensionlev END) OVER (PARTITION BY a.stdid, a.schyr)
END AS prior_comprehensionlev,
CASE 
	WHEN b.rn%2 = 1 THEN MAX(CASE WHEN b.rn%2 = 0 THEN a.literacysc END) OVER (PARTITION BY a.stdid, a.schyr) 
	ELSE MAX(CASE WHEN b.rn%2 = 1 THEN a.literacysc END) OVER (PARTITION BY a.stdid, a.schyr)
END AS prior_literacysc,
CASE 
	WHEN b.rn%2 = 1 THEN MAX(CASE WHEN b.rn%2 = 0 THEN a.literacylev END) OVER (PARTITION BY a.stdid, a.schyr) 
	ELSE MAX(CASE WHEN b.rn%2 = 1 THEN a.literacylev END) OVER (PARTITION BY a.stdid, a.schyr)
END AS prior_literacylev
FROM source a
INNER JOIN lagleadids b ON a.stdid = b.stdid AND a.schyr = b.schyr)
