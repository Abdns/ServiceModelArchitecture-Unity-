using UnityEngine;

public class SaveLoadService : ISaveLoadService
{
    private const string ProgressKey = "Progress";

    private readonly IGameFactory _gameFactory;
    private readonly IPersistentProgressService _progressService;

    public SaveLoadService(IPersistentProgressService progressService,  IGameFactory factory)
    {
        _progressService = progressService;
        _gameFactory = factory;
    }

    public void SaveProgress()
    {
        foreach (ISavedProgress progressWriter in _gameFactory.ProgressWriters)
        {
            progressWriter.UpdateProgree(_progressService.Progress);
        }

        PlayerPrefs.SetString(ProgressKey, _progressService.Progress.ToJson());
        
    }
    public PlayerProgress LoadProgress() => PlayerPrefs.GetString(ProgressKey)?.ToDeserialized<PlayerProgress>();
     
}

