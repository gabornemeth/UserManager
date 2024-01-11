[![Build Status](https://nrglabz.visualstudio.com/UserManager/_apis/build/status%2FUserManager%20API?branchName=main)](https://nrglabz.visualstudio.com/UserManager/_build/latest?definitionId=20&branchName=main)
[![Build Status](https://nrglabz.visualstudio.com/UserManager/_apis/build/status%2FTokenGrabber%20webapp?branchName=main)](https://nrglabz.visualstudio.com/UserManager/_build/latest?definitionId=21&branchName=main)

# UserManager

Sample ASP.NET Web API for manipulating user entities. The sample dataset is available [here](https://jsonplaceholder.typicode.com/users)

### Design choices

The identifier of the user has been changed to string instead of integer, because I could not achieve neither with the simple MongoDB management tools (Mongo Express, MongoDB Compass) nor with the MongoDB .NET Driver to setup a sequence in the database and grab a value from it before inserting a new entity. With a workaround in the DAL integer identifiers have been working - can check it in the [feature/integer-id branch](https://github.com/gabornemeth/UserManager/blob/feature/integer-id/UserManager/Mongo/MongoUserRepository.cs#L104-L121) - but because the goal was to keep the app as close to production ready as possible, rather switched to using string (backed by Object ID in MongoDB).

Address and company entities are handled as attached ones of the user and not standalone entities. Companiy could be treated as a standalone type as well with a redesign (separate db collection, add identifier, etc.).

No separate set of objects has been created for the database models (that would be the clean architecture on paper, but as we can work with this structure equally well as the main models inside the app, we can skip this step in a pragmatic way).

No separate delete permission has been added, because the bad guy/girl with the write scope can still to erase the user data (only can't wipe out the whole entity).

Instead of using the traditional controller based approach, went with the Minimal API. To make endpoint mapping easier, I chose using [FastEndPoints](https://fast-endpoints.com/) library. I had no experience with this previously, wanted to try it out. It worked out pretty well, overall experience is positive. I mainly like that you can separate responsibilities better than you can do by using the MVC approach (one class for one operation), although the underlying layers (`IUserService`, `IUserRepository`) still have that all in one approach as the controllers would.

API is documented with Swagger (/swagger url) and also can be tried out there in action.

### Try out, test, develop

The main solution that you should open with Visual Studio for example is [UserManager.sln](https://github.com/gabornemeth/UserManager/blob/main/UserManager.sln).

You have to have Docker Desktop installed and started (it is needed in order to make the database tests working as well). Then you can spin up the MongoDB backend by using [mongo-docker-compose.yaml](https://github.com/gabornemeth/UserManager/blob/main/mongo-docker-compose.yaml). It has a basic setup for development.
```
 docker-compose -f mongo-docker-compose.yaml -p usermanager-api-dev up
```

Then you have to setup the following secrets - filled in the Mongo connection properties according to the sample [mongo-docker-compose.yaml](https://github.com/gabornemeth/UserManager/blob/main/mongo-docker-compose.yaml).

Authority (issuer) and audience has to match with the ones used for `dotnet user-jwts`
```json
  "MongoDB": {
    "ConnectionString": "mongodb://admin:p4ssw0rd@localhost:27017",
    "Database": "usermanager-db"
  },
  "JwtBearer": {
    "Authority": "",
    "Audience": ""
  },
```
You can use `dotnet user-jwts` to generate test tokens in the UserManager folder. You can learn about using this tool [here](https://learn.microsoft.com/en-us/aspnet/core/security/authentication/jwt-authn?view=aspnetcore-8.0&tabs=windows).
You should use something like the following for generating a token allowing you to read and write data.
```
dotnet user-jwts create --audience <audience> --issuer <authority> -scope "read:users write:users"
```
Don't forget to match the issuer in the `appsettings.json` file as well.

[NUKE](https://nuke.build/) build has been setup to make easier the code coverage report generation. If you have any issue with making NUKE work, please check the link.

Nuke uses [Coverlet](https://github.com/coverlet-coverage/coverlet) and [ReportGenerator](https://github.com/danielpalme/ReportGenerator) under the hood.

Basically you have to build and run the [_build](https://github.com/gabornemeth/UserManager/tree/main/build) project (report generation is configured as the default target) either from the development environment, or from command line. In the command line you have to use `build.ps1` PowerShell script on Windows or `build.sh` on Linux/macOS.

Code coverage report is saved into the `CodeCoverage` folder (you will find both xml and html versions).

### Moving towards production

For a more advanced scenario I have setup a  [very simple web application](https://github.com/gabornemeth/UserManager/tree/main/TokenGrabber) [running in Azure](https://tokengrabber.azurewebsites.net) based on the ASP.NET MVC Sample downloaded from [Auth0](https://www.auth0.com) to be able to get JWT access tokens relatively easily. Under the hood the API and this web application has been setup in the Auth0 ecosystem. This approach has the big benefit of being able to use these tokens in development mode and also running the API in a Docker container. If the tokens generated by the `dotnet user-jwts` or similar tools, it can be securely used only in development.

You can build a Docker image with the API, through its Dockerfile. I have setup a basic CI/CD pipeline (see the badges at the top), tests run as part of it, both the API and the TokenGrabber webb app are deployed into Azure.
- [UserManager API](https://usermanager.azurewebsites.net/swagger/)
- [TokenGrabber web app](https://tokengrabber.azurewebsites.net/) - It starts very slow if has been stopped due to inactivity. Can get token to test with the user/pass: `usermanager-api-test@nrglabz.com/T3stUs3r2024`

### Known issues, shortcomings, areas to improve
- Enhance JSON parsing in order to be able to enforce non nullable properties all the way (`User.Name`, `Address.City`, etc.)
- Add tests for specific validation errors, not just checking bad requests (if we want to give hints to the client about what was wrong).
- Internal server error handling (e.g. with displaying a more generic error message, not revealing the stack trace of the Exception - has been enabled for production builds)
- Add more integration tests for the endpoints (having the underlying architecture in place)
- Add traits for the tests to better categorize them
- Test whether the right scope checking policy have been applied to the endpoint
- May add tests for the Swagger documentation Summary classes
- Fine tune the Swagger documentation (serialization of objects into JSON)
- Add telemetry, logging
- Enhance database (add constraints, add sequence to be able to use auto-increment numbers as identifier if this is a must)
- Validation logic could be more centralized and enhanced.
- No need to separately build the app in the CI/CD pipeline, executing the NUKE build does this as well.
- rewrite Git log to remove all traces of accidentally commited sensitive data :)
