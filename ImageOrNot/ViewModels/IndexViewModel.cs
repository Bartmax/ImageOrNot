using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

namespace ImageOrNot.ViewModels
{
    public class IndexViewModel
    {
        private const string outputfolder = "result";
        public IndexViewModel()
        {
            var files = Directory.GetFiles("wwwroot\\Resources", "*.jpg");
            Files = new Stack<string>(files);
            Directory.CreateDirectory(outputfolder);
        }

        public Stack<string> Files { get; private set; } = new Stack<string>();

        public string Current => Files.FirstOrDefault().Replace("wwwroot", "");

        public Stack<string> History { get; private set; } = new Stack<string>();



        public Task Save(string folder)
        {
            var file = Files.Pop();
            var source = $"{file}";

            Directory.CreateDirectory(Path.Combine(outputfolder, folder));
            var target = Path.Combine(outputfolder, folder, Path.GetFileName(source));
            History.Push(target);
            try
            {
                File.Move(source, target, true);
            }
            catch
            {
                return Task.FromCanceled(new CancellationToken());
            }
            return Task.CompletedTask;
        }

        public void Undo()
        {
            var source = History.Pop();
            var target = Path.Combine("wwwroot", "Resources", Path.GetFileName(source));
            File.Move(source, target, true);
            Files.Push(target);
        }
    }
}
