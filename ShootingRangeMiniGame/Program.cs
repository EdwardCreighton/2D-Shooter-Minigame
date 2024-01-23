using ShootingRangeMiniGame.Assets;
using ShootingRangeMiniGame.Engine.Core;

namespace ShootingRangeMiniGame
{
	static class Program
	{
		[STAThread]
		static void Main()
		{
			// To customize application configuration such as set high DPI settings or default font,
			// see https://aka.ms/applicationconfiguration.
			ApplicationConfiguration.Initialize();

			var gameplaySystemsLoader = new ShootingRangeMiniGameLoader();
			
			Application.Run(new App(gameplaySystemsLoader));
		}
	}
}