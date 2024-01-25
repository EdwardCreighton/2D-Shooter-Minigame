namespace ShootingRangeMiniGame.Engine.Core
{
	public struct GameProgressData
	{
		public bool GameResult;

		public int InitialTargetsCount;
		public int InitialTime;
		public int InitialBulletsCount;
		
		public float TimeLeft;
		public int TargetsLeft;
		public int FreeProjectiles;

		public int BulletsLeft;
		public bool WeaponReady;
	}
}