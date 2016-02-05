# Why Won't SQL Server Implement Regex Natively
Like the heading above, I found myself asking this question recently when I found that SQL Server doesn't have native regular expression functions.  It took me as a bit of a surprise after working with Oracle and Postgres RDBMS; it was even more surprising to see the number of articles/blog posts by Microsoft staff on how to build out Regular Expression functions using the common language runtime integration.  So, I decided to put together a few different functions that I thought might be helpful/useful to others that should also make it a bit easier to get up and running with things quickly.  

# Build Info
Our SQL Server instance is 2008 R2 and the server uses v 2.0 of the .Net framework.  I don't imagine it would be difficult to compile against newer versions of the CLR, but I would have no way of testing this.  

# Design Info
The code is organized with a single class containing static methods used to implement the functions/methods of interest.  Each method in the SQLRegex class references classes from which the method is implemented.  This helps when the static method is cached and subsequent calls fail to initialize a new object.  However, using the safe assembly access setting it is not possible to initialize static members in the classes that get created by the method calls.  

# Additional Work/TODO
In addition to some basic regular expression and string processing methods, I'm also going to work on integrating some string distance metrics to provide fuzzy matching algorithms/access.  
