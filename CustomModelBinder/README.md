# Custom Model Binder
## Short Description
Add custom model binders for ASP.NET Core MVC application.
## Topics
 - ASP.NET Core
 - WebApi apps
 - Routing
 - Model Binding
 - Testing and Debugging
## Requirements
 - Create custom model binder that allows to accept the requests like `http://localhost:52691/api/location?coord=1,2,3`.
 - The action should return the `Point` object with corresponding x, y, z coordinates filled from the request.
 - The Action result should be in json format.
 - Create a Person model with some arbitrary properties, including identifier as following `public Guid Id {get;set;}`.
 - Create another custom model binder that allows to accept the requests like `http://localhost:52691/api/person/TWFuIGlzIGRpc` where `TWFuIGlzIGRpc` is a base-64 encoded person identifier.
 - The Action result should return json with the `Person` object with decoded identifier property.