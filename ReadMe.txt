
-------------------------Artikel/Product Store Solution ReadMe---------------------------------

Solution Name : SOLID

For this solution, I have added .NET core MVC as well. From where User can upload CSV File manually.
I need to make UI, Because I wanted to upload Big CSV file for processing. That I have generated manually.

---------------------------------------------------------------------------------------------------------------------------

Solution Layers:
1) UI Layer (Optional)
Project Name: SOLID.MVC

2) .NET Core Web API Layer
Project Name: SOLID.API

3) Business Layer

	Class Library 
	1) SOLID.IRepository
	2) SOLID.Repository
	
4) Data Layer
Class Library: SOLID.DAL

5) Common Layer
Class Library: SOLID.Common 
For all Common models/Entity, which are used in multiple projects

---------------------------------------------------------------------------------------------------------------------------

As part of This project Implementation, I have implemented following points
1) Implemented SOLID priciples
2) Implemented .NET Core Dependency Injection
3) Created MS SQL Server Database
4) Craeted JSON file in same 'API' project Folder
5) Transformed CSV Data into Logical Model
6) Normalization of CSV data

---------------------------------------------------------------------------------------------------------------------------

Normalization:

Tables:
1) Artikle
2) Categories (Q1)
3) ColorCode
4) Color
---------------------------------------------------------------------------------------------------------------------------

=> Database First Approach is Used for this implementation
=> Project implementation tested with CSV file with 70MB size and 950,000 rows. It work properly.
=> To Run the project, Run the Solution in 'Multiple Startup' projects
1) SOLID.MVC
2) SOLID.API








