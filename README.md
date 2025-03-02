Project title: A VETTED MEMBERSHIP COMMUNITY FOR FEMALES IN SPORT & ENTERTAINMENT
This projects highlight the key features for women to take participation in different sports and entertainments filed by building strong industry connections

Installion process:
To run this project the one must have the visual studio 2022 and Microsoft server management studio for runnung the application and seeing data storing
Install both software from google chrome browser
Make sure you have install all .netframeworks(e.g .netframeworksCoreentity,.netSQLServer) in visual studio 2022 in order to run the application 
How to use or run:
After installing the visual studio 2022 click on run button by selecting option of" IIS Express" from the run dropdown menu.
Go to laptop search bar and search for SQL Server installation 2022 > Go to installation > install microrsoft mangement sql studio and install all server data tools and mangement tools displayed on screen 
After that connect to the database and apply migration in visual studio by writing:
Apply-Migration "Init"
and then
Update-Database
and then run the program 
Before applying the above step go to the appsettings.json in visual studio opening the provided code file and copy this below code at the last line where this provided sentence is written  "osprey_web_appContextConnection":"Server=RUDRA;Database=master;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True;"

Task to do in MIcrosoft mangement studio:
For seeing where the data base store you have to simply run the some different queries by clicking new queries on the top side attach beloe:
1.select*from AspNetUsers
2.select*from UserWorkshops
3.select*from Mentors
4.select*from Workshops
5.select*from AspNetUserLogins



 


