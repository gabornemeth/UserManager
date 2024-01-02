[![Build Status](https://nrglabz.visualstudio.com/UserManager/_apis/build/status%2Fgabornemeth.UserManager?branchName=main)](https://nrglabz.visualstudio.com/UserManager/_build/latest?definitionId=20&branchName=main)

# UserManager

Sample ASP.NET Web API for manipulating user entities.

You can create a JWT token with the `dotnet user-jwts --audience usermanager-api` command.

Instead of using the traditional controller based approach, went with the Minimal API. To make mappings easier, I chose using FastEndPoints library. I had not experience with this previously, wanted to try it out. It worked out pretty well. I mainly like that you can separate responsibilities better than you can do with a controller (one class for one operation), although the underlying layers (`IUserService`, `IUserRepository`) still have that all in one approach as the controllers would.

## Design choices

Address and company entities are handled as attached ones of the user and not standalone ones.

### Further improvements
- constraints in the database
- separate layer of objects for the database models
- validation logic could be more centralized and enhanced
- fine tune the Swagger documentation (serialization of objects into JSON)
