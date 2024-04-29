using HagaDropsIt.Shared.Entities;
using HagaDropsIt.Shared.Interfaces;

namespace HagaDropsIt.API.Extensions;

public static class ReviewEndpointExtensions
{
	public static IEndpointRouteBuilder MapReviewEndpoints(this IEndpointRouteBuilder app)
	{
		var group = app.MapGroup("api/reviews");

		group.MapGet("/", GetAllReviews);
		group.MapGet("/{reviewId}", GetReviewById);
		group.MapGet("/user/{userId}", GetReviewsByUserId);
		group.MapGet("/product/{productId}", GetReviewsByProductId);
		group.MapPost("/", AddReview);
		group.MapPut("/", UpdateReview);
		group.MapDelete("/{reviewId}", DeleteReview);

		return app;
	}

	private static async Task<IResult> GetAllReviews(IReviewService<Review> repo)
	{
		var reviews = await repo.GetAllReviews();

		return reviews == null ? Results.NotFound() : Results.Ok(reviews);
	}

	private static async Task<IResult> GetReviewById(IReviewService<Review> repo, int reviewId)
	{
		var review = await repo.GetReviewById(reviewId);

		return review == null ? Results.NotFound($"No reviews exist with Id: {reviewId}") : Results.Ok(review);
	}

	private static async Task<IResult> GetReviewsByUserId(IReviewService<Review> repo, string userGuid)
	{
		var reviews = await repo.GetReviewsByUserId(userGuid);

		return reviews == null ? Results.NotFound($"No reviews exist for userId: {userGuid}") : Results.Ok(reviews);
	}

	private static async Task<IResult> GetReviewsByProductId(IReviewService<Review> repo, int productId)
	{
		var reviews = await repo.GetReviewsByProductId(productId);

		return reviews == null ? Results.NotFound($"No reviews exist for productId: {productId}") : Results.Ok(reviews);
	}

	private static async Task<IResult> AddReview(IReviewService<Review> repo, Review newReview)
	{
		if (newReview is null)
		{
			return Results.BadRequest("Review body cannot be null!");
		}

		await repo.AddReview(newReview);

		return Results.Created($"/api/review/{newReview.Id}", newReview);
	}

	private static async Task<IResult> UpdateReview(IReviewService<Review> repo, Review updatedReview)
	{
		if (updatedReview is null)
		{
			return Results.BadRequest("Review cannot be null!");
		}

		await repo.UpdateReview(updatedReview);

		return Results.Ok(updatedReview);
	}

	private static async Task<IResult> DeleteReview(IReviewService<Review> repo, int reviewId)
	{
		var review = await repo.GetReviewById(reviewId);

		if (review is null)
		{
			return Results.NotFound();
		}

		await repo.DeleteReview(reviewId);

		return Results.NoContent();
	}
}
