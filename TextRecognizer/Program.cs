using System.Threading.Tasks;

namespace TextRecognizer
{
    internal static class Program
    {
        private static async Task Main(string[] args)
        {
            // var img = Image.FromFile(@"C:\Users\Konstantin\Desktop\8.jpg");
            //
            // if (img == null) return;
            //
            // var cropped = CropImage(img, new Rectangle(0, 0, img.Width, img.Height / 2));
            //
            // var temp = Path.GetTempFileName();
            // cropped.Save(temp);
            //
            // AsposeOcr api = new AsposeOcr();
            //
            // var result = api.RecognizeImage(temp);
            // Console.WriteLine(result[0]);

            var svc = new ReaderService(@"C:\Users\Konstantin\Desktop\cards");
            await svc.StartRecognizing();
        }
    }
}