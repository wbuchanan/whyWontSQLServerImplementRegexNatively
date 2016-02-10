/*
These examples are specific to the RDBMS used by Minneapolis Public Schools, but 
the concepts are applicable across RDBMS systems.  
*/

-- Returns invalid phone numbers first and valid phone numbers after
SELECT	PHONE_NUMBER AS PhoneNumber,
		[dbo].regexMatch('\(?\d{3}\)?-? *\d{3}-? *-?\d{4}', PHONE_NUMBER) AS isValid 
FROM	[EDUCAT-SSIS-20].[DISCOVERY_DB].[dbo].PERSON_PHONE
ORDER BY 2 ASC;

-- Returns the name of schools in Title/Proper case
SELECT	SCHOOL_TITLE AS StandardSchoolNames, 
		[dbo].toProper(SCHOOL_TITLE) AS BetterSchoolNames
FROM	[EDUCAT-SSIS-20].[DISCOVERY_DB].[dbo].SCHOOL;

-- Returns the indices that match the string AP in course names
SELECT	COURSE_TITLE AS CourseName, 
		dbo.startIndex('AP', COURSE_TITLE) AS beginAPinCourseName,
		dbo.endIndex('AP', COURSE_TITLE) AS endAPinCourseName,
		dbo.indexList('AP', COURSE_TITLE) AS allIndicesAPinCourseName		
FROM	[EDUCAT-SSIS-20].[DISCOVERY_DB].[dbo].COURSE;

-- Expands the abbreviation IB to the string International Baccalaureate
SELECT	DISTINCT
		COURSE_TITLE AS CourseName,
		dbo.regexReplace('International Baccalaureate', '(IB)', COURSE_TITLE) AS UnabbreviatedCourseName
FROM	[EDUCAT-SSIS-20].[DISCOVERY_DB].[dbo].COURSE
WHERE	dbo.regexMatch('(IB)', COURSE_TITLE) = 1;


-- Query that shows how regular expressions can be helpful for finding invalid cases
WITH badNames AS (	  
/*
\p{Lu} - Unicode character class reference for upper cased letters
\p{Ll} - Unicode character class reference for lower cased letters
\p{Pd} - Unicode character class reference for dashes (e.g., Born-Married)
\p{Zs} - Unicode character class reference for spaces
\u0027 - Unicode character for apostrophes
\.	   - A string literal for a period (it needs to be escaped since the period has a unique meaning in regular expressions)
*/
SELECT	pid,		-- Person ID
		famid,		-- Family ID
		ptype,		-- Person Type Code
		firstnm,	-- First Name
		lastnm,		-- Last Name
		mi,			-- Middle Initial
		dob,		-- Date of Birth
		created,	-- Date the record was created
		modified,	-- Date the record was last modified
		dbo.regexMatch('([^\p{Lu}\p{Ll}\p{Pd}\p{Zs}\u0027])', firstnm) AS regexFirst, 
		dbo.regexMatch('([^\p{Lu}\p{Ll}\p{Pd}\p{Zs}\u0027\.])', lastnm) AS regexLast
FROM	staging.people)
SELECT *
FROM badNames
WHERE regexFirst = 1 OR regexLast = 1
ORDER BY modified DESC;


/*
This example is for a method still in development that returns a row vector of double valued 
string distance estimates/metrics.

SELECT * 
FROM [dbo].allDistanceMetrics('this', 'that');

*/
