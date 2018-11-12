using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ForumDev.Data;
using ForumDev.Data.Models;
using ForumDev.ViewModels.ApplicationUser;
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

        public async Task<IActionResult> Detail(string id)
        {
            var user = userService.GetById(id);
            var userRoles = await userManager.GetRolesAsync(user);

            var viewMod = new ProfileViewModel
            {
                UserId = user.Id,
                Username = user.UserName,
                UserRating = user.Rating.ToString(),
                Email = user.Email,
                ProfileImageUrl = user.ProfileImageUrl,
                MemberSince = user.MemberSince,
                IsAdmin = userRoles.Contains("Admin")
            };

            return View(viewMod);
        }

        #endregion
    }
}