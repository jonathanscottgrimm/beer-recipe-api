INSTRUCTIONS ...

To make this API work, you need to set the DB up. To do this: 

1) Open the sql migration script "BeerAppDbMigrationScript" in the "SqlMigrationScripts" folder in SSMS. 

2) Edit the FILENAME property (there are two of them and they are demarcated by comments) to the path you wish to save the db to.

3) Execute the script. 

4) Open the file appsettings.json.

5) Change the connection string for "BeerAppDbContext" to the one pointing to the database you just created. (I'm assuming you know how to find out what the connection string is...).

That should work. If it doesn't, please let me know and I'll try to troubleshoot... 

Jonathan Grimm
202-210-8931
jonathanscottgrimm@gmail.com