using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.IdentityModel.Claims;

namespace WebUi.Controllers
{
    [HandleError]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewData["Message"] = "Welcome to ASP.NET MVC!";

            var principal = System.Threading.Thread.CurrentPrincipal as Microsoft.IdentityModel.Claims.IClaimsPrincipal;
            if( principal != null && principal.Identities.Count > 0 )
            {
                var identity = principal.Identities[ 0 ];

                #region idQuery
                var idQuery = 
                    from c in identity.Claims 
                    where c.ClaimType == Microsoft.IdentityModel.Claims.ClaimTypes.NameIdentifier 
                    select c;
                #endregion

                #region displayNameQuery
                var displayNameQuery = 
                    from c in identity.Claims
                    where c.ClaimType == Microsoft.IdentityModel.Claims.ClaimTypes.Name
                    select c;
                #endregion

                var id = idQuery.FirstOrDefault().Value;
                var displayName = displayNameQuery.FirstOrDefault().Value;
                var facebookAccessToken = string.Empty;

                #region Get facebookAccessToken
                foreach( Claim claim in identity.Claims )
                {
                    string[] ParsedClaimType = claim.ClaimType.Split( '/' );
                    string ClaimKey = ParsedClaimType[ ParsedClaimType.Length - 1 ];

                    if( ClaimKey.Equals( "AccessToken" ) )
                    {
                        facebookAccessToken = claim.Value;
                        break;
                    }
                }
                #endregion

                var userIdentifier = new WebUi.AuthentificatorService.UserIdentification();
                    userIdentifier.Id = id;
                    userIdentifier.DisplayName = displayName;

                var authentificatorServiceClient = new WebUi.AuthentificatorService.AuthentificatorServiceClient();

                // POT cookie 
                Guid cookie = authentificatorServiceClient.SetUserAndGetCookie( userIdentifier );

                Session[ "FederationCookie" ] = cookie.ToString();

                // Facebook cookie
                Session[ "FacebookCookie" ] = facebookAccessToken;
            }


            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult WcfTestButton()
        {
            //AuthentificatorService.AuthentificatorServiceClient
            //    authentificatorServiceClient = new AuthentificatorService.AuthentificatorServiceClient();

            //string result = authentificatorServiceClient.Test();

            return View();
        }

        public ActionResult Authenticate()
        {
            //var userIdentifier = new AuthentificatorService.UserIdentification();
            //    userIdentifier.DisplayName = "";
            //    userIdentifier.Id = "";

            //var authentificatorServiceClient = new AuthentificatorService.AuthentificatorServiceClient();
            
            //Guid cookie = authentificatorServiceClient.SetUserAndGetCookie( userIdentifier );

            //AuthentificatorService.AuthentificatorServiceClient
            //    authentificatorServiceClient = new AuthentificatorService.AuthentificatorServiceClient();

            //string result = authentificatorServiceClient.Test();

            return View();
        }
    }
}
