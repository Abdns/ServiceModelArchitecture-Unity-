using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StaticDataService : IStaticDataService
{
    private Dictionary<Type, StaticData> _staticData;

    public void LoadStaticData()
    {
       _staticData = Resources.LoadAll<StaticData>("StaticData").ToDictionary(x => x.GetType(), x => x);
    }

    public T GetStaticData<T>() where T : StaticData
    {
        if (_staticData.TryGetValue(typeof(T), out StaticData data))
            return (T)data;

        return null;
    }
 
}
