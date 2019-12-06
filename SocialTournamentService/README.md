# Social tournament service
## Short Description
As a gaming website we want to implement a tournament service.
Each player holds certain amount of bonus points. Website funds its players with bonus points based on all kind of activity. Bonus points can be traded to goods and represent value like real money. One of the social products class is a social tournament. This is a competition between players in a multi-player game like poker, bingo, etc). Entering a tournament requires a player to deposit certain amount of entry fee in bonus points. A winner is determined by a service and gets all bonus points submitted to tournament's deposit.
## Requirements
### Goal
 - Create a RESTful web server with a social tournament service implementation.
 - User info:
   - id
   - name
   - balance
 - Tournament info:
   - id
   - name
   - deposit
   - prize
   - users
   - winner
### Constraints
 - bonus points balance ≥ 0
 - deposit > 0
### API endpoints
The API should have the following endpoints:
 - Create new user
    ```JSON
    POST /user
    Request:
    {
        "name" : ​name
    }
    Response:
    {
        "id": 1
    }
    ```
 - Get user info
    ```JSON
    GET /user/{id}
    Request: empty
    Response:
    {
        "id": 1,
        "name" : ​name,
        "balance": 700
    }
    ```
 - Remove user
    ```JSON
    DELETE /user/{id}
    Request: empty
    Response: empty
    ```
 - Take user bonus points (subtract)
    ```JSON
    POST /user/{id}/take
    Request:
    {
        "points" : ​300
    }
    Response: empty
    ```
 - Add bonus points to user
    ```JSON
    POST /user/{id}/fund
    Request:
    {
        "points" : ​400
    }
    Response: empty
    ```
 - Create a tournament
    ```JSON
    POST /tournament
    Request:
    {
        "name" : ​name,
        "deposit": 1000
    }
    Response:
    {
        "id": 1
    }
    ```
 - Get tournament info
    ```JSON
    GET /tournament/{id}
    Request: empty
    Response:
    {
        "id": 1,
        "name" : ​name,
        "deposit": 1000,
        "prize": 4000,
        "users": [ 1, 2, ...],
        "winner": 0
    }
    ```
 - Join tournament
    ```JSON
    POST /tournament/{id}/join
    Request:
    {
        "userId" : ​13
    }
    Response: empty
    ```
 - Finish tournament
    ```JSON
    POST /tournament/{id}/finish
    Request: empty
    Response: empty
    ```
 - Cancel tournament
    ```JSON
    DELETE /tournament/{id}
    Request: empty
    Response: empty
    ```