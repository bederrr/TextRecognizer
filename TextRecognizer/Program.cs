using System;
using System.Linq;
using System.Threading;
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

            // Enumerable.Range(0, 10).Select(x => Task.Run(() => Write(x))).ToList();
            //
            // await Write(99);
        }

        private static async Task Write(int i)
        {
            while (true)
            {
                Console.WriteLine($"A: {Thread.CurrentThread.ManagedThreadId} {i}");
                await Task.Delay(500);
            }
        }
    }
}