using UnityEngine;

public class Player : MonoBehaviour, ISavedProgress
{
    private int _currentLvl = 0;

    public void UpdateProgree(PlayerProgress progress)
    {
        progress.WorldData.LvlNumber = _currentLvl;
    }

    public void LoadProgres(PlayerProgress progress)
    {
        int toLoadLvl = progress.WorldData.LvlNumber;

        if (_currentLvl != toLoadLvl)
        {
            _currentLvl = toLoadLvl;
        }
    }


}

