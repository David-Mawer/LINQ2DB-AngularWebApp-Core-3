# This is a .Net Core 3.1 (Angular 9 + API) Template for Linq2Db

This is the basic Angular WebApp template for **.Net Core 3.1** with a few tweaks: -

 - The ORM tool is **Linq2DB**
 - **Identity tables don't have the default names** - these can be changed.
 - Linq2DB is fully integrated into the project : -
	 - the default appsettings.json connection string is used to configure Linq2DB
	 - The T4 class generation is installed and configured
	 - Database is SQL Server

# Instructions

 1. Clone or download the project.
 2. Run the "Create Database" script on you local SQL Server instance.
 3. Edit appsettings.json to point to the same server instance where you created the database.
 The project should now run (if the database connections are correctly set-up) .
 
 4. After confirming that the project runs, you can edit the T4 generation file `DB\LinqDB.tt` to point to the new database.
 5. Now you can continue with the Angular - API project developing whatever you had in mind in the first place (See Startup.cs and DemoController for tips on accessing the database).
