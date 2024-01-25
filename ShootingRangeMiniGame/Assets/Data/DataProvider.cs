using System.Text.RegularExpressions;

namespace ShootingRangeMiniGame.Assets.Data
{
	public class DataProvider
	{
		public int TargetsCount { get; private set; }
		public float ProjectileSpeed { get; private set; }
		public float Time { get; private set; }
		public int BulletsCount { get; private set; }
		
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
				ProjectileSpeed = float.Parse(matches[1].Value);
				Time = float.Parse(matches[2].Value);
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