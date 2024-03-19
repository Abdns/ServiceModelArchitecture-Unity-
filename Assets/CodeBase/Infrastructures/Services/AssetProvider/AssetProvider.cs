using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;

public class AssetProvider: IAsssetProvider
{
    private readonly Dictionary<string, AsyncOperationHandle> _completeCash = new Dictionary<string, AsyncOperationHandle>();
    private readonly Dictionary<string, List<AsyncOperationHandle>> _handles = new Dictionary<string, List<AsyncOperationHandle>>();

    public void Initialize()
    {
        Addressables.InitializeAsync();
    }
    public async Task<T> Load<T>(string adress) where T : class
    {
        if(_completeCash.TryGetValue(adress, out AsyncOperationHandle completeHandle))
        {
            return completeHandle.Result as T;
        }

        AsyncOperationHandle<T> handle = Addressables.LoadAssetAsync<T>(adress);

        handle.Completed += h =>
        {
            _completeCash[adress] = h;
        };

        AddHandle(adress, handle);

        return await handle.Task;
    }

    public async Task<int> GetObjectsCountByLabel(string label)
    {
        IList<IResourceLocation> locations = await Addressables.LoadResourceLocationsAsync(label).Task;
        Addressables.Release(locations);

        return locations.Count;
    }
   
    public void CleanUp()
    {
        foreach (List<AsyncOperationHandle> resourceHandles in _handles.Values)
        {
            foreach (AsyncOperationHandle handle in resourceHandles)
            {       
                Addressables.Release(handle);
            }
        }
    }
    private void AddHandle<T>(string key, AsyncOperationHandle<T> handle) where T : class
    {
        if (!_handles.TryGetValue(key, out List<AsyncOperationHandle> resourceHandles))
        {
            resourceHandles = new List<AsyncOperationHandle>();
            _handles[key] = resourceHandles;
        }

        resourceHandles.Add(handle);
    }
}

