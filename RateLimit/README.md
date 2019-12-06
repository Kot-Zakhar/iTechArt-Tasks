# Rate Limit #
## Short Description ##
Add an attribute to limit the number of concurrent requests to Action.
## Topics ##
- ASP.NET Core
- Async methods
- Dependency Injection
- Sorting, Filtering and Paging
- Logging
## Requirements ##
- Create a `Profile` object that will contain the following properties:
  - Id (Guid)
  - FirstName
  - LastName
  - Birthday
- Create a `json-file` with a collection of profiles.
- Create a `ProfileService` class that will create a collection of profiles from json file and return it with paging, filtering and sorting.
- Use Dependency Injection to register class as a single instance.
- Inject it into the controller.
- Implement Action method that will call the `ProfilesService` to get profiles. The Action should support paging, filtering and sorting.
- Create `Profiles.cshtml` view that will have a simple table with the list of profiles with paging, filtering and sorting.
- Implement custom action attribute that will restrict the number of concurrent requests to the Action. Make it configurable:
  - Developer should be able to specify the amount of allowed concurrent requests.
  - Return `429 (Too Many Requests)` when the number of requests is higher then the specified limit.
- To ease the implementation add some delay in the `ProfilesService` class or in the Action.