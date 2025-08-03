using UnityEngine;
using VContainer;
using VContainer.Unity;

public class GameLifetimeScope : LifetimeScope
{
    [SerializeField] private GroundSpawnerModel _groundSpawnerModel;
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
        builder.Register<PlayerModel>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
        builder.Register<PlayerMow>(Lifetime.Singleton).AsImplementedInterfaces();
        builder.Register<PlayerSeller>(Lifetime.Singleton).AsImplementedInterfaces();
    }
}