using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tweetinvi;
using Tweetinvi.Auth;
using Tweetinvi.Parameters;

namespace Twitify.Data
{
    public class TwitifyController : Controller
    {
        public static readonly IAuthenticationRequestStore _myAuthRequestStore = new LocalAuthenticationRequestStore();
        public static string ConsumerKey = "";// get from developer.twitter.com, needs developer account
        public static string ConsumerSecret = "";// get from developer.twitter.com, needs developer account
        public static string TwitterUri = "";// write your twitter callback url
        public static string clientId = "";// get from developer.spotify.com
        public static Uri SpotifyUri = new Uri("");// write your spotify callback url
    }

}
