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
	
Team-418 Members:
	- John Pot