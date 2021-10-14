using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MVC_Produkt_Manager.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using ZXing;
using ZXing.QrCode;

using ImageMagick;

namespace MVC_Produkt_Manager.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IWebHostEnvironment _hostEnvironment;

        public HomeController(ILogger<HomeController> logger, IWebHostEnvironment hostEnvironment)
        {
            _logger = logger;

            _hostEnvironment = hostEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]


        public IActionResult Index(IFormCollection formCollection)
        {
            var writer = new QRCodeWriter();
            var resultBit = writer.encode(formCollection["QRCodeString"], BarcodeFormat.QR_CODE, 200, 200);
            var matrix = resultBit;
            int scale = 2;
            Bitmap result = new Bitmap(matrix.Width * scale, matrix.Height * scale);
            for (int x = 0; x < matrix.Height; x++)
            {
                for (int y = 0; y < matrix.Width; y++)
                {
                    Color pixel = matrix[x, y] ? Color.Black : Color.White;
                    for (int i = 0; i < scale; i++)
                        for (int j = 0; j < scale; j++)
                            result.SetPixel(x*scale+i,y * scale +j,pixel);
                        }
                    }
             
                    string webRoothPath = _hostEnvironment.WebRootPath;
                    result.Save(webRoothPath + "\\Images\\QrcodeNew.png");
                      ViewBag.URL = "\\Images\\QrcodeNew.png";
                    return View();
                }



        public IActionResult ReadQRCode()
        {
            string webRootPath = _hostEnvironment.WebRootPath;
            var path = webRootPath + "\\Images\\barcoderReader.jpg";
            var reader = new BarcodeReaderGeneric();
            Bitmap image = (Bitmap)Image.FromFile(path);
            using (image)
            {
                LuminanceSource source;
                source = new ZXing.Windows.Compatibility.BitmapLuminanceSource(image);
                Result result = reader.Decode(source);
                ViewBag.Text = result.Text;
            }
            return View("Index");
        }



            
        

        public IActionResult Privacy()
        {

            var info = new MagickImageInfo(@"C:\Users\DCV\source\repos\MVC_Produkt_Manager\MVC_Produkt_Manager\wwwroot\img\66107bbc-95fe-4e5d-944a-9095efaf8329.jpeg");

            ViewData["testWidth"] = info.Width;

            ViewBag.testWidth = info.Width;
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        public IActionResult magick()
        {

            var datei = @"C:\Users\DCV\source\repos\MVC_Produkt_Manager\MVC_Produkt_Manager\wwwroot\img\66107bbc-95fe-4e5d-944a-9095efaf8329.jpeg";

            // Read from file
            var info = new MagickImageInfo(@"C:\Users\DCV\source\repos\MVC_Produkt_Manager\MVC_Produkt_Manager\wwwroot\img\66107bbc-95fe-4e5d-944a-9095efaf8329.jpeg");

            var image2 = new MagickImage(@"C:\Users\DCV\source\repos\MVC_Produkt_Manager\MVC_Produkt_Manager\wwwroot\img\barcoderReader.jpg");

            //image.TransparentChroma(MagickColors.Black, MagickColors.Blue);

            using (var image = new MagickImage(datei))
            {
                
                image.Alpha(AlphaOption.Set);
                image.ColorFuzz = new Percentage(20);
                image.Settings.BackgroundColor = MagickColors.Transparent;
                image.Settings.FillColor = MagickColors.None;
                image.FloodFill(MagickColors.Aqua, 200, 220);
                image.Crop(new MagickGeometry(500, 220, 840, 800));
                image.Resize(new MagickGeometry(1000));
                image.Composite(image2,200,200);
                  
               //MagickFormat format =  image.Format = MagickFormat.Png;
                image.Write(@"C:\Users\DCV\source\repos\MVC_Produkt_Manager\MVC_Produkt_Manager\wwwroot\img\Snakeware.100x100." + MagickFormat.Png);
            }

            //image.BackgroundColor = new ColorMono(true).ToMagickColor();

            //Q16(Blue):
            //image.TransparentChroma(new MagickColor(0, 0, 0), new MagickColor(0, 0, Quantum.Max));
            //image.TransparentChroma(new ColorRGB(0, 0, 0).ToMagickColor(), new ColorRGB(0, 0, Quantum.Max).ToMagickColor());
            //image.BackgroundColor = new MagickColor("#00f");
            //image.BackgroundColor = new MagickColor("#0000ff");
            //image.BackgroundColor = new MagickColor("#00000000ffff");

            //With transparency(Red):
            //image.BackgroundColor = new MagickColor(0, 0, Quantum.Max, 0);
            //image.BackgroundColor = new MagickColor("#0000ff80");

            //Q8(Green):
          //   image.TransparentChroma(new MagickColor(0, 0, 0), new MagickColor(5, 5, ));
            //image.TransparentChroma(new ColorRGB(0, 0, 0).ToMagickColor(), new ColorRGB(0, Quantum.Max, 0).ToMagickColor());
            //image.BackgroundColor = new MagickColor("#0f0");
           // image.BackgroundColor = new MagickColor("#00ff00");

            //image.Write(@"C:\Users\DCV\source\repos\MVC_Produkt_Manager\MVC_Produkt_Manager\wwwroot\img\Snakeware.100x100.png");

            


                //System.Drawing.Image image = System.Drawing.Image.FromFile(@"C:\Users\DCV\source\repos\MVC_Produkt_Manager\MVC_Produkt_Manager\wwwroot\img\8a877757-f8c1-4260-9704-1df55ef98976.jpeg");
                //var dpiX = image.HorizontalResolution;
                //var dpiY = image.VerticalResolution;

            Console.WriteLine(info.Width);
            Console.WriteLine(info.Height);
            Console.WriteLine(info.ColorSpace);
         
            Console.WriteLine(info.Format);


            return View("Privacy");
        }

    }
}
