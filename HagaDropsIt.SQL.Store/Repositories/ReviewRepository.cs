using HagaDropsIt.Shared.Entities;
using HagaDropsIt.Shared.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HagaDropsIt.SQL.Store.Repositories;

public class ReviewRepository(StoreDbContext context) : IReviewService<Review>
{
	public async Task<IEnumerable<Review>> GetAllReviews()
	{
		return await context.Reviews.ToListAsync();
	}

	public async Task<Review?> GetReviewById(int id)
	{
		return await context.Reviews.FindAsync(id);
	}

	public async Task<IEnumerable<Review>> GetReviewsByUserId(string userGuid)
	{
		return await context.Reviews.Where(r => r.UserGuid == Guid.Parse(userGuid)).ToListAsync();
	}

	public async Task<IEnumerable<Review>> GetReviewsByProductId(int productId)
	{
		return await context.Reviews.Where(r => r.ProductId == productId).ToListAsync();
	}

	public async Task AddReview(Review newReview)
	{
		await context.Reviews.AddAsync(newReview);
		await context.SaveChangesAsync();
	}

	public async Task UpdateReview(Review updatedReview)
	{
		var oldReview = await context.Reviews.FindAsync(updatedReview.Id);
		if (oldReview == null)
		{
			return;
		}

		oldReview.Title = updatedReview.Title;
		oldReview.Content = updatedReview.Content;
		oldReview.Rating = updatedReview.Rating;
		oldReview.DateCreated = updatedReview.DateCreated;

		await context.SaveChangesAsync();
	}

	public async Task DeleteReview(int id)
	{
		var review = await context.Reviews.FindAsync(id);
		if (review == null)
		{
			return;
		}

		context.Reviews.Remove(review);
		await context.SaveChangesAsync();
	}
}
