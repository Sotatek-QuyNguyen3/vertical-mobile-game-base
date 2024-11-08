using Newtonsoft.Json;
public class APIPost
{
	//API get login data from server. This use for version test and remove when game release
	

	//API create building, Input row col is building coordinate, building code generate base on type of building. 
	public static APIRequest APICreateBuilding(int row, int col, int islandId, string buildingCode)
	{
		APIRequest request = new APIRequest();
		request.url = string.Format("{0}islands/my/shop/buildings", GameConstants.HOST);

		var data = new
		{
			coordStartPoint = new
			{
				row = row,
				col = col
			},
			buildingCode = buildingCode,
			islandId = islandId
		};
		request.body = JsonConvert.SerializeObject(data);
		return request;
	}

	public static APIRequest APIOpenChest()
	{
		APIRequest request = new APIRequest();
		request.url = string.Format("{0}islands/my/users/open-chest", GameConstants.HOST);

		var data = new
		{
		};
		request.body = JsonConvert.SerializeObject(data);
		return request;
	}

	public static APIRequest APIPerformBattle()
	{
		APIRequest request = new APIRequest();
		request.url = string.Format("{0}islands/my/battles", GameConstants.HOST);

		var data = new
		{
		};
		request.body = JsonConvert.SerializeObject(data);
		return request;
	}

	public static APIRequest APIGetEnemy()
	{
		APIRequest request = new APIRequest();
		request.url = string.Format("{0}islands/my/battles/enemy", GameConstants.HOST);

		var data = new
		{
		};
		request.body = JsonConvert.SerializeObject(data);
		return request;
	}
	
	public static APIRequest APIGrowCrop(string farmId, int cropType)
	{
		APIRequest request = new APIRequest();
		request.url = string.Format("{1}islands/my/farms/{0}/grow-crops", farmId, GameConstants.HOST);

		var data = new
		{
			cropType = cropType
		};
		request.body = JsonConvert.SerializeObject(data);
		return request;
	}
	#region farm 
	public static APIRequest APIHarvestCrop(string farmId)
	{
		APIRequest request = new APIRequest();
		request.url = string.Format("{1}islands/my/farms/{0}/harvest", farmId, GameConstants.HOST);

		var data = new
		{

		};
		request.body = JsonConvert.SerializeObject(data);
		return request;
	}

	public static APIRequest APIFarmLevelUp(string farmId)
	{
		APIRequest request = new APIRequest();
		request.url = string.Format("{1}islands/my/farms/{0}/levelup", farmId, GameConstants.HOST);

		var data = new
		{

		};
		request.body = JsonConvert.SerializeObject(data);
		return request;
	}
	#endregion

	#region bunihouse
	
	public static APIRequest APIMoveBunicorn(int oldHouseId, int newHouseId, int[] bunicornIds)
	{
		APIRequest request = new APIRequest();
		request.url = string.Format("{0}islands/my/buni-houses/move-bunicorn", GameConstants.HOST);
		var data = new
		{
			bunicornIds = bunicornIds,
			buniHouseIdOld = oldHouseId,
			buniHouseIdNew = newHouseId
		};
		request.body = JsonConvert.SerializeObject(data);
		return request;
	}

	public static APIRequest APIMoveTrainerToBuniHouse(int oldHouseId, int newHouseId, int[] trainerIds)
	{
		APIRequest request = new APIRequest();
		request.url = string.Format("{0}islands/my/buni-houses/move-trainer", GameConstants.HOST);
		var data = new
		{
			trainerIds = trainerIds,
			buniHouseIdOld = oldHouseId,
			buniHouseIdNew = newHouseId
		};
		request.body = JsonConvert.SerializeObject(data);
		return request;
	}
	public static APIRequest APIAddBunicorn(string buniHouseId, int[] bunicornIds)
	{
		APIRequest request = new APIRequest();
		request.url = string.Format("{1}islands/my/buni-houses/{0}/bunicorns", buniHouseId, GameConstants.HOST);

		var data = new
		{
			bunicornIds = bunicornIds
		};
		request.body = JsonConvert.SerializeObject(data);
		return request;
	}

	public static APIRequest APIAddTrainers(string buniHouseId, int[] trainerIds)
	{
		APIRequest request = new APIRequest();
		request.url = string.Format("{1}islands/my/buni-houses/{0}/trainers", buniHouseId, GameConstants.HOST);

		var data = new
		{
			trainerIds = trainerIds
		};
		request.body = JsonConvert.SerializeObject(data);
		return request;
	}

