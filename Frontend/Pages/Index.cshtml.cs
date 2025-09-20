using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace Frontend.Pages
{
    public class IndexModel : PageModel
    {
        private readonly HttpClient _http;

        public IndexModel(IHttpClientFactory httpFactory)
        {
            // HttpClient para HTTP en localhost (no HTTPS)
            _http = new HttpClient
            {
                BaseAddress = new Uri("http://localhost:5078/") // HTTP
            };
            _http.DefaultRequestHeaders.Accept.Clear();
            _http.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        [BindProperty]
        public string Username { get; set; }
        [BindProperty]
        public string Password { get; set; }

        public string Message { get; set; }

        public async Task<IActionResult> OnPostRegisterAsync()
        {
            var json = JsonSerializer.Serialize(new { Username, Password });
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _http.PostAsync("api/auth/register", content);

            Message = response.IsSuccessStatusCode
                ? "Usuario registrado correctamente."
                : "Error al registrar el usuario.";

            return Page();
        }

        public async Task<IActionResult> OnPostLoginAsync()
        {
            var json = JsonSerializer.Serialize(new { Username, Password });
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _http.PostAsync("api/auth/login", content);

            if (!response.IsSuccessStatusCode)
            {
                Message = "Credenciales incorrectas.";
                return Page();
            }

            var result = await response.Content.ReadAsStringAsync();
            using var doc = JsonDocument.Parse(result);
            var token = doc.RootElement.GetProperty("token").GetString();

            // Guardar token en session
            HttpContext.Session.SetString("JWT", token);

            return RedirectToPage("/Profile");
        }
    }
}
