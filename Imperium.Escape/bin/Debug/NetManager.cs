using Imperium.Client;
using NetData = System.Collections.Generic.Dictionary<string, object>;

namespace Caesar.Net
{
	public class NetManager : AbstractNetManager
	{
		

		public System.Boolean Login(System.String login, System.String password)
		{
			return Request<System.Boolean>(
				"Login",
				new NetData
				{
					{"login", login},
					{"password", password}
				});
		}

		public Imperium.CommonData.VisionDto GetVision()
		{
			return Request<Imperium.CommonData.VisionDto>(
				"GetVision",
				new NetData
				{
					
				});
		}

		public System.Boolean UpgradeBuilding(Province.Vector.Vector position, System.String name)
		{
			return Request<System.Boolean>(
				"UpgradeBuilding",
				new NetData
				{
					{"position", position},
					{"name", name}
				});
		}

		public System.Collections.Generic.Dictionary<System.String, System.Object>[] GetNews()
		{
			return Request<System.Collections.Generic.Dictionary<System.String, System.Object>[]>(
				"GetNews",
				new NetData
				{
					
				});
		}

		public System.Boolean AddResources()
		{
			return Request<System.Boolean>(
				"AddResources",
				new NetData
				{
					
				});
		}

		public System.Boolean Move(Province.Vector.Vector from, Province.Vector.Vector to)
		{
			return Request<System.Boolean>(
				"Move",
				new NetData
				{
					{"from", from},
					{"to", to}
				});
		}

		public System.Boolean BeginResearch(System.String name)
		{
			return Request<System.Boolean>(
				"BeginResearch",
				new NetData
				{
					{"name", name}
				});
		}

		public System.Int32 GetTechnologiesCount()
		{
			return Request<System.Int32>(
				"GetTechnologiesCount",
				new NetData
				{
					
				});
		}

		public System.Boolean Attack(Province.Vector.Vector from, Province.Vector.Vector to)
		{
			return Request<System.Boolean>(
				"Attack",
				new NetData
				{
					{"from", from},
					{"to", to}
				});
		}
	}
}