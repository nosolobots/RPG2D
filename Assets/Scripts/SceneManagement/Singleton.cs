using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    // This is a generic singleton class that can be used for any MonoBehaviour-derived class.
    // It ensures that only one instance of the class exists in the scene at any time.
    // If an instance already exists, it destroys the new instance.
    // It also provides a static Instance property to access the singleton instance.
    // The instance is created if it doesn't exist when accessed for the first time.
    // The instance is not destroyed when loading a new scene, allowing it to persist across scenes.

    private static T _instance;
    public static T Instance => _instance;
    
    protected virtual void Awake()
    {
        if (_instance != null && this.gameObject != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = (T) this;
        }
        
        if (!gameObject.transform.parent)
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}