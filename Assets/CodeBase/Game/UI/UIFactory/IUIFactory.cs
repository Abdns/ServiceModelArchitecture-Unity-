using System.Threading.Tasks;

public interface IUIFactory : IService
{
    Task CreateUIRoot();
    Task CreateLevelsWindow();
}

