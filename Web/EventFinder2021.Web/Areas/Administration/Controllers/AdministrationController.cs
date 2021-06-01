namespace EventFinder2021.Web.Areas.Administration.Controllers
{
    using EventFinder2021.Common;
    using EventFinder2021.Web.Controllers;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    [Area("Administration")]
    public class AdministrationController : BaseController
    {
    }
}
