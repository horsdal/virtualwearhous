# Introduction #

The Unicef ChildMed web application is built on [ASP.NET MVC](http://www.asp.net/mvc) with a dependency on two additional .dlls (found in UnicefVirtualWarehouse/UnicefVirtualWarehouse/bin) and [MS SQL Server Express](http://www.microsoft.com/express/Database/) or above.
To run the application a [Windows Server](http://www.microsoft.com/windowsserver/) with [IIS](http://www.iis.net/) and a MS SQL Server is required. The SQL Server can (obviously) be on a separate server.

# Staging #
The staging site is at http://childmedtest.apphb.com/ and is hosted through [AppHarbor](https://appharbor.com/) (an ASP.NET oriented [PaaS](http://en.wikipedia.org/wiki/Platform_as_a_service) solution on top on [Amazon EC2](http://aws.amazon.com/ec2/).

AppHarbor uses [git](http://git-scm.com/) for publishing. The model is that you push your git repository master up to AppHarbor which then compiles and deplaoys the application automatically.

To publish the application to the staging environment setup a local git repository in the root directory of the web application (UnicefVirtualWarehouse/UnicefVirtualWarehouse/):
```
    git init
    git add . 
    git commit -m "Initial commit"
```
The git add command adds all files, except for those mentioned in .gitignore.
When the local git repository is up and running setup the staging repository at AppHarbor as a remote repository:
```
    git remote add appharbor http://[username]@localhost:1824/childmedtest.git
```
Lastly, to publish push your local repostory to AppHarbor:
```
    git push appharbor master
```

To publish further modifications first commit all changes to the local git repository and then push to AppHarbor.

## Accessing the database ##

The staging database can be reached with MS SQL Studio Express using the username/password provided by AppHarbor.

## Configuring connection strings ##

The connections string used by everything in the application except the logger is the connection string called unicefvirtualwarehouse found in the web.config. This connection string is automatically replaced with the connections string for the AppHarbor database of the same name by the AppHarbor deployment pipeline.
The connection string for the logger needs to be set up manually in the nlog.config file, by going to the AppHarbor dashboard and copying the appropriate connections string. NB Do not commit nlog.config with the AppHarbor connection string to SVN: If you do that it will be publically available.


# Production #

No production environment has been chosen yet.