	public static APIRequest APIMoveBunicorn(int[] bunicornIds, int oldBunihouseId, int newBunihouseId)
	{
		APIRequest request = new APIRequest();
		request.url = string.Format("{0}my/buni-houses/move-bunicorn", GameConstants.HOST);

		var data = new
		{
			bunicornIds = bunicornIds,
			buniHouseIdOld = oldBunihouseId,
			buniHouseIdNew = newBunihouseId
		};
		request.body = JsonConvert.SerializeObject(data);
		return request;
	}

	public static APIRequest APIMoveTrainer(int[] trainerIds, int oldBunihouseId, int newBunihouseId)
	{
		APIRequest request = new APIRequest();
		request.url = string.Format("{0}my/buni-houses/move-trainer", GameConstants.HOST);

		var data = new
		{
			trainerIds = trainerIds,
			buniHouseIdOld = oldBunihouseId,
			buniHouseIdNew = newBunihouseId
		};
		request.body = JsonConvert.SerializeObject(data);
		return request;
	}

	public static APIRequest APIHarvestBuni(string buniId)
	{
		APIRequest request = new APIRequest();
		request.url = string.Format("{1}islands/my/buni-houses/{0}/harvest", buniId, GameConstants.HOST);

		var data = new
		{

		};
		request.body = JsonConvert.SerializeObject(data);
		return request;
	}
	#endregion

	public static APIRequest APIFeedBuni(string buniCornId)
	{
		APIRequest request = new APIRequest();
		request.url = string.Format("{0}bunicorns/my/bunicorns/{1}/feed-bunicorn", GameConstants.HOST, buniCornId);
		var data = new
		{

		};
		request.body = JsonConvert.SerializeObject(data);
		return request;
	}

	public static APIRequest APIAddBuniIntoBuniHouse(string buniHouseId, int[] arrId)
	{
		APIRequest request = new APIRequest();
		request.url = string.Format("{0}islands/my/buni-houses/{1}/bunicorns", GameConstants.HOST, buniHouseId);
		var data = new
		{
			bunicornIds = arrId
		};
		request.body = JsonConvert.SerializeObject(data);
		return request;
	}
	public static APIRequest APICreateTeam(int[] bunicornIds)
	{
		APIRequest request = new APIRequest();
		request.url = string.Format("{0}islands/my/teams", GameConstants.HOST);
		var data = new
		{
			bunicornIds = bunicornIds
		};
		request.body = JsonConvert.SerializeObject(data);
		return request;
	}

	public static APIRequest APIUpdateTeam(int[] bunicornIds)
	{
		APIRequest request = new APIRequest();
		request.url = string.Format("{0}islands/my/teams/update", GameConstants.HOST);
		var data = new
		{
			bunicornIds = bunicornIds
		};
		request.body = JsonConvert.SerializeObject(data);
		return request;
	}

	public static APIRequest APICheatBalance(int gold = 1, int food = 1, int burInGame = 1, int buniInGame = 1)
	{
		APIRequest request = new APIRequest();
		request.url = string.Format("{0}islands/my/mock/change-balance-account", GameConstants.HOST);

		var data = new
		{
			burInGame = burInGame.ToString(),
			buniInGame = buniInGame.ToString(),
			gold = gold.ToString(),
			food = food.ToString()
		};
		request.body = JsonConvert.SerializeObject(data);
		return request;
	}
	public static APIRequest APIUnlockIsland(string islandId)
	{
		APIRequest request = new APIRequest();
		request.url = string.Format("{0}islands/my/islands/{1}/unlock-island", GameConstants.HOST, islandId);
		var data = new
		{
		};
		request.body = JsonConvert.SerializeObject(data);
		return request;
	}


	public static APIRequest APIResetAccount()
	{
		APIRequest request = new APIRequest();
		request.url = string.Format(string.Format("{0}islands/my/mock/reset-account", GameConstants.HOST));
		var data = new
		{
		};
		request.body = JsonConvert.SerializeObject(data);
		return request;
	}
	public static APIRequest APIClaimTrainerMentoring(string mentoringHouseId)
	{
		APIRequest request = new APIRequest();
		request.url = string.Format("{0}islands/my/mentoring-houses/{1}/claim-trainer", GameConstants.HOST, mentoringHouseId);
		var data = new
		{
		};
		request.body = JsonConvert.SerializeObject(data);
		return request;
	}


}

public class APIRequest
{
	public string url;
	public string body;
}
