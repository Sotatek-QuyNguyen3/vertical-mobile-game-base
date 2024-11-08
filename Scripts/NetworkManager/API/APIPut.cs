using Newtonsoft.Json;
using UnityEngine;
public class APIPut
{

	public static APIRequest APIClaimExpObstacle(string islandId, string obstacleId, string type)
	{
		APIRequest request = new APIRequest();
		request.url = string.Format("{0}islands/my/islands/{1}/claim-exp", GameConstants.HOST, islandId);
		var data = new
		{
			id = int.Parse(obstacleId),
			type = type
		};
		request.body = JsonConvert.SerializeObject(data);
		return request;
	}


}

