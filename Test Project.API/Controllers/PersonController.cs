using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Test_Project.BLL.Managers.Interfaces;
using Test_Project.BLL.Models;
using Test_Project.Shared.Models;
using Test_Project.Shared.Resources;

namespace Test_Project.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IPersonManager personManager;
        private readonly IRelatedPersonManager relatedPersonManager;
        private readonly IWebHostEnvironment webHostEnvironment;

        public PersonController(IPersonManager personManager, IWebHostEnvironment webHostEnvironment, IRelatedPersonManager relatedPersonManager)
        {
            this.personManager = personManager;
            this.relatedPersonManager = relatedPersonManager;
            this.webHostEnvironment = webHostEnvironment;
        }

        [Route("index")]
        [HttpGet]
        public IActionResult Index()
        {
            return Ok(GlobalResource.ApiStatus);
        }

        [Route("add")]
        [HttpPost]
        public async Task<IActionResult> AddPerson(PersonModel model)
        {
            return Ok(await personManager.AddPersonAsync(model));
        }

        [Route("update")]
        [HttpPost]
        public async Task<IActionResult> UpdatePerson(PersonModel model)
        {
            return Ok(await personManager.UpdatePerson(model));
        }

        [Route("remove/{Id}")]
        [HttpPost]
        public async Task<IActionResult> RemovePerson(int Id)
        {
            return Ok(await personManager.RemovePersonAsync(Id));
        }

        [Route("uploadImg/{personId}")]
        [HttpPost]
        public async Task<IActionResult> UploadImage(int personId, IFormFile file)
        {

            var fileName = Guid.NewGuid().ToString() + ".jpg";
            using (var stream = new FileStream(webHostEnvironment.ContentRootPath + "\\Images\\" + fileName, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            await personManager.UploadImageAsync(personId, fileName);

            return Ok(new OperationOutcome(true, GlobalResource.Success));
        }

        [Route("addRelatedPerson")]
        [HttpPost]
        public async Task<IActionResult> AddRelatedPerson(RelatedPersonModel model)
        {
            return Ok(await relatedPersonManager.AddRelatedPersonAsync(model));
        }

        [Route("removeRelatedPerson/{Id}")]
        [HttpPost]
        public async Task<IActionResult> RemoveRelatedPerson(int Id)
        {
            return Ok(await relatedPersonManager.RemoveRelatedPersonAsync(Id));
        }

        [Route("getFullPersonInfo/{Id}")]
        [HttpGet]
        public async Task<IActionResult> GetPersonById(int Id)
        {
            return Ok(await personManager.GetPersonAsync(Id));
        }

        [Route("relatedPersonCount")]
        [HttpGet]
        public async Task<IActionResult> GetRelatedPersonCount()
        {
            return Ok(await personManager.GetPersonWithRelatedPersonCount());
        }

        [Route("getPersonByModel")]
        [HttpGet]
        public async Task<IActionResult> GetPersonByModel(PersonFilterModel filter)
        {
            return Ok(await personManager.GetPersonByModel(filter));
        }
    }
}