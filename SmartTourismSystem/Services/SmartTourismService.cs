// Services/SmartTourismService.cs
using Microsoft.EntityFrameworkCore;
using SmartTourismSystem.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartTourismSystem.Services
{
    public class SmartTourismService
    {
        private readonly ApplicationDbContext _context;

        public SmartTourismService(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<SmartSuggestion> GetSmartSuggestions(int userId)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserId == userId);

            if (user == null || string.IsNullOrEmpty(user.Interests))
                return new List<SmartSuggestion>();

            var userInterests = user.Interests.Split(',').Select(i => i.Trim()).ToList();

            var suggestions = (from place in _context.TouristPlaces.Include(p => p.Category)
                               where userInterests.Any(interest =>
                                   place.Category.CategoryName.Contains(interest) ||
                                   place.Description.Contains(interest) ||
                                   place.PlaceName.Contains(interest))
                               orderby place.AverageRating descending
                               select new SmartSuggestion
                               {
                                   UserId = userId,
                                   PlaceId = place.PlaceId,
                                   Reason = $"مطابق با علاقمندی شما به {string.Join("، ", userInterests)}",
                                   ConfidenceScore = 0.8m,
                                   Place = place
                               }).Take(6).ToList();

            return suggestions;
        }

        public List<TouristPlace> SearchPlaces(string searchTerm = null, string city = null,
                                             string category = null, decimal minRating = 0)
        {
            var query = _context.TouristPlaces
                .Include(p => p.Category)
                .AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
                query = query.Where(p => p.PlaceName.Contains(searchTerm) ||
                                       p.Description.Contains(searchTerm));

            if (!string.IsNullOrEmpty(city))
                query = query.Where(p => p.City.Contains(city));

            if (!string.IsNullOrEmpty(category))
                query = query.Where(p => p.Category.CategoryName.Contains(category));

            if (minRating > 0)
                query = query.Where(p => p.AverageRating >= minRating);

            return query.OrderByDescending(p => p.AverageRating).ToList();
        }

        // متد جدید برای ذخیره چت
        public async Task SaveChatAsync(int userId, string userMessage, string aiResponse, string category)
        {
            var chat = new Aichat
            {
                UserId = userId,
                UserMessage = userMessage,
                Airesponse = aiResponse,
                Category = category,
                CreatedAt = DateTime.Now
            };

            _context.Aichats.Add(chat);
            await _context.SaveChangesAsync();
        }
    }
}