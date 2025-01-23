using System.Net;

namespace Exam.Helpers.Extentions
{
    public static class FileExtention
    {


        public static string Upload(this IFormFile file, string rootpath, string folderName)
        {
            string filename = file.FileName;
            if (filename.Length > 64)
            {
                filename = filename.Substring(filename.Length - 64, 64);
            }
            filename = Guid.NewGuid() + filename; string path = Path.Combine(rootpath, folderName, filename);
            using (FileStream stream = new FileStream(path, FileMode.Create))
            { file.CopyTo(stream); }
            return filename;
        }
        public static bool DeleteFile(string rootpath, string folderName, string filename)
        {


            string path = Path.Combine(rootpath, folderName, filename);


            if (File.Exists(path))
            {
                return false;

            }
            File.Delete(path); return true;

        }
    }



}

