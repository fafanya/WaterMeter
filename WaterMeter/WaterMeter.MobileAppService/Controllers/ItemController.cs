using System;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WaterMeter.Common.Models;
using WaterMeter.Repositories;

namespace WaterMeter.Controllers
{
    [Route("api/[controller]")]
    public class ItemController : Controller
    {
        private readonly IItemRepository ItemRepository;

        public ItemController(IItemRepository itemRepository)
        {
            ItemRepository = itemRepository;
        }

        [HttpGet]
        public IActionResult List()
        {
            return Ok(ItemRepository.GetAll());
        }

        [HttpGet("{id}")]
        public TMeasurement GetItem(int id)
        {
            TMeasurement item = ItemRepository.Get(id);
            return item;
        }

        [HttpPost]
        public IActionResult Create([FromBody]TMeasurement item)
        {
            try
            {
                if (item == null || !ModelState.IsValid)
                {
                    return BadRequest("Invalid State");
                }

                ItemRepository.Add(item);

            }
            catch (Exception)
            {
                return BadRequest("Error while creating");
            }
            return Ok(item);
        }

        [HttpPut]
        public IActionResult Edit([FromBody] TMeasurement item)
        {
            try
            {
                if (item == null || !ModelState.IsValid)
                {
                    return BadRequest("Invalid State");
                }
                ItemRepository.Update(item);
            }
            catch (Exception)
            {
                return BadRequest("Error while creating");
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            ItemRepository.Remove(id);
        }

        [Route("CreateCounterMeasure/")]
        [HttpPost]
        public IActionResult Post()
        {
            var form = Request.Form;

            string path = string.Empty;
            var file = form.Files["file"];
            var uploadLocation = Path.Combine(Environment.CurrentDirectory, "Uploads\\UsersImg");
            var fileName = file.FileName.Split('\\').LastOrDefault().Split('/').LastOrDefault();
            if (file.Length > 0)
            {
                path = Path.Combine(uploadLocation, fileName);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
            }

            var item = JsonConvert.DeserializeObject<TMeasurement>(form["item"]);
            item.PhotoServerPath = path;
            ItemRepository.Add(item);

            return Ok();
        }
    }
}