using System.Net.Http;
using System.Threading.Tasks;

namespace CineWave.Helpers;

public class TMDBHelper
{
    private const string BaseUrl = "https://api.themoviedb.org/3/";

    private static readonly string ApiKey = PseudoRandomEncryptor.Decrypt("NmViYmRmOWJiNzVjM2E0ZjVkNDRlODhiNjdhODVmYmY=");

    public static async Task<string> GetMovieData(string movieTitle)
    {
        using var client = new HttpClient();
        var url = $"{BaseUrl}search/movie?api_key={ApiKey}&query={movieTitle}";
        var response = await client.GetAsync(url);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }

    public static string GetTopMovies(int limit = 20)
    {
        using var client = new HttpClient();
        var url = $"{BaseUrl}movie/top_rated?api_key={ApiKey}&page=1&per_page={limit}";
        var response = client.GetAsync(url).GetAwaiter().GetResult();
        response.EnsureSuccessStatusCode();
        return response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
    }

}