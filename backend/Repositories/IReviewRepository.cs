using System.Collections.Generic;
using System.Threading.Tasks;
using bookSansar.Entities;

namespace bookSansar.Repositories
{
    public interface IReviewRepository
    {
        Task<Review> CreateAsync(Review review);
        Task<Review> GetByIdAsync(int id);
        Task<IEnumerable<Review>> GetByBookIdAsync(int bookId);
        Task<Review> UpdateAsync(Review review);
        Task DeleteAsync(int id);
        Task<double> GetAverageRatingForBookAsync(int bookId);
    }
} 