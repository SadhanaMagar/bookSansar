using System.Collections.Generic;
using System.Threading.Tasks;
using bookSansar.DTO;
using bookSansar.Entities;

namespace bookSansar.Services
{
    public interface IReviewService
    {
        Task<ReviewResponseDTO> CreateReviewAsync(CreateReviewDTO reviewDto, string userId);
        Task<ReviewResponseDTO> GetReviewByIdAsync(int id);
        Task<IEnumerable<ReviewResponseDTO>> GetReviewsByBookIdAsync(int bookId);
        Task<ReviewResponseDTO> UpdateReviewAsync(int id, CreateReviewDTO reviewDto, string userId);
        Task DeleteReviewAsync(int id, string userId);
        Task<double> GetAverageRatingForBookAsync(int bookId);
    }
} 