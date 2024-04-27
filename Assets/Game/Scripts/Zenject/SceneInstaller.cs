using Zenject;

public class SceneInstaller : MonoInstaller
{
    #region Fields
    public Grid Grid;
    #endregion
    public override void InstallBindings()
    {
        Container.Bind<GameManager>().AsSingle();
        Container.Bind<GridManager>().AsSingle(); 
        Container.Bind<GridTable>().FromComponentInHierarchy().AsSingle();
        Container.BindMemoryPool<Grid, Grid.Pool>().FromComponentInNewPrefab(Grid);
    }
}