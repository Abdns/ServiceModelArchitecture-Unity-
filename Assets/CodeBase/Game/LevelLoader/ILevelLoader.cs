using System.Threading.Tasks;

public interface ILevelLoader : IService
{
    public Task LoadLevel(int level);
}

