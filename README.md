# UserManager

Sample ASP.NET Web API for manipulating user entities.

You can create JWT tokens with the `dotnet user-jwts` command to test operation in development mode.

Instead of using the traditional controller based approach, went with the Minimal API. To make mappings easier, I chose using FastEndPoints library. I had not experience with this previously, wanted to try it out. It worked out pretty well. I mainly like that you can separate responsibilities better than you can do with a controller (one class for one operation), although the underlying layers (`IUserService`, `IUserRepository`) still have that all in one approach as the controllers would.

### Design choices

Address and company entities are handled as attached ones of the user and not standalone ones.

### Known issues, shortcomings, areas to improve
- Enhance JSON parsing in order to be able to enforce non nullable properties all the way (`User.Name`, `Address.City`, etc.)
- Enhance database (add constraints, add sequence to be able to use auto-increment numbers as identifier)
- Separate layer of objects for the database models
- Validation logic could be more centralized and enhanced
- Fine tune the Swagger documentation (serialization of objects into JSON)
