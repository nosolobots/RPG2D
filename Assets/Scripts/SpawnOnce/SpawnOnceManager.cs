using UnityEngine;

public class SpawnOnceManager: Singleton<SpawnOnceManager>
{
    protected override void Awake()
    {
        base.Awake();
    }

    void Start()
    {
        // Ensure that PlayerPrefs are cleared on start
        PlayerPrefs.DeleteAll();
    }

    public void MarkAsDestroyed(string id)
    {
        PlayerPrefs.SetInt(id, 1);
        PlayerPrefs.Save();
    }
    
    public bool IsDestroyed(string id)
    {
        return PlayerPrefs.GetInt(id, 0) == 1;
    }
}
