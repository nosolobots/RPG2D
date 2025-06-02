using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : Singleton<SceneManagement>
{
    // Nombre del portal de destino al que se debe teletransportar el jugador
    public string destPortalName { get; private set; }
    public void SetDestPortalName(string name) => destPortalName = name;

    protected override void Awake()
    {
        base.Awake();

        // Nos suscribimos al evento de carga de escena
        // Esto nos permite instanciar los recursos de la escena actual
        // cuando se carga una nueva escena
        // Esto es útil para asegurarnos de que los recursos se instancien
        // después de que la escena haya sido completamente cargada
        // y los objetos de la escena estén disponibles.           
        SceneManager.sceneLoaded += OnSceneLoaded; 
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Instanciamos los recursos de la escena actual
        SpawnSceneResources(scene.buildIndex);
    }

    void SpawnSceneResources(int sceneID)
    {
        // Obtenemos los recursos de la escena actual
        var resources = ResourcesManager.Instance.GetSceneResources(sceneID);
        
        // Instanciamos los recursos
        foreach (var resource in resources)
        {
            if (resource.itemPrefab != null)
            {
                if (resource.state == SpawnOnceSO.SpawnOnceState.ToSpawn)
                {
                    GameObject gameObject = Instantiate(resource.itemPrefab, resource.spawnPosition, Quaternion.identity);
                    // Almacenamos el ID del item en el nombre del GameObject
                    gameObject.name = resource.itemID; 
                }
            }
            else
            {
                Debug.LogWarning($"Prefab for resource {resource.name} is not assigned.");
            }
        }
    }
}
