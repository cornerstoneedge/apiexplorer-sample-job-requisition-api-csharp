User Guide
---------------------------------------------
App.config - User has to enter the values of the below mentioned keys in the app.config file.

<appSettings>    
	<!--Enter the client id from the portal.-->
    <add key="ClientId" value="" />
	<!--Enter the client secret from the portal.-->
    <add key="ClientSecret" value="" />
	<!--Default grant type is client_credentials-->
    <add key="GrantType" value="client_credentials" />
	<!--Default scope is all-->
    <add key="Scope" value="all" />
	<!--Enter the URL for the Service-->
    <add key="ServiceURL" value="https://[portal].csod.com/services/api/Recruiting/JobRequisitionDetails?lastModifiedSince=2019-04-17" />	
</appSettings>
   

Dev Guide
--------------------------------------------
OAuth2.cs - Using this class to Get the Access Token.

EdgeApi.cs - Using this class to Call the Api.

Util.cs - Using this class to Build Request Parameters.

Portal.cs - This class contains the properties like  ClientId, ClientSecret, GrantType , Scope and ServiceURL. And get the values from the app.config file.

App.config - This configuration file contains the values of the keys ClientId, ClientSecret, GrantType , Scope and ServiceURL. Users has to set the values according to the portal they are looking for.

Program.cs - This is the execution class to call the OAuth2 Get Access Token and return the token and corresponding details.

HttpClientHelper.cs - This is a helper class which sending HTTP requests and receiving HTTP responses from a resource identified by a URI. Extension of the HTTPClient class.

HttpClientParameters.cs - Defined the properties used in HttpClientHelper class.

CommonExtensions.cs - Extension class for strings and common methods like Split, ContainsAny, Join, ConvertToCommaDelimitedString and ConvertToDelimitedString etc.

