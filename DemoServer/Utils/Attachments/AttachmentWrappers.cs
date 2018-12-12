using System.IO;
using Microsoft.AspNetCore.Http;

namespace DemoServer.Utils.Attachments
{
    public abstract class AttachmentWrapper
    {
        public abstract Stream OpenStream();
    }

    public class FormFileAttachmentWrapper : AttachmentWrapper
    {
        private readonly IFormFile _formFile;

        public FormFileAttachmentWrapper(IFormFile formFile)
        {
            _formFile = formFile;
        }

        public override Stream OpenStream() => _formFile.OpenReadStream();
    }

    public class LocalFileAttachmentWrapper : AttachmentWrapper
    {
        private readonly string _filePath;

        public LocalFileAttachmentWrapper(string filePath)
        {
            _filePath = filePath;
        }

        public override Stream OpenStream() => new FileStream(_filePath, FileMode.Open);
    }
}
