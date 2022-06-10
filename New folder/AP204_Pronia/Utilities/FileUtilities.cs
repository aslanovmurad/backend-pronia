using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace AP204_Pronia.Utilities
{
    public static class FileUtilities
    {
        public static async Task<string> FileCreate(this IFormFile fromFile, string root, string folder)
        {
            string filestrim = Guid.NewGuid() + fromFile.FileName;
            string path = Path.Combine(root, folder);
            string fullpath = Path.Combine(filestrim, path);

            using (FileStream file = new FileStream(fullpath, FileMode.Create))
            {
                await fromFile.CopyToAsync(file);
            }
            return filestrim;
        }
        public static void FileDelete(string root, string path, string imageName)
        {
            string fullpath = Path.Combine(root, path, imageName);
            if (File.Exists(fullpath))
            {
                File.Delete(fullpath);
            }
        }
    }
}
