using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using semaine11_serveur.Data;
using semaine11_serveur.Models;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace semaine11_serveur.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PicturesController : ControllerBase
    {
        private readonly semaine11_serveurContext _context;

        public PicturesController(semaine11_serveurContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult> GetPictureIds()
        {
            // À modifier
            return Ok();
        }

        [HttpGet("{size}/{id}")]
        public async Task<ActionResult> GetPicture(string size, int id)
        {
            // À modifier
            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult> PostPicture()
        {
            // À modifier
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePicture(int id)
        {
            // À modifier
            return Ok();
        }
    }
}
