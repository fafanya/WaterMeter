using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO;
using WaterMeter.Repositories;
using Microsoft.AspNetCore.WebUtilities;
using System.Collections;
using System.Web;
using System.Net.Http;
using System.Diagnostics;
using System.Net;
using Microsoft.Extensions.PlatformAbstractions;
using Newtonsoft.Json;
using WaterMeter.MobileAppService.Models;

namespace WaterMeter.Controllers
{
    [Route("api/[controller]")]
    public class MeasurementsController : ControllerBase
    {
        private readonly WaterMeterServiceContext _context;

        public MeasurementsController(WaterMeterServiceContext context)
        {
            _context = context;
        }

        // GET: api/Measurements
        [HttpGet]
        public IEnumerable<TMeasurement> GetMeasurement()
        {
            return _context.Measurement;
        }

        // GET: api/Measurements/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMeasurement([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var measurement = await _context.Measurement.FindAsync(id);

            if (measurement == null)
            {
                return NotFound();
            }

            return Ok(measurement);
        }

        // PUT: api/Measurements/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMeasurement([FromRoute] int id, [FromBody] TMeasurement measurement)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != measurement.TMeasurementId)
            {
                return BadRequest();
            }

            _context.Entry(measurement).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MeasurementExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Measurements
        [HttpPost]
        public async Task<IActionResult> PostMeasurement([FromBody] TMeasurement measurement)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Measurement.Add(measurement);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMeasurement", new { id = measurement.TMeasurementId }, measurement);
        }

        // DELETE: api/Measurements/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMeasurement([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var measurement = await _context.Measurement.FindAsync(id);
            if (measurement == null)
            {
                return NotFound();
            }

            _context.Measurement.Remove(measurement);
            await _context.SaveChangesAsync();

            return Ok(measurement);
        }

        private bool MeasurementExists(int id)
        {
            return _context.Measurement.Any(e => e.TMeasurementId == id);
        }

        [Route("CreateCounterMeasure/")]
        [HttpPost]
        public async Task<IActionResult> PostAsync()
        {
            var form = Request.Form;

            string path = String.Empty;
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
            //ItemRepository.Add(item);

            return await PostMeasurement(item);

            //return Ok();
        }
    }
}