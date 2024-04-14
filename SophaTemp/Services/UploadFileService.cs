namespace SophaTemp.Services
{
    public class UploadFileService : IUploadFileService
    {
        IWebHostEnvironment env;
        public UploadFileService(IWebHostEnvironment env)
        {
            this.env = env;
        }
        public string Upload(IFormFile file, string directory, bool encrypt )
        {
            string NewName = "";
            string[] AllowedExt = { ".jpg", ".png", ".jpeg" };
            string FileExt = Path.GetExtension(file.FileName);
            if (AllowedExt.Contains(FileExt.ToLower()))
            {
                // Encrypt the name if Encript parameter is true
                NewName = encrypt ? Guid.NewGuid() + file.FileName : file.FileName;
                string PathFile = Path.Combine(env.WebRootPath, directory , NewName);
                // Creating the folder if not exists
                System.IO.Directory.CreateDirectory(Path.Combine(env.WebRootPath, directory));
                using (FileStream stream = System.IO.File.Create(PathFile))
                {
                    file.CopyTo(stream);
                }
            }
            return "~/" + NewName;
        }
    }
}
