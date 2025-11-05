// Services/GeminiService.cs
using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SmartTourismSystem.Services
{
    public interface IGeminiService
    {
        Task<string> GetChatResponseAsync(string userMessage);
    }

    public class GeminiService : IGeminiService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        private readonly JsonSerializerOptions _jsonOptions;

        public GeminiService(HttpClient httpClient, string apiKey)
        {
            _httpClient = httpClient;
            _apiKey = apiKey;
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };

            // تنظیم هدرها
            _httpClient.DefaultRequestHeaders.Add("User-Agent", "SmartTourismSystem/1.0");
            _httpClient.Timeout = TimeSpan.FromSeconds(30);
        }

        public async Task<string> GetChatResponseAsync(string userMessage)
        {
            try
            {
                // ساخت درخواست مشابه کد پایتون
                var requestBody = new
                {
                    contents = new[]
                    {
                        new
                        {
                            parts = new[]
                            {
                                new
                                {
                                    text = $"شما یک دستیار هوشمند گردشگری هستید که در زمینه گردشگری ایران تخصص دارید. " +
                                           $"لطفاً به زبان فارسی روان و دقیق پاسخ دهید. " +
                                           $"در پاسخ‌های خود مکان‌های گردشگری واقعی در ایران را پیشنهاد دهید. " +
                                           $"سوال کاربر: {userMessage}"
                                }
                            }
                        }
                    },
                    generationConfig = new
                    {
                        temperature = 0.7,
                        topK = 40,
                        topP = 0.95,
                        maxOutputTokens = 1024,
                    }
                };

                var json = JsonSerializer.Serialize(requestBody, _jsonOptions);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                // استفاده از مدل gemini-2.0-flash (مشابه gemini-2.5-flash)
                var url = $"https://generativelanguage.googleapis.com/v1beta/models/gemini-2.0-flash:generateContent?key={_apiKey}";

                Console.WriteLine($"Sending request to Gemini API...");
                Console.WriteLine($"URL: {url}");

                var response = await _httpClient.PostAsync(url, content);
                var responseContent = await response.Content.ReadAsStringAsync();

                Console.WriteLine($"Response Status: {response.StatusCode}");

                if (response.IsSuccessStatusCode)
                {
                    var geminiResponse = JsonSerializer.Deserialize<GeminiResponse>(responseContent, _jsonOptions);

                    if (geminiResponse?.Candidates?.Length > 0 &&
                        geminiResponse.Candidates[0].Content?.Parts?.Length > 0)
                    {
                        var aiResponse = geminiResponse.Candidates[0].Content.Parts[0].Text;
                        Console.WriteLine($"AI Response: {aiResponse}");
                        return aiResponse;
                    }
                    else
                    {
                        Console.WriteLine("No content in response");
                        return "پاسخی از سرویس دریافت نشد. لطفاً دوباره تلاش کنید.";
                    }
                }
                else
                {
                    Console.WriteLine($"Error Response: {responseContent}");
                    return $"خطا در سرویس: {response.StatusCode} - {responseContent}";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return GenerateFallbackResponse(userMessage);
            }
        }

        private string GenerateFallbackResponse(string userMessage)
        {
            // پاسخ پیش‌فرض در صورت خطا
            userMessage = userMessage.ToLower();

            if (userMessage.Contains("تاریخی") || userMessage.Contains("موزه") || userMessage.Contains("کاخ"))
            {
                return "برای بازدید از مکان‌های تاریخی، پیشنهاد می‌کنم:\n\n" +
                       "• کاخ سعدآباد تهران - مجموعه‌ای از کاخ‌های سلطنتی\n" +
                       "• باغ فردوس - موزه سینما در فضایی تاریخی\n" +
                       "• کاخ گلستان - از آثار میراث جهانی یونسکو\n" +
                       "• نقش جهان اصفهان - میدان تاریخی با معماری بی‌نظیر";
            }
            else if (userMessage.Contains("طبیعت") || userMessage.Contains("کوه") || userMessage.Contains("پیاده‌روی"))
            {
                return "برای گردش در طبیعت، این مقاصد را توصیه می‌کنم:\n\n" +
                       "• درکه تهران - مسیرهای کوهنوردی و هوای پاک\n" +
                       "• جنگل ناهارخوران گرگان - طبیعت سرسبز شمال\n" +
                       "• دشت لار - منطقه‌ای ییلاقی نزدیک تهران\n" +
                       "• آبشار شوشتر - مجموعه‌ای تاریخی-طبیعی";
            }
            else if (userMessage.Contains("ساحل") || userMessage.Contains("دریا") || userMessage.Contains("دریایی"))
            {
                return "برای سفر به مناطق ساحلی:\n\n" +
                       "• چمخاله لنگرود - ساحل شنی و آرام\n" +
                       "• کیش - جزیره‌ای با امکانات تفریحی کامل\n" +
                       "• رامسر - ساحل و هتل‌های دیدنی\n" +
                       "• نوشهر - جنگل و دریا در کنار هم";
            }
            else
            {
                return "سلام! من دستیار هوشمند گردشگری شما هستم.\n\n" +
                       "می‌توانم در زمینه‌های زیر به شما کمک کنم:\n" +
                       "• پیشنهاد مکان‌های تاریخی و فرهنگی\n" +
                       "• معرفی مقاصد طبیعت‌گردی\n" +
                       "• پیشنهاد مناطق ساحلی و دریا\n" +
                       "• اطلاعات درباره شهرهای مختلف ایران\n\n" +
                       "لطفاً سوال خود را دقیق‌تر بیان کنید.";
            }
        }
    }

    // کلاس‌های مدل برای پاسخ Gemini
    public class GeminiResponse
    {
        public Candidate[] Candidates { get; set; }
        public PromptFeedback PromptFeedback { get; set; }
    }

    public class Candidate
    {
        public Content Content { get; set; }
        public string FinishReason { get; set; }
        public int Index { get; set; }
    }

    public class Content
    {
        public Part[] Parts { get; set; }
        public string Role { get; set; }
    }

    public class Part
    {
        public string Text { get; set; }
    }

    public class PromptFeedback
    {
        public SafetyRating[] SafetyRatings { get; set; }
    }

    public class SafetyRating
    {
        public string Category { get; set; }
        public string Probability { get; set; }
    }
}