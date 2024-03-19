using System.Threading.Tasks;

public interface IAsssetProvider : IService
{
    public void Initialize();
    public Task<T> Load<T>(string adress) where T : class;
    public Task<int> GetObjectsCountByLabel(string label); 
    public void CleanUp();
}

