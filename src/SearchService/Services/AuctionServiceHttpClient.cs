using MongoDB.Entities;
using SearchService.Models;

public class AuctionServiceHttpClient
{
	private readonly HttpClient _httpClient;
	private readonly IConfiguration _config;

	public AuctionServiceHttpClient(HttpClient httpClient, IConfiguration config)
	{
		_httpClient = httpClient;
		_config = config;
	}

	public async Task<List<Item>> GetItemsForSearchDB()
	{
		var lastUpdated = await DB.Find<Item, string>().Sort(x => x.Descending(d => d.UpdatedAt)).Project(x => x.UpdatedAt.ToString()).ExecuteFirstAsync();

		return await _httpClient.GetFromJsonAsync<List<Item>>(_config["AuctionServiceUrl"] + "/api/auctions?date=" + lastUpdated);
	}
}