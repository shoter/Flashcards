using Flashcards.Entities;
using Flashcards.Entities.Enums.Files;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Hosting;

namespace Services.Code.Uploads
{
    public class UploadLocation
    {
        private const string UploadFolder = "~/Content/Upload/";

        static public readonly UploadLocation FlashcardImageFolder = new UploadLocation(UploadFolder + "FlashcardImage/");
        static public readonly UploadLocation TempFolder = new UploadLocation(UploadFolder + "Temp/");

        public readonly string Path;

        public string AbsolutePath => HostingEnvironment.MapPath(Path);

        public UploadLocation(string path)
        {
            Path = path;
        }

        public static UploadLocation Get(FileTypeEnum fileType)
        {
            switch (fileType)
            {
                case FileTypeEnum.FlashcardImage:
                    return FlashcardImageFolder;
                case FileTypeEnum.Temp:
                    return TempFolder;
            }
            throw new NotImplementedException();
        }

        public static UploadLocation Get(File file)
        {
            return Get((FileTypeEnum)file.FileTypeID);
        }

        public static string GetAbsoluteFilePathForLocation(File file)
        {
            return Get(file).GetAbsoluteFilePathForLocation(file.Filename);
        }

        public string GetAbsoluteFilePathForLocation(string filename)
        {
            return System.IO.Path.Combine(AbsolutePath, filename);
        }

        /// <summary>
        /// Location relative to executable
        /// </summary>
        public string GetRelativeFilePathForLocation(string filename)
        {
            var path = Path.Replace("~", "");

            return System.IO.Path.Combine(path, filename);
        }

        /// <summary>
        /// returns relative location with ~ in front. Location relative to app main folder?
        /// </summary>
        public string GetVirtualFilePathForLocation(string filename)
        {
            return "~" + GetRelativeFilePathForLocation(filename);
        }


    }
}
