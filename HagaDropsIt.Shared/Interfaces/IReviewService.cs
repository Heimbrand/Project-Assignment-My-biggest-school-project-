namespace HagaDropsIt.Shared.Interfaces;

public interface IReviewService<T> where T : class
{
	Task<IEnumerable<T>> GetAllReviews();
	Task<T?> GetReviewById(int id);
	Task<IEnumerable<T>> GetReviewsByUserId(string userGuid);
	Task<IEnumerable<T>> GetReviewsByProductId(int productId);
	Task AddReview(T newReview);
	Task UpdateReview(T updatedReview);
	Task DeleteReview(int id);
}
