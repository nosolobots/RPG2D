using UnityEngine;

public class SceneManagement : Singleton<SceneManagement>
{
    public string destPortalName { get; private set; }
    public void SetDestPortalName(string name) => destPortalName = name;
}
