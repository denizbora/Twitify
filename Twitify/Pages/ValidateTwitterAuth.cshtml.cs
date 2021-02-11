using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SpotifyAPI.Web;
using Tweetinvi;
using Tweetinvi.Auth;
using Tweetinvi.Parameters;
using Twitify.Data;

namespace Twitify.Pages
{
    public class ValidateTwitterAuthModel : PageModel
    {
        public async Task<RedirectResult> OnGet()
        {
            var appClient = new TwitterClient(TwitifyController.ConsumerKey, TwitifyController.ConsumerSecret);

            // Extract the information from the redirection url
            var requestParameters = await RequestCredentialsParameters.FromCallbackUrlAsync(Request.QueryString.Value, TwitifyController._myAuthRequestStore);
            // Request Twitter to generate the credentials.
            var userCreds = await appClient.Auth.RequestCredentialsAsync(requestParameters);
            Response.Cookies.Append("AccessToken", userCreds.AccessToken);
            Response.Cookies.Append("AccessTokenSecret", userCreds.AccessTokenSecret);
            // Congratulations the user is now authenticated!
            var userClient= new TwitterClient(userCreds);
            var user = await userClient.Users.GetAuthenticatedUserAsync();
            Response.Cookies.Append("Bio", userClient.Users.GetAuthenticatedUserAsync().Result.Description);
            Response.Cookies.Append("UserName", userClient.Users.GetAuthenticatedUserAsync().Result.ScreenName);
            Response.Cookies.Append("Profile",userClient.Users.GetAuthenticatedUserAsync().Result.ProfileImageUrl400x400);
            Uri baseUri = TwitifyController.SpotifyUri;

            var loginRequest = new LoginRequest(baseUri, TwitifyController.clientId, LoginRequest.ResponseType.Token)
            {
                Scope = new[] { Scopes.UserReadPlaybackState, Scopes.UserReadEmail, Scopes.UserReadCurrentlyPlaying }
            };
            return Redirect(loginRequest.ToUri().ToString());

        }
    }
}
