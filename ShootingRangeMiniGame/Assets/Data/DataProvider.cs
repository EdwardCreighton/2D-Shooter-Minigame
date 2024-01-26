using System.Numerics;
using System.Text.RegularExpressions;
using ShootingRangeMiniGame.Engine;

namespace ShootingRangeMiniGame.Assets.Data
{
	public class DataProvider
	{
		public int TargetsCount { get; private set; }
		public int ProjectileSpeed { get; private set; }
		public int Time { get; private set; }
		public int BulletsCount { get; private set; }

		public Point[] PlayerMesh { get; } =
		{
			new(-15, -15),
			new(15, -15),
			new(15, -7),
			new(55, -7),
			new(55, 7),
			new(15, 7),
			new(15, 15),
			new(-15, 15),
		};
		public Point[] TargetMesh { get; } =
		{
			new(-20, -20),
			new(20, -20),
			new(20, 20),
			new(-20, 20),
		};
		public Point[] ProjectileMesh { get; } =
		{
			new(-5, -5),
			new(5, -5),
			new(5, 5),
			new(-5, 5),
		};

		public BoundingBox TargetBoundingBox { get; } = new(-20f, -20f, 40f, 40f);
		public BoundingBox ProjectileBoundingBox { get; } = new(-5f, -5f, 10f, 10f);
		
		public DataProvider()
		{
			SetGameData();
		}

		private void SetGameData()
		{
			try
			{
				StreamReader streamReader = new StreamReader("input.txt");

				string data = streamReader.ReadToEnd();
				string pattern = @"\d+";
				MatchCollection matches = Regex.Matches(data, pattern);
				
				TargetsCount = int.Parse(matches[0].Value);
				ProjectileSpeed = int.Parse(matches[1].Value);
				Time = int.Parse(matches[2].Value);
				BulletsCount = int.Parse(matches[3].Value);
				
				streamReader.Close();
			}
			catch (Exception e)
			{
				TargetsCount = 20;
				ProjectileSpeed = 700;
				Time = 60;
				BulletsCount = 35;
			}
		}
	}
}