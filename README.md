Project 1.5 
	- Requirements:
		- MVC frontend with some dynamic behavior in JS
		- REST service for all data access, backed by
			SQL ServerDB
		- Seperation of conserns in REST service
		- Users with different roles/privilages 
		- Data/interaction model, views, DB all at least
			as complex as Project 1
		- Async DB and HTTP access
	
	- Project Structure
		- MVC Application solution
			- Uses REST service solution 
		- RESTService solution
			- WebApp project
				- References DataAccess / Library
			- Library project
				- No references
			- DataAccess project
				- References SQL server
				
	- Presentation on Thursday, January 3rd.
	
Music Library Description:
	
	Music Management system that enables users to play and organize music.
Using a SQL Server hosted on Azure, we will store a database of songs that 
will be updated through Admin users. Songs will then link to corresponding 
Youtube pages where the music can be played. Users will be able to search 
the database and sort songs based on song name, artist, and album. They will 
also be able to store a list of their favorite songs.

	
Team-418 Members:
	- John Pot
	- Justin Sy
	- Devin Rouse
			
			
			
			
			
			
			
			
			
			
			
			
			
			