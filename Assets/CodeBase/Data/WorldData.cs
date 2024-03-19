using System;

[Serializable]
public class WorldData
{
    public int LvlNumber;

    public WorldData(int initialLvl)
    {
        LvlNumber = initialLvl;
    }
}

