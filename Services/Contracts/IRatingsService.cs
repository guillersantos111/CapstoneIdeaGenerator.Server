﻿using CapstoneIdeaGenerator.Server.Entities.DTOs;
using CapstoneIdeaGenerator.Server.Entities.Models;

namespace CapstoneIdeaGenerator.Server.Services.Contracts
{
    public interface IRatingsService
    {
        Task<bool> SubmitRating(int capstoneId, int ratingValue, string userId, string title);
        Task<IEnumerable<RatingRequestDTO>> GetAllRatings();
        Task<IEnumerable<Ratings>> GetAllRatingsDetailes();
    }
}
