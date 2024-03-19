using System;

[Serializable]
public class PlayerProgress
{
    public WorldData WorldData;

    public PlayerProgress(int initialLvl)
    {
        WorldData = new WorldData(initialLvl);
    }
}

