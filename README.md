# CustomerManagement.API

Manual Testing Process:

IISExpress:
1. Run an instance of Visual Studio (.NET 6.0 Framework must be installed beforehand)
2. Open CustomerManagement.API.sln so that projects are visible
3. Run application either in Release or Debug mode using IISExpress (HTTPS certificate must be created and trusted).

Docker:
1. CustomerManagement.API is supplied with a Dockerfile (configured for Windows container) so that one can use Docker Desktop to load Image and run Container.
2. Please refer to the Docker Desktop UI to get exposed HTTP and HTTPS port.
3. Once a Docker Container for CustomerManagement.API is running, please use the next URI template to load Swagger UI: {Scheme}://{ServiceHost}:{ServicePort}/swagger.
4. It is important that the corresponding HTTPS sertificate is trusted.

API Capabilities:
1. Once Swagger is visible, make a request to the anonymous GET /api/JwtToken endpoint writing preferable Name and Surname of the current user.
2. Copy JWT Token value from the response (ommiting double quotes) and supply Swagger with it pressing "Authorize" button in the upper right corner.
3. Press "Authorize" button so that swagger start to pupulate Bearer token in every response's header.
4. "newUserGuid" value from the same response may be used for the "customerUid" parameter in POST "/api/UserFinancialData/Account/User" request.
5. GET "/api/UserFinancialData" and POST "/api/UserFinancialData/Account/CurrentUser" will take the same "customerUid" from the token claims.
 
   
