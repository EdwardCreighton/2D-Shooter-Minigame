using Leopotam.Ecs;
using ShootingRangeMiniGame.Engine.Core;
using ShootingRangeMiniGame.Assets.Data;
using ShootingRangeMiniGame.Assets.Components;
using ShootingRangeMiniGame.Assets.Markers;

namespace ShootingRangeMiniGame.Assets.Systems
{
	public class GameProgressSystem : IEcsInitSystem, IEcsRunSystem
	{
		private EcsFilter<Weapon, PlayerMarker> _playerWeaponFilter;
		private EcsFilter<Projectile> _projectilesFilter;
		private EcsFilter<TargetMarker> _targetsFilter;

		private DataProvider _dataProvider;
		private App _app;

		public void Init()
		{
			ref var gameProgressData = ref _app.GameProgressData;
			gameProgressData.TimeLeft = _dataProvider.Time;
			gameProgressData.InitialTime = _dataProvider.Time;
			gameProgressData.InitialTargetsCount = _dataProvider.TargetsCount;
			gameProgressData.InitialBulletsCount = _dataProvider.BulletsCount;
		}

		public void Run()
		{
			ref var weapon = ref _playerWeaponFilter.Get1(0);
			
			ref var gameProgressData = ref _app.GameProgressData;
			gameProgressData.TimeLeft -= _app.UpdateDeltaTime;
			gameProgressData.TargetsLeft = _targetsFilter.GetEntitiesCount();
			gameProgressData.WeaponReady = weapon.ReloadElapsed >= weapon.ReloadDuration;
			gameProgressData.BulletsLeft = weapon.BulletsLeft;
			gameProgressData.FreeProjectiles = _projectilesFilter.GetEntitiesCount();
		}
	}
}