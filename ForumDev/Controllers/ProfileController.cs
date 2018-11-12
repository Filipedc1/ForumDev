using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ForumDev.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ForumDev.Controllers
{
    public class ProfileController : Controller
    {
        #region Fields

        private readonly UserManager<ApplicationUser> userManager;
        private readonly IApplicationUser userService;
        private readonly IUpload uploadService;

        #endregion

        #region Controller

        public ProfileController(UserManager<ApplicationUser> userManager, IApplicationUser userService, IUpload uploadService)
        {
            this.userManager = userManager;
            this.userService = userService;
            this.uploadService = uploadService;
        }

        #endregion

        #region Actions

        public IActionResult Detail(string id)
        {



            return View();
        }

        #endregion
    }
}