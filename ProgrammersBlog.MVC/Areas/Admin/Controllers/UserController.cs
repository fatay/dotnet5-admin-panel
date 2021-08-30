using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProgrammersBlog.Entities.Concrete;
using ProgrammersBlog.Entities.Dtos;
using ProgrammersBlog.MVC.Areas.Admin.Models;
using ProgrammersBlog.Shared.Utilities.Extensions;
using ProgrammersBlog.Shared.Utilities.Results.Types;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ProgrammersBlog.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {
        // Dependencies
        private readonly UserManager<User> _userManager;
        private readonly IWebHostEnvironment _env;
        private readonly IMapper _mapper;
        private readonly SignInManager<User> _signInManager;

        public UserController(UserManager<User> userManager, IWebHostEnvironment env, IMapper mapper, SignInManager<User> signInManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _env = env;
            _mapper = mapper;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.ToListAsync();
            return View(new UserListDto
            {
                Users = users,
                ResultStatus = ResultStatus.Success
            });
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Login(UserLoginDto userLoginDto)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(userLoginDto.Email);
                if (user != null)
                {
                    // If user exist in dB.
                    var result = await _signInManager.PasswordSignInAsync(user, userLoginDto.Password, userLoginDto.RememberMe, false);
                    if (result.Succeeded)
                    {
                        RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("", "E-posta adresi veya şifre yanlış!");
                        return View("UserLogin");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "E-posta adresi veya şifre yanlış!");
                    return View("UserLogin");
                }
            }
            return View("UserLogin");
        }

        [Authorize]
        [HttpGet]
        public async Task<JsonResult> GetAllUsers()
        {
            var users = await _userManager.Users.ToListAsync();
            var userListDto = JsonSerializer.Serialize(new UserListDto
            {
                Users = users,
                ResultStatus = ResultStatus.Success
            }, new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve
            });
            return Json(userListDto);
        }

        [Authorize]
        [HttpGet]
        public IActionResult Add()
        {
            return PartialView("_UserAddPartial");
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Add(UserAddDto userAddDto)
        {
            if (ModelState.IsValid) // Is validation completed successfully;
            {
                userAddDto.Picture = await ImageUpload(userAddDto.UserName, userAddDto.PictureFile); // Update image and assign name to userAddDto
                var user = _mapper.Map<User>(userAddDto);   // Mapping userAddDto => User using AutoMapper
                var result = await _userManager.CreateAsync(user, userAddDto.Password); // Create user asynchronously.
                if (result.Succeeded)
                {
                    var userAddAjaxModel = JsonSerializer.Serialize(new UserAddAjaxViewModel
                    {
                        UserDto = new UserDto
                        {
                            ResultStatus = ResultStatus.Success,
                            Message = $"{user.UserName} adlı kullanıcı başarıyla eklendi.",
                            User = user
                        },
                        UserAddPartial = await this.RenderViewToStringAsync("_UserAddPartial", userAddDto)
                    });
                    return Json(userAddAjaxModel);
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    var userAddAjaxErrorModel = JsonSerializer.Serialize(new UserAddAjaxViewModel
                    {
                        UserAddDto = userAddDto,
                        UserAddPartial = await this.RenderViewToStringAsync("_UserAddPartial", userAddDto)
                    });
                    return Json(userAddAjaxErrorModel);
                }
            }
            var userAddAjaxStateErrorModel = JsonSerializer.Serialize(new UserAddAjaxViewModel
            {
                UserAddDto = userAddDto,
                UserAddPartial = await this.RenderViewToStringAsync("_UserAddPartial", userAddDto)
            });
            return Json(userAddAjaxStateErrorModel);
        }

        [Authorize]
        [HttpGet]
        public async Task<PartialViewResult> Update(int userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            var userUpdateDto = _mapper.Map<UserUpdateDto>(user);
            return PartialView("_UserUpdatePartial", userUpdateDto);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Update(UserUpdateDto userUpdateDto)
        {
            if (ModelState.IsValid)
            {
                bool isNewPictureUploaded = false;
                var oldUser = await _userManager.FindByIdAsync(userUpdateDto.Id.ToString()); // Get user that it will be update
                var oldUserPicture = oldUser.Picture; // Get old user picture from server disk.
                if (userUpdateDto.PictureFile != null) // If new file uploaded, delete old one. Else, use same file.
                {
                    userUpdateDto.Picture = await ImageUpload(userUpdateDto.UserName, userUpdateDto.PictureFile);
                    isNewPictureUploaded = true;
                }

                if (isNewPictureUploaded)
                {
                    ImageDelete(oldUserPicture);
                }
                else
                {
                    userUpdateDto.Picture = oldUserPicture;
                }

                // Mapping UserUpdateDto to User ----- using userUpdateDto and oldUser objects
                var updatedUser = _mapper.Map<UserUpdateDto, User>(userUpdateDto, oldUser);
                var result = await _userManager.UpdateAsync(updatedUser);
                if (result.Succeeded)
                {
                    var userUpdateViewModel = JsonSerializer.Serialize(new UserUpdateAjaxViewModel
                    {
                        UserDto = new UserDto
                        {
                            ResultStatus = ResultStatus.Success,
                            Message = $"{updatedUser.UserName} adlı kullanıcı başarıyla güncellendi.",
                            User = updatedUser
                        },
                        UserUpdatePartial = await this.RenderViewToStringAsync("_UserUpdatePartial", userUpdateDto)
                    });
                    return Json(userUpdateViewModel);
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    var userUpdateErrorViewModel = JsonSerializer.Serialize(new UserUpdateAjaxViewModel
                    {
                        UserUpdateDto = userUpdateDto,
                        UserUpdatePartial = await this.RenderViewToStringAsync("_UserUpdatePartial", userUpdateDto)
                    });
                    return Json(userUpdateErrorViewModel);
                }
            }
            else
            {
                var userUpdateStateErrorViewModel = JsonSerializer.Serialize(new UserUpdateAjaxViewModel
                {
                    UserUpdateDto = userUpdateDto,
                    UserUpdatePartial = await this.RenderViewToStringAsync("_UserUpdatePartial", userUpdateDto)
                });
                return Json(userUpdateStateErrorViewModel);
            }
        }

        [Authorize]
        public async Task<JsonResult> Delete(int userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                var deletedUser = JsonSerializer.Serialize(new UserDto
                {
                    ResultStatus = ResultStatus.Success,
                    Message = $"{user.UserName} adlı kullanıcı başarıyla silindi.",
                    User = user
                });
                ImageDelete(user.Picture);
                return Json(deletedUser);
            }
            else
            {
                string errMsg = String.Empty;
                foreach (var error in result.Errors)
                {
                    errMsg = $"{error.Description}\n";
                }
                var deletedUserErrorModel = JsonSerializer.Serialize(new UserDto
                {
                    ResultStatus = ResultStatus.Error,
                    Message = $"{user.UserName} adlı kullanıcı silinirken hata oluştu.\n {errMsg}"
                });
                return Json(deletedUserErrorModel);
            }
        }

        [Authorize]
        public async Task<string> ImageUpload(string userName, IFormFile pictureFile)
        {
            // Accessing img : [~][AltGr+Ü] => "~/img/userFileName.png"
            string wwwroot = _env.WebRootPath; // Get path.
            string fileExtension = Path.GetExtension(pictureFile.FileName); // Get file extension.
            string fileName = $"{userName}_{DateTimeExtensions.GetDateTimeWithUnderScore()}"; // Create new image fileName with dates.
            string fileNameWithExtension = fileName + fileExtension; // Created file name with file extension.
            var path = Path.Combine($"{wwwroot}/img", fileNameWithExtension); // Create path.
            await using (var stream = new FileStream(path, FileMode.Create))  // Write image to path via stream. 
            {
                await pictureFile.CopyToAsync(stream); // sTREAM
            }
            return fileNameWithExtension;
        }

        [Authorize]
        public bool ImageDelete(string pictureName)
        {
            string wwwroot = _env.WebRootPath; // Get current wwwroot path.
            var fileToDelete = Path.Combine($"{wwwroot}/img", pictureName);
            if (System.IO.File.Exists(fileToDelete))
            {
                System.IO.File.Delete(fileToDelete);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
