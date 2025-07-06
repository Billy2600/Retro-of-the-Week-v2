using RetroOfTheWeekShared.Models;

namespace RetroOfTheWeekFrontEnd.API.Interfaces
{
    public interface ICommentsApiService
    {
        Task<CommentModel[]?> GetCommentsForPost(int postId);
    }
}
