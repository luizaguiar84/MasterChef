using MasterChef.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;


namespace MasterChef.Api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ImagemController : Controller
    {
        private readonly IConfiguration configuration;
        public ImagemController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        [ProducesResponseType(typeof(byte[]), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(byte[]), StatusCodes.Status404NotFound)]
        [HttpGet]
        [Route("{imagem}")]
        public async Task<IActionResult> Get(string imagem)
        {
            try
            {
                var path = configuration["filePah"].ToString();
                var filapath = $"{path}{imagem}";
                var imageBytes = await System.IO.File.ReadAllBytesAsync(filapath);

                if (imageBytes.Length > 0)
                {
                    return Ok(imageBytes);
                }
                else
                {
                    return NotFound(imagem);
                }
            }
            catch (Exception)
            {

                throw;
            }

        }

        [ProducesResponseType(typeof(byte[]), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(byte[]), StatusCodes.Status500InternalServerError)]
        [HttpPost]
        public async Task<IActionResult> Post(Imagem imagem)
        {
            try
            {
                var path = configuration["filePah"].ToString();
                var pathFull = $"{path}/{imagem.nameImage}";
                MemoryStream ms = new MemoryStream(imagem.imagem);
                var image = Image.FromStream(ms);
                image.Save(pathFull);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
