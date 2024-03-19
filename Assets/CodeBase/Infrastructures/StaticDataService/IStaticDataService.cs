public interface IStaticDataService: IService
{
    public void LoadStaticData();
    public T GetStaticData<T>() where T : StaticData;
}

