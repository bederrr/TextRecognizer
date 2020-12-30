using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;
using Aspose.OCR;

namespace TextRecognizer
{
    public class ReaderService
    {
        private readonly string _path;
        private readonly Channel<string> _queue;
        private readonly AsposeOcr _ocr;
        
        public ReaderService(string path)
        {
            EnsurePath(path);
            
            _queue = Channel.CreateUnbounded<string>();
            _path = path;
            _ocr = new AsposeOcr();
        }

        public async Task StartRecognizing()
        {
            await AddFilesToQueue();

            var a = Enumerable.Range(0, 3)
                .Select(_ =>Task.Run(RecognizeNext))
                .ToList();

            await Task.WhenAll(a);
        }

        private async Task AddFilesToQueue()
        {
            foreach (var file in Directory.GetFiles(_path))
            {
                Console.WriteLine($"add, ThreadId: {Thread.CurrentThread.ManagedThreadId}");
                await _queue.Writer.WriteAsync(file);
            }
        }

        private async Task RecognizeNext()
        {
            var file = await _queue.Reader.ReadAsync();

            var img = Image.FromFile(file);
            if (img == null) return;

            var temp = Path.GetTempFileName();
            CropHalfImage(img).Save(temp);

            var result = _ocr.RecognizeImage(temp);
            Console.WriteLine(result[0] + " Id: " + Thread.CurrentThread.ManagedThreadId);
        }

        private static Image CropHalfImage(Image img)
        {
            var bmpImage = new Bitmap(img);
            return bmpImage.Clone(new Rectangle(0, 0, img.Width, img.Height / 2), bmpImage.PixelFormat);
        }

        private static void EnsurePath(string path)
        {
            if (!Directory.Exists(path)) throw new Exception($"Folder: '{path}' doesn't exist.");
        }
    }
}