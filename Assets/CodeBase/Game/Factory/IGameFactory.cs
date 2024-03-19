using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public interface IGameFactory: IService
{
    List<ISavedProgressReader> ProgressReaders { get; }
    List<ISavedProgress> ProgressWriters { get; }
    Task CreateHero(Vector3 position);
    Task CreateHud();
    Task<GameObject> CreateLevel(int lvlNumber);
    void Cleanup();
}

