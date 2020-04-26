using System.Linq;
using System.Threading.Tasks;
using FP.AzureStorage.Service;
using FP.AzureStorage.Service.Models;
using FP.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.SignalR;

namespace FP.WebApp.Controllers
{
    [Route("api")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        private readonly IImageService _imageService;
        private readonly IHubContext<ImageHub> _hubContext;

        public UploadController(IImageService imageService, IHubContext<ImageHub> hubContext)
        {
            this._imageService = imageService;
            _hubContext = hubContext;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> Upload()
        {
            var file = Request.Form.Files.FirstOrDefault();
            var title = Request.Form.ContainsKey("Title") ? Request.Form["Title"].ToString() : string.Empty;

            if (file == null || string.IsNullOrEmpty(title))
            {
                return new ObjectResult(new FPResult { Success = false });
            }

            if (!file.ContentType.Contains("image")) {
                return new ObjectResult(new FPResult { Success = false, Message="Only images are accepted" });
            }

            //save to azure blobstorage
            var bloblUrl = await this._imageService.Upload(new FileModel { Title = title, Name = file.FileName, Stream = file.OpenReadStream() });

            //send to signalr
            await _hubContext.Clients.All.SendAsync("ReceiveMessage", bloblUrl, title);


            return new ObjectResult(new FPResult { Success = true });

        }


    }
}