namespace SophaTemp.Services
{
    public interface IUploadFileService
    {
        public string Upload(IFormFile file, string directory, bool encrypt);
    }
}
