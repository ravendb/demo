using System;
using Microsoft.AspNetCore.Http;

namespace DemoServer.Utils
{
    public static class FileValidator
    {
        private const int MaxFileSizeInMb = 5;
        private const long MaxFileSizeInBytes = MaxFileSizeInMb * 1024 * 1024;

        public static void Check(IFormFile formFile)
        {
            if (formFile.Length > MaxFileSizeInBytes)
                throw new InvalidOperationException($"File is invalid: its size is larger than {MaxFileSizeInMb} MB.");
        }
    }
}
