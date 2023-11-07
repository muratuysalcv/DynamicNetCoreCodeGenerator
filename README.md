# DynamicNetCoreCodeGenerator
It is a project that allows you to code the API project with .NET Core and download it as "Source Code" after you enter the connection information of your database.

By creating more than one API in the system, it makes it possible to code multiple API requirements in a single project by connecting to more than one database.

Working Structure
After the database connections defined in the system, it first connects to the databases and reads the tables with the "Build" button.

Then, the Entity Framework generator structure is run in the background and classes are created.

The created classes are generated to work with the generic Data Access model of the system and Repository classes are also coded.

Each system is also encoded with "Controller" elements that can be accessed via the /api/{DB Name}/{Table Name} extension.

The prepared Controller, Entity Classes and Db Connection information are replaced with generic text template files and the replaced files are collected in a single folder.

As a result, the API project written in .NET Core of more than one database in a single project is downloaded as .rar by providing only database connection information.

Since the downloaded project is source code, the necessary improvements can be made on the code.
