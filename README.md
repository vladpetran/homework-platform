# homework-platform

1.	High level functionality
    a.	User sign-up
    b.	User login
    c.	Role based functionality
    d.	Administration module
    e.	File upload
    f.	Email notifications
    g.	Reporting
2.	Technologies
    a.	MVC / Webforms (any or both)
    b.	Database
        i.	MSSQL
        ii.	Stored Procedures / Functions
        iii.	ADO.NET access (no ORM)
    c.	UI
        i.	Bootstrap
        ii.	jQuery
        iii.	CSS
    d.	Backend
        i.	Custom User/Roles module (no ASP.NET Identity)
        ii.	Repository Pattern
        iii.	Layered Architecture (DataAccessLayer, BusinessLayer, PresentationLayer)

I.	Project description
Create an Education Portal which allows communication between students and teachers. 

Definitions
Roles:
	Admin 
	Teacher
Student 
Landing page:
	First page presented after login
Homework:
	Collection of assigned work for students 
Grade:
	Used for homework grading

Requirements
Users/Roles
-	Admin:
-	User with elevated privileges
-	Creates Teacher accounts
-	Assigns students to teachers
-	Resets user passwords
-	Teacher
-	Creates and assigns homework for students
-	Reviews and grades homework uploaded by students
-	Send email notifications to students
-	Can pull reports based on students assigned to him/her
-	Can add comments to homework and reject/accept homework
-	Can view all students assigned to him (including student information)
-	Student
-	Can only be created through sign-up process 
-	Can choose his teacher at sign-up
-	Has to confirm account using email address
-	Can upload files for homework
-	Can receive email notifications
-	Can pull reports regarding his/her own account (grades, pending homework)
Homework
-	Can contain multiple uploaded files
-	File types should be parsable text (txt, xls, cs…)
-	Can be viewed by teacher in browser
-	Can contain comments added by teacher
-	Has one of these statuses: Uploaded, Commented, Accepted, Rejected depending on last action
-	Can only be accessed by owner (student and teacher to whom student belongs to)
-	Has a due date (after the due date is reached and teacher has not accepted the homework it becomes Rejected and student looses grade)

Homework review
-	Teacher can add comments to an upload document
-	If teacher adds comments the student is notified and homework status changes to Commented
-	Student has to act on comments and re-upload the document
-	Teacher accepts or rejects the homework
Landing page
-	Is different for each role
-	Each role should have quick access to most common features from this page
-	Should always display current logged in user
-	Should contain only information accessible to current logged in user (based on role)

Homepage
-	Public page with sign-up / login options
-	Should be the only page accessible without being logged in 

Reports
-	Data can be viewed on the portal
-	Data can be exported into either PDF or XLS (or both) format
-	Data should be restricted by user / role access
-	Ex: 
-	for Teachers: all pending homework / all assigned student grades, top 10 students by grade...
-	For Students: all grades...
