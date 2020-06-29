using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace projectRootNamespace.Api.Infrastructure
{
    [ApiController]
    [Authorize]
    public abstract class BaseController : ControllerBase
    {
        // TODO: Place logic common to all or most controllers here.
        //       Example would be a logger or a proeprty that returns
        //       the authenticated user.
    }
}
