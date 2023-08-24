# CustomerManagement.API

Manual Testing Process (for developers):
1. Run an instance of Visual Studio (.NET 6.0 Framework must be installed beforehand)
2. Open CustomerManagement.API.sln so that projects are visible
3. Run application either in Release or Debug mode using IISExpress (HTTPS certificate must be created and trusted) and Chrome browser.
4. Once Swagger is visible, make a request to the anonymous GET /api/JwtToken endpoint writing preferable Name and Surname of the current user.
5. Copy JWT Token value from the response (ommiting double quotes) and supply Swagger with it pressing "Authorize" button in the upper right corner.
6. Press "Authorize" button so that swagger start to pupulate Bearer token in every response's header.
7. "newUserGuid" value from the same response may be used for the "customerUid" parameter in POST "/api/UserFinancialData" request.
8. GET "/api/UserFinancialData" and POST "/api/UserFinancialData/currentUser" will take the same "customerUid" from the token claims.
   
