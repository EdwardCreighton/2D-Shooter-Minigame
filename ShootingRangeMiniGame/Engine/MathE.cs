using System.Numerics;

namespace ShootingRangeMiniGame.Engine
{
	public static class MathE
	{
		public const float Deg2Rad = 0.0174532925199433f;
		public const float Rad2Deg = 57.2957795130823209f;

		public static float Angle(this Vector2 v)
		{
			float angleRad = (float)Math.Atan2(v.Y, v.X);
			return 360f + angleRad * Rad2Deg;
		}
	}
}