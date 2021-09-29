Notes:
*****
1. There are two solutions in the folder. (API and UI)
2. API is build using .net core 3.1 
3. UI is build using angular 10.
4. API is configured with swagger for testability. (CRUD operations are implemented)
5. Database is build on mongoDB as data nature is unstructured.
6. Data dump used has low data quality. Therefore, a single word search can return multiple list based on usage i.e. adjective,noun etc.

Instructions:
*************
1. Unzip the folder.
2. Open both the solutions individually using Visual Studio.
3. Run each solution.
4. UI will open a bit slow for first time.
5. Enter a word and click search.
6. "Get All" button return top 10 records from the database.

Bugs\Incomplete:
***************
1. GetAll feature is not working properly i.e. service is called but UI do not update.
2. Add feature is not added on the UI due time boundation and work committments.



