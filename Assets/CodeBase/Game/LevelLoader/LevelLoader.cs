using System.Threading.Tasks;
using UnityEngine;

public class LevelLoader : ILevelLoader
{
    private readonly IGameFactory _gameFactory;
    private GameObject _currentLevel;
    public LevelLoader(IGameFactory gameFactory)
    {
        _gameFactory = gameFactory;
    }

    public async Task LoadLevel(int level)
    {
        if (_currentLevel != null)
        {
            Object.Destroy(_currentLevel);
        }
        _currentLevel = await _gameFactory.CreateLevel(level);       
    }


}
