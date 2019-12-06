# Custom Json Formatter #
## Short Description ##
Add custom JSON formatter to ASP.NET Core MVC application.
## Topics ##
- ASP.NET Core
- MVC apps
- WebApi apps
- Controllers, Views, Models
- Dependency Injection
- Startup class
- Testing and Debugging
## Requirements ##
- Create next API endpoints:
  - `/api/article GET`
  - `/api/article/{id} GET`
  - `/api/profile/{id} GET`
- The user should be able to use:
  - default Json Formatter using `application/json` Accept header,
  - custom Json Formatter using `application/json+cutom` Accept header.
- The formatter should be configurable via extending `IMvcBuilder` and added in `ConfigureServices` method.
- The user will make a request to WebApi with Accept header set as `application/json+custom`. E.g.
    ```
    GET /api/article HTTP/1.1
    Accept: application/json+custom
    ```

- The response should contain the json-model of the requested object plus dictionary of links. Dictionary of links should contain next elements:
  - `self` - i.e. link to item itself.
  - `get-author` will be available on articles. It will contain link to author profile. E.g., given model is
    ``` c#
    public class ArticleModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get;set; }
        public int AuthorId { get;set; }
    }
    ```
    The json from the response will look like
    ```json
    {
        "data": {
            "id": 1,
            "title": "News about John Doe",
            "description": "lorem ipsum dolor sit amet",
            "authorId": 1
        },
        "_links": {
            "self": "http://localhost:52691/api/article/1",
            "get-author": "http://localhost:52691/api/profile/1",
        }
    }
    ```

- The user should be able to use default Json Formatter in advance. So json from the response will look as usual without additional links when Accept header is being set to   `application/json`. E.g.
    ```json
    {
        "id": 1,
        "title": "News about John Doe",
        "description": "lorem ipsum dolor sit amet",
        "authorId": 1
    }
    ```
- Feel free to choose the way to store initial data.
- Use Fiddler or Postman to send requests to WebAPI.