using ShootingRangeMiniGame.Engine.Core;
using ShootingRangeMiniGame.Assets;

namespace ShootingRangeMiniGame
{
	static class Program
	{
		[STAThread]
		static void Main()
		{
			ApplicationConfiguration.Initialize();
			Application.Run(new App(new ShootingRangeMiniGameLoader()));
		}
	}
}