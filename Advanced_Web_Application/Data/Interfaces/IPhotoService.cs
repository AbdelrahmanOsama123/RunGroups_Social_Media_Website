using CloudinaryDotNet.Actions;

namespace Advanced_Web_Application.Data.Interfaces
{
    public interface IPhotoService
    {
        Task<ImageUploadResult> AddPhoteAsync(IFormFile file);

        Task<DeletionResult> DeletePhotoAsync(string PublicId);
    }
}