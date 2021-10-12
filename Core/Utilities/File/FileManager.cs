using Core.Utilities.Results;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Core.Utilities.File
{
 
    public class FileManager: IFileService
    {
        private readonly IConfiguration _configuration;
        public FileManager(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        
        public IDataResult<FileModel> Add(string path, IFormFile file, MimeTypeEnum mimeTypeEnum, double maximumSizeLimit = 50)
        {
            var shieldResult = Shield(file, mimeTypeEnum, maximumSizeLimit);
            if (shieldResult.Success)
            {
                var mainFilePath = _configuration.GetSection("FilesPath").Value;
                var directoryPath = mainFilePath + path;
                var fileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                var filepath = directoryPath + fileName;
                CheckAndCreateDirectory(directoryPath);
                using (var fileStream = new FileStream(filepath, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
                var fileModel = new FileModel();
                fileModel.Path = path + fileName;
                return new SuccessDataResult<FileModel>(fileModel);
            }
            else
            {
                return new ErrorDataResult<FileModel>(shieldResult.Message);
            }
        }
        public IResult Delete(string fileUrl)
        {
            var mainFilePath = _configuration.GetSection("FilesPath").Value;
            var deletedFile = mainFilePath + fileUrl;
            System.IO.File.Delete(deletedFile);
            return new SuccessResult();
        }

        public IDataResult<byte[]> PhotoToByteArray(IFormFile file)
        {
            using (var stream = new MemoryStream())
            {
                file.CopyTo(stream);
                return new SuccessDataResult<byte[]>(stream.ToArray());
            }
        }

        private static void CheckAndCreateDirectory(string directory)
        {
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
        }

        private static double ConvertByteToMb(string byteSize)
        {
            var newByteSize = byteSize.Replace(".", ",");
            var size = Convert.ToDouble(newByteSize);
            return size / 1048576;
        }

        private static IResult CheckIfFileSizeIsWithinLimits(IFormFile file, double maximumSizeLimit)
        {
            var requstFileSize = Math.Round(ConvertByteToMb(file.Length.ToString()), 2);
            if (requstFileSize <= maximumSizeLimit)
            {
                return new SuccessResult();
            }
            return new ErrorResult($"Dosya boyutu cok fazla. Yuklenen dosyanin boyutu: {requstFileSize} " +
                $"MB - Kabul edilen en fazla dosya boyutu: {maximumSizeLimit} MB");
        }

        private static IResult CheckIfMimeType(IFormFile file, MimeTypeEnum mimeTypeEnum)
        {
            var fileMimeType = Path.GetExtension(file.FileName);
            foreach (var item in MimeTypes.mimeTypesArray)
            {
                if (mimeTypeEnum == item.Key && item.Value.Find(x => x.Equals(fileMimeType)) != null)
                {
                    return new SuccessResult();
                }
            }
            return new ErrorResult("Dosya TÜrü Kabul edilemiyor");
        }

        private static IResult Shield(IFormFile file, MimeTypeEnum mimeTypeEnum, double maximumSizeLimit)
        {
            var result = Business.BusinessRules.Run(
                CheckIfMimeType(file, mimeTypeEnum),
                CheckIfFileSizeIsWithinLimits(file, maximumSizeLimit)
            );
            return result;
        }

    }
}
