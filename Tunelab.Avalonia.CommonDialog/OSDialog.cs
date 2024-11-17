using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Platform.Storage;
using Svg;

namespace Tunelab.CommonDialog
{
    public class OSDialog
    {
        public static async Task<string?> OpenFolderAsync()
        {
            var openFileDialog = new OpenFolderDialog();
            var result = await openFileDialog.ShowAsync(null);
            return result;
        }
        public static async Task<string[]?> OpenFileAsync(Dictionary<string, string[]> Filters)
        {
            List<FileDialogFilter> fdf = new List<FileDialogFilter>();
            foreach (var kv in Filters) fdf.Add(new FileDialogFilter() { Name = kv.Key, Extensions = kv.Value.ToList() });
            var openFileDialog = new OpenFileDialog
            {
                Filters = fdf
            };
            var result = await openFileDialog.ShowAsync(null);
            return result;
        }
        public static async Task<string?> SaveFileAsync(Dictionary<string, string[]> Filters)
        {
            List<FileDialogFilter> fdf = new List<FileDialogFilter>();
            foreach (var kv in Filters) fdf.Add(new FileDialogFilter() { Name = kv.Key, Extensions = kv.Value.ToList() });
            var openFileDialog = new SaveFileDialog
            {
                Filters = fdf
            };
            var result = await openFileDialog.ShowAsync(null);
            return result;
        }

        public static void NavigatePath(string Path)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                // Windows
                Process.Start(new ProcessStartInfo
                {
                    FileName = "explorer.exe",
                    Arguments = Path,
                    UseShellExecute = true
                });
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                // Linux
                Process.Start(new ProcessStartInfo
                {
                    FileName = "xdg-open",
                    Arguments = Path,
                    UseShellExecute = true
                });
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                // macOS
                Process.Start(new ProcessStartInfo
                {
                    FileName = "open",
                    Arguments = Path,
                    UseShellExecute = true
                });
            }
            else
            {
                Console.WriteLine("不支持的操作系统。");
            }
        }
    }
}
