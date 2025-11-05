// Pages/Index.cshtml.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SmartTourismSystem.Models;
using SmartTourismSystem.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartTourismSystem.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly SmartTourismService _tourismService;
        private readonly IGeminiService _geminiService;

        public IndexModel(
            ApplicationDbContext context,
            SmartTourismService tourismService,
            IGeminiService geminiService)
        {
            _context = context;
            _tourismService = tourismService;
            _geminiService = geminiService;
        }

        public List<TouristPlace> TopPlaces { get; set; } = new List<TouristPlace>();
        public List<SmartSuggestion> Suggestions { get; set; } = new List<SmartSuggestion>();
        public List<TouristPlace> SearchResults { get; set; } = new List<TouristPlace>();

        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }

        [BindProperty]
        public string UserMessage { get; set; }

        public string ChatResponse { get; set; }
        public bool IsAIResponding { get; set; }

        public async Task OnGetAsync()
        {
            await LoadPageData();
        }

        public async Task<IActionResult> OnPostSearchAsync()
        {
            SearchResults = _tourismService.SearchPlaces(SearchTerm);
            TopPlaces = SearchResults;
            await LoadPageData();
            return Page();
        }

        public async Task<IActionResult> OnPostChatAsync()
        {
            if (string.IsNullOrEmpty(UserMessage))
            {
                ChatResponse = "لطفاً پیامی وارد کنید";
                await LoadPageData();
                return Page();
            }

            IsAIResponding = true;
            var userId = HttpContext.Session.GetInt32("UserId") ?? 1;

            try
            {
                // استفاده از Gemini AI واقعی
                ChatResponse = await _geminiService.GetChatResponseAsync(UserMessage);

                // ذخیره چت در دیتابیس
                await _tourismService.SaveChatAsync(userId, UserMessage, ChatResponse, "AI Chat");
            }
            catch (TaskCanceledException)
            {
                ChatResponse = "زمان درخواست به پایان رسید. لطفاً دوباره تلاش کنید.";
            }
            catch (Exception ex)
            {
                ChatResponse = $"خطا در ارتباط با دستیار هوشمند: {ex.Message}";
            }
            finally
            {
                IsAIResponding = false;
            }

            await LoadPageData();
            return Page();
        }

        private async Task LoadPageData()
        {
            // دریافت بهترین مکان‌های تهران
            TopPlaces = await _context.TouristPlaces
                .Include(p => p.Category)
                .Where(p => p.City == "تهران")
                .OrderByDescending(p => p.AverageRating)
                .Take(8)
                .ToListAsync();

            // اگر کاربر لاگین کرده، پیشنهادات هوشمند
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId.HasValue)
            {
                Suggestions = _tourismService.GetSmartSuggestions(userId.Value);
            }

            // اگر جستجو انجام شده
            if (!string.IsNullOrEmpty(SearchTerm) && SearchResults.Any())
            {
                TopPlaces = SearchResults;
            }
        }
    }
}