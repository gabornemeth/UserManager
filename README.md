[![Build Status](https://nrglabz.visualstudio.com/UserManager/_apis/build/status%2FUserManager%20API?branchName=main)](https://nrglabz.visualstudio.com/UserManager/_build/latest?definitionId=20&branchName=main)
[![Build Status](https://nrglabz.visualstudio.com/UserManager/_apis/build/status%2FTokenGrabber%20webapp?branchName=main)](https://nrglabz.visualstudio.com/UserManager/_build/latest?definitionId=21&branchName=main)

# UserManager

Sample ASP.NET Web API for manipulating user entities. The sample dataset is available [here](https://jsonplaceholder.typicode.com/users)

### Design choices

The identifier of the user has been changed to string instead of integer, because I could not achieve neither with the simple MongoDB management tools (Mongo Express, MongoDB Compass) nor with the MongoDB .NET Driver to setup a sequence in the database and grab a value from it before inserting a new entity. With a workaround in the DAL integer identifiers have been working - can check it in the feature/integer-id branch - but because the goal was to keep the app as close to production ready as possible, rather switched to using string (backed by Object ID in MongoDB).

Address and company entities are handled as attached ones of the user and not standalone entities. Companiy could be treated as a standalone type as well with a redesign (separate db collection, add identifier, etc.).

No separate delete permission has been added, because the bad guy/girl with the write scope can still to erase the user data (only can't wipe out the whole entity).

Instead of using the traditional controller based approach, went with the Minimal API. To make endpoint mapping easier, I chose using FastEndPoints library. I had no experience with this previously, wanted to try it out. It worked out pretty well, overall experience is positive. I mainly like that you can separate responsibilities better than you can do by using the MVC approach (one class for one operation), although the underlying layers (`IUserService`, `IUserRepository`) still have that all in one approach as the controllers would.

API is documented with Swagger (/swagger url) and it can be tried out there too.

### Try out, test, develop

You should have Docker Desktop installed. Then you can spin up the MongoDB backend by using mongo-docker-compose.yaml. It has a basic setup for development.
Then you have to setup the following secrets.
```json
  "MongoDB": {
    "ConnectionString": "",
    "Database": ""
  },
  "JwtBearer": {
    "Authority": "",
    "Audience": ""
  },
```
You can use `dotnet jwts-user` to generate test tokens.

For a more advanced scenario I have setup a very simple application (TokenGrabber) [running in Azure](https://tokengrabber.azurewebsites.net) based on the ASP.NET MVC Sample downloaded from [Auth0](https://www.auth0.com) to be able to get JWT access tokens relatively easily. Under the hood the API and this web application has been setup in the Auth0 ecosystem. This approach has the big benefit of being able to use these tokens in development mode and also running the API in a Docker container. If the tokens generated by the `dotnet user-jwts` or similar tools, it can be securely used only in development.
You can build a Docker image with the API, through its Dockerfile. I also have configured a compose.yaml to start a container from the built docker image along with the MongoDB backend, but have not commited it to the repository, and the issuer and audience of the JWT tokens have to be set too. I have setup a basic CI/CD pipeline (see the badges at the top), tests run as part of it, TokenGrabber is deployed, the API is not yet deployed.

### Known issues, shortcomings, areas to improve
- Enhance JSON parsing in order to be able to enforce non nullable properties all the way (`User.Name`, `Address.City`, etc.)
- Add tests for specific validation errors, not just checking bad requests (if we want to give hints to the client about what was wrong).
- Internal server error handling (e.g. with displaying a more generic error message, not revealing the stack trace of the Exception)
- Add more integration tests for the endpoints (having the underlying architecture in place)
- Add traits for the tests to better categorize them
- Test whether the right scope checking policy have been applied to the endpoint
- May add tests for the Swagger documentation Summary classes
- Add telemetry, logging
- Enhance database (add constraints, add sequence to be able to use auto-increment numbers as identifier)
- Separate layer of objects for the database models (that would be the clean architecture on paper, but as we can work with this structure equally well as the main models inside the app, we can skip this step in a pragmatic way)
- Validation logic could be more centralized and enhanced
- Fine tune the Swagger documentation (serialization of objects into JSON)
- Push the build Docker image to Azure Container repository and start it with the configuration supplied in Azure
- rewrite Git log to remove all traces of accidentally commited sensitive data :)