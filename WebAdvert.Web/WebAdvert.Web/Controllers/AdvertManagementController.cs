using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebAdvert.Web.Services;
using WebAdvert.Web.Models.AdvertManagement;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Authorization;
using WebAdvert.Web.ServiceClients;
using AutoMapper;

namespace WebAdvert.Web.Controllers
{
    public class AdvertManagementController : Controller
    {
        private readonly IFileUploader _fileUploader;
        private readonly IAdvertAPIClient _advertAPIClient;
        private readonly IMapper _mapper;

        public AdvertManagementController(IFileUploader fileUploader, IAdvertAPIClient advertAPIClient, IMapper mapper)
        {
            _fileUploader = fileUploader;
            _advertAPIClient = advertAPIClient;
            _mapper = mapper;
        }

        public IActionResult Create()
        {
            return View(new CreateAdvertViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateAdvertViewModel model, IFormFile imageFile)
        {
            if(ModelState.IsValid)
            {
                var apiCallResponse = await _advertAPIClient.Create(_mapper.Map<CreateAdvertModel>(model));
                //var id = apiCallResponse.Id;
                //if (imageFile != null)
                //{
                //    var fileName = string.IsNullOrEmpty(imageFile.FileName)? Path.GetFileName(imageFile.FileName) : id;
                //    var filePath = $"{id}/{fileName}";
                //    try
                //    {
                //        using(var readerStream = imageFile.OpenReadStream())
                //        {
                //            var result = await _fileUploader.UploadFileAsync(filePath, readerStream);
                //            if (!result)
                //                throw new Exception();
                //        }
                //      var canConfirm =  await _advertAPIClient.Confirm(new ConfirmAdvertRequest() { Id = id, FilePath = filePath, Status = AdvertStatus.Active });
                //        if (!canConfirm)
                //            throw new Exception();
                //    }
                //    catch
                //    {
                //        await _advertAPIClient.Confirm(new ConfirmAdvertRequest() { Id = id, FilePath = filePath, Status = AdvertStatus.Pending });
                //    }
                //}
                
            }
            if (model != null)
            {
                var data = await _advertAPIClient.Get(_mapper.Map<string>(""));
                return View("Dashboard", data);
            }
            else
                return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var model = await _advertAPIClient.Get(_mapper.Map<string>(""));

            return View("Dashboard",model);
        }
    }
}