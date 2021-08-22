using AutoMapper;
using Microsoft.AspNetCore.Hosting;
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
using System.Threading.Tasks;

namespace ProgrammersBlog.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly IWebHostEnvironment _env;
        private readonly IMapper _mapper;

        public UserController(UserManager<User> userManager, IWebHostEnvironment env, IMapper mapper)
        {
            _userManager = userManager;
            _env = env;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.ToListAsync();
            return View(new UserListDto{
                Users = users, 
                ResultStatus = ResultStatus.Success
            });
        }

        [HttpGet]
        public IActionResult Add()
        {
            return PartialView("_UserAddPartial");
        }

        [HttpPost]
        public async Task<IActionResult> Add(UserAddDto userAddDto)
        {
            if (ModelState.IsValid) // Is validation completed successfully;
            {
                userAddDto.Picture = await ImageUpload(userAddDto); // Update image and assign name to userAddDto
                var user = _mapper.Map<User>(userAddDto);   // Mapping userAddDto => User using AutoMapper
                var result = await _userManager.CreateAsync(user, userAddDto.Password); // Create user asynchronously.
                if (result.Succeeded)
                {
                    var userAddAjaxModel = JsonSerializer.Serialize(new UserAddAjaxViewModel
                    {
                        UserDto = new UserDto {
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
                    var userAddAjaxErrorModel = JsonSerializer.Serialize(new UserAddAjaxViewModel { 
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


        public async Task<string> ImageUpload(UserAddDto userAddDto)
        {
            // Accessing img : [~][AltGr+Ü] => "~/img/userFileName.png"
            string wwwroot  = _env.WebRootPath;
            string fileExtension = Path.GetExtension(userAddDto.PictureFile.FileName); // Get file extension.
            string fileName = $"{userAddDto.UserName}_{DateTimeExtensions.GetDateTimeWithUnderScore()}"; // Create new image fileName with dates.
            string fileNameWithExtension = fileName + fileExtension; // Created file name with file extension.
            var path = Path.Combine($"{wwwroot}/img",fileNameWithExtension); // Create path.
            await using(var stream = new FileStream(path, FileMode.Create))  // Write image to path via stream. 
            {
                await userAddDto.PictureFile.CopyToAsync(stream);
            }
            return fileNameWithExtension;
        }
    }
}
