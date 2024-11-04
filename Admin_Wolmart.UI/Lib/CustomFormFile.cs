using Microsoft.AspNetCore.Components.Forms;
namespace Admin_Wolmart.UI.Lib
{
    public class CustomFormFile : IFormFile
    {
        private readonly IBrowserFile _browserFile;

        public CustomFormFile(IBrowserFile browserFile)
        {
            _browserFile = browserFile;
        }

        public string ContentType => _browserFile.ContentType;

        public string ContentDisposition => $"form-data; name=\"{Name}\"; filename=\"{FileName}\"";

        public IHeaderDictionary Headers => new HeaderDictionary();

        public long Length => _browserFile.Size;

        public string Name => _browserFile.Name;

        public string FileName => _browserFile.Name;

        public void CopyTo(Stream target)
        {
            using var stream = _browserFile.OpenReadStream();
            stream.CopyTo(target);
        }

        public async Task CopyToAsync(Stream target, CancellationToken cancellationToken = default)
        {
            using var stream = _browserFile.OpenReadStream();
            await stream.CopyToAsync(target, cancellationToken);
        }

        public Stream OpenReadStream()
        {
            return _browserFile.OpenReadStream();
        }
    }
}
