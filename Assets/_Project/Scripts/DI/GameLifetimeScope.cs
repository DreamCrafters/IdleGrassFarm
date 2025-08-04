using UnityEngine;
using VContainer;
using VContainer.Unity;

public class GameLifetimeScope : LifetimeScope
{
    [SerializeField] private GroundSpawnerModel _groundSpawnerModel;
    [SerializeField] private PlayerCornsModel _playerCornsModel;
    [SerializeField] private FarmConfig _farmConfig;
    [SerializeField] private PlayerConfig _playerConfig;
    [SerializeField] private PlayerMovement _playerMovement;

    protected override void Configure(IContainerBuilder builder)
    {
        RegisterConfigs(builder);
        RegisterFarmTilesSpawn(builder);
        RegisterPlayerComponents(builder);
    }

    private void RegisterConfigs(IContainerBuilder builder)
    {
        builder.RegisterInstance(_farmConfig);
        builder.RegisterInstance(_playerConfig);
    }

    private void RegisterFarmTilesSpawn(IContainerBuilder builder)
    {
        builder.RegisterInstance(_groundSpawnerModel);
        builder.Register<FarmFieldTileManager>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
        builder.Register<FarmFieldTilePresenter>(Lifetime.Transient);
        builder.Register<FarmTileFactory>(Lifetime.Singleton);
        builder.Register<GroundSpawnerPresenter>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
    }

    private void RegisterPlayerComponents(IContainerBuilder builder)
    {
        builder.RegisterInstance(_playerMovement);
        builder.RegisterInstance(_playerCornsModel);
        builder.Register<PlayerModel>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
        builder.Register<PlayerMowPresenter>(Lifetime.Singleton).AsImplementedInterfaces();
        builder.Register<PlayerSellPresenter>(Lifetime.Singleton).AsImplementedInterfaces();
        builder.Register<PlayerCornsPresenter>(Lifetime.Singleton).AsImplementedInterfaces();
        builder.Register<PlayerBuyPresenter>(Lifetime.Singleton).AsImplementedInterfaces();
    }
}