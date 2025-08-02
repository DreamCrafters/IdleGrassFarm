using UnityEngine;
using VContainer;
using VContainer.Unity;

public class GameLifetimeScope : LifetimeScope
{
    [SerializeField] private GroundSpawnerService _groundSpawnerData;
    [SerializeField] private FarmSettings _farmSettings;

    protected override void Configure(IContainerBuilder builder)
    {
        RegisterFarmTiles(builder);
    }

    private void RegisterFarmTiles(IContainerBuilder builder)
    {
        builder.RegisterInstance(_farmSettings);
        builder.RegisterInstance(_groundSpawnerData);
        builder.Register<FarmFieldTileManager>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
        builder.Register<FarmFieldTileService>(Lifetime.Transient);
        builder.Register<FarmFieldTilePresenter>(Lifetime.Transient);
        builder.Register<FarmTileFactory>(Lifetime.Singleton);
        builder.Register<GroundSpawnerPresenter>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
    }
}
