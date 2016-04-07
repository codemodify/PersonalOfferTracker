using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading;
using Microsoft.IdentityModel.Claims;


namespace WebUi
{
    public partial class FederationCallbackHandler : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var principal = Thread.CurrentPrincipal as IClaimsPrincipal;
            if (principal != null && principal.Identities.Count > 0)
            {
                var identity = principal.Identities[0];
                var query = from c in identity.Claims where c.ClaimType == ClaimTypes.Email select c;
                var emailClaim = query.FirstOrDefault();
                if (emailClaim != null)
                {
                    Session["User"] = emailClaim.Value;
                }
            }

            string returnPage = (string)Session["ReturnPage"];

            Response.Redirect( returnPage );
        }
    }
}