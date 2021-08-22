# Sprout_AspNetCore_React_Exam Entry from Joshua D. Siuagan

## Things I have changed so far:

### - Edited Solution Project Structuring to a Clean Code Architecture.
### - Applied CQRS + MediatR on Accessing data or doing some task to make things neat on Controller Part.
### - Applied some Input Validations using FluentValidation
### - Applied AutoMapper for Mapping from one object to another.
### - Edited the PUT & POST Request of Frontend to alert the user what error occured to proceed on adding/updating.
### - Included Unit testing using xUnitTest for .Net Core + FakeItEasy for mocking purposes on test case scenarios.
### - Updated the DbContext Table naming conventions and Table Dataypes and Relationships.
### - Included an Update ".bak" Data inside "Sprout.Exam.Webpp/Sql_Database_Updated_Backup/Sprout_Updated_Db.bak" Since I have edited the Naming and Table Relationships on Context and I also add new migration for me to run update-database.


## Instruction to Run this:

### - Clone this Project.
### - Restore the Database Backup from "Sprout.Exam.Webpp/Sql_Database_Updated_Backup/Sprout_Updated_Db.bak".
### - On Sprout.Exam.WebApp go to "appsettings.json" and change the value of "ConnectionStrings" to your preffered connectionstring on where you restore the given database. The database name is "SproutExamDb"
### - Make sure to open SSL on Asp.Net Core Settings 
### - Make sure to have an LTS Node js installed since this have a react app inside of it. 
### - Run it and Login as Username: "sprout.test@gmail.com" and for Password: "P@$$word6".


# Enjoy!!


## You can contact me via email: "joshuasiuagan0406@gmail.com" for more questions.
