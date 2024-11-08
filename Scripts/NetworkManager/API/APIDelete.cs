using Newtonsoft.Json;
public class APIDelete
{
    public static APIRequest APISellFarm(string id)
	{
		APIRequest request = new APIRequest();
		request.url = string.Format("{1}islands/my/farms/{0}/sell", id, GameConstants.HOST);
		var data = new
		{

		};
		request.body = JsonConvert.SerializeObject(data);

		return request;
		
	}

	public static APIRequest APIRemoveTrainer(string buniHouseId, int[] arrId)
	{
		APIRequest request = new APIRequest();
		request.url = string.Format("{0}islands/my/buni-houses/{1}/trainers", GameConstants.HOST, buniHouseId);
		var data = new
		{
			trainerIds = arrId
		};
		request.body = JsonConvert.SerializeObject(data);
		return request;
	}
}

