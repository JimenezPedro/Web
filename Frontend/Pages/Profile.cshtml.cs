using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Headers;
using System.Text.Json;

namespace Frontend.Pages
{
    public class ProfileModel : PageModel
    {
        private readonly HttpClient _http;

        public ProfileModel(IHttpClientFactory httpFactory)
        {
            _http = new HttpClient
            {
                BaseAddress = new Uri("http://localhost:5078/") // HTTP
            };
            _http.DefaultRequestHeaders.Accept.Clear();
            _http.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public string Username { get; set; }
        public string UserId { get; set; }

        public async Task<IActionResult> OnGet()
        {
            var token = HttpContext.Session.GetString("JWT");
            if (string.IsNullOrEmpty(token))
                return RedirectToPage("/Index");

            _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _http.GetAsync("api/auth/Profile");

            if (!response.IsSuccessStatusCode)
                return RedirectToPage("/Index");

            var result = await response.Content.ReadAsStringAsync();
            using var doc = JsonDocument.Parse(result);

            UserId = doc.RootElement.GetProperty("userId").GetString();
            Username = doc.RootElement.GetProperty("username").GetString();

            return Page();
        }
    }
}
