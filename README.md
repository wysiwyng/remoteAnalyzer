remoteAnalyzer
==============

a project to examine the possibilities of remote analyzing computers via c#, php and mysql. note it's not intended to maliciously track an user's activity, and shouldn't be used for this anyway. (we're not EA here) so only give persons the targetClient if they know what you're sending them there, as this program can, in it's final form, analyze & access the whole computer it's running on.


compiled using VS2012 and .NET Framework 4.5 when using the original solution file

compiles with mono 2.10.8+ and monodevelop 3.0.3.2+ when using the mono solution file


usage
-----

1. set up a web server with php and mysql, create the tables using the non-existing create_mysql_tables script
2. load the vs solution and compile
3. send somebody the targetClient
5. start operatorClient program/project
6. enter the url to the above server
7. use some of the commands you find in the main routine
8. ???
9. profit!
