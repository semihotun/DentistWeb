using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.File
{
    public class MimeTypes
    {
        public static readonly List<string> Image = new List<string>() {
            ".jpg",
            ".jpeg",
            ".png",
            ".svg" ,
            ".webp"
        };
        public static readonly List<string> Video = new List<string>() {
            ".ogg",
            ".webm",
            ".wave",
            ".wav" ,
        };
        public static readonly List<string> Pdf = new List<string>(){
            ".pdf",
        };

        public static readonly Dictionary<MimeTypeEnum, List<string>> mimeTypesArray = new Dictionary<MimeTypeEnum, List<string>>()
        {
            { MimeTypeEnum.Image , Image },
            { MimeTypeEnum.Pdf , Pdf },
            { MimeTypeEnum.Video , Video },
        };

    }
}
