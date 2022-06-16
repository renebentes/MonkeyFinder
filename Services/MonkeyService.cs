using System.Net.Http.Json;

namespace MonkeyFinder.Services;

public class MonkeyService
{
    private readonly HttpClient _httpClient;

    private IEnumerable<Monkey> _monkeys;

    public MonkeyService()
    {
        _httpClient = new HttpClient();
    }

    public async Task<IEnumerable<Monkey>> GetMonkeys()
    {
        if (_monkeys is not null)
        {
            return _monkeys.AsEnumerable();
        }

        const string url = "https://montemagno.com/monkey.json";
        var response = await _httpClient.GetAsync(url);

        if (response.IsSuccessStatusCode)
        {
            _monkeys = await response.Content.ReadFromJsonAsync<IEnumerable<Monkey>>();
        }

        return _monkeys;
    }
}