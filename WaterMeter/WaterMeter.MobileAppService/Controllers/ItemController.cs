using System;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using WaterMeter.Models;
using Microsoft.AspNetCore.Http;
using System.Linq;
using Microsoft.Extensions.PlatformAbstractions;

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
        public Item GetItem(string id)
        {
            Item item = ItemRepository.Get(id);
            return item;
        }

        [HttpPost]
        public IActionResult Create([FromBody]Item item)
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
        public IActionResult Edit([FromBody] Item item)
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
        public void Delete(string id)
        {
            ItemRepository.Remove(id);
        }

        [Route("Files/Upload/")]
        [HttpPost]
        public IActionResult Post(IFormFile file)
        {
            var uploadLocation = Path.Combine(Environment.CurrentDirectory, "Uploads\\UsersImg");

            var fileName = file.FileName.Split('\\').LastOrDefault().Split('/').LastOrDefault();

            if (file.Length > 0)
            {
                using (var stream = new FileStream(Path.Combine(uploadLocation, fileName), FileMode.Create))
                {
                    file.CopyTo(stream);
                }
            }
            return Ok();
        }
    }
}