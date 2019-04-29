namespace SharedKernel.Abstractions.PLL.Media
{
    public interface IMedialLoadService
    {
        bool UploadFile(string fullFilePath);
        bool DownloadFile(string mediaName, string blobFileName);
    }
}