using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ForumDev.Data;
using ForumDev.Data.Models;
using ForumDev.ViewModels.ApplicationUser;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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
        private readonly IHostingEnvironment _hosting;

        #endregion

        #region Controller

        public ProfileController(UserManager<ApplicationUser> userManager, IApplicationUser userService, IUpload uploadService, IHostingEnvironment he)
        {
            this.userManager = userManager;
            this.userService = userService;
            this.uploadService = uploadService;
            this._hosting = he;
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

        [HttpPost]
        public async Task<IActionResult> UploadProfileImage(IFormFile file)
        {
            string filename = string.Empty;
            var userId = userManager.GetUserId(User);

            if (file != null)
            {
                filename = UploadImage(file);
                string url = "/images/" + Path.GetFileName(file.FileName);
                await userService.SetProfileImage(userId, url);
            }

            return RedirectToAction("Detail", "Profile", new { id = userId });
        }

        #endregion

        public string UploadImage(IFormFile img)
        {
            var filePath = Path.Combine(_hosting.WebRootPath + "\\images", Path.GetFileName(img.FileName));
            img.CopyTo(new FileStream(filePath, FileMode.Create));

            return filePath;
        }
    }
}