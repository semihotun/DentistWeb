using Core.Utilities.Results;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.File
{
    public interface IFileService
    {
        IDataResult<FileModel> Add(string path, IFormFile file, MimeTypeEnum mimeTypeEnum, double maximumSizeLimit = 50);
        IResult Delete(string fileUrl);
        IDataResult<byte[]> PhotoToByteArray(IFormFile file);
    }
}
