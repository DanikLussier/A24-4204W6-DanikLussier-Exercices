using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
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
        public async Task<ActionResult<List<int>>> GetPictureIds()
        {
            // À modifier
            return Ok(_context.Picture.Select(p => p.Id));
        }

        [HttpGet("{size}/{id}")]
        public async Task<ActionResult> GetPicture(string size, int id)
        {
            // À modifier
            if (_context.Picture == null)
            {
                return NotFound();
            }

            Picture? picture = await _context.Picture.FindAsync(id);
            if (picture == null || picture.FileName == null || picture.MimeType == null)
            {
                return NotFound(new { Message = "Cette objet photo n'existe pas ou n'a pas de photo associée" });
            }

            if (!(Regex.Match(size, "large|small").Success))
            {
                return BadRequest(new { Message = "La taille demandée est inadéquate" });
            }

            byte[] bytes = System.IO.File.ReadAllBytes(Directory.GetCurrentDirectory() + "/images/" + size + "/" + picture.FileName);

            return File(bytes, picture.MimeType);
        }

        [HttpPost]
        [DisableRequestSizeLimit]
        public async Task<ActionResult<Picture>> PostPicture()
        {
            Picture picture = new Picture();
            // À modifier
            try
            {
                IFormCollection formCollection = await Request.ReadFormAsync();

                int i = 0;
                IFormFile? file = formCollection.Files.GetFile("image" + i);
                while (file != null)
                {
                    file = formCollection.Files.GetFile("image" + i);
                    if (file == null) break;

                    Image image = Image.Load(file.OpenReadStream());

                    picture.FileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    picture.MimeType = file.ContentType;

                    image.Save(Directory.GetCurrentDirectory() + "/images/large/" + picture.FileName);
                    image.Mutate(i =>
                        i.Resize(new ResizeOptions()
                        {
                            Mode = ResizeMode.Min,
                            Size = new Size() { Height = 200 }
                        })
                    );
                    image.Save(Directory.GetCurrentDirectory() + "/images/small/" + picture.FileName);

                    _context.Picture.Add(picture);
                    await _context.SaveChangesAsync();

                    i++;
                }
                return Ok(picture);

                file = formCollection.Files.GetFile("image" + i);
                if (file != null)
                {
                    
                }
                else
                {
                    return NotFound(new { Message = "Aucune image fournie" });
                }
            }
            catch(Exception)
            {
                throw;
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePicture(int id)
        {
            // À modifier
            if (_context.Picture == null)
            {
                return NotFound();
            }

            var picture = await _context.Picture.FindAsync(id);

            if (picture == null)
            {
                return NotFound(new { Message = "Cette photo n'existe pas" });
            }

            if (picture.MimeType != null && picture.FileName != null)
            {
                System.IO.File.Delete(Directory.GetCurrentDirectory() + "/images/small" + picture.FileName);
                System.IO.File.Delete(Directory.GetCurrentDirectory() + "/images/large" + picture.FileName);
            }

            _context.Picture.Remove(picture);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
