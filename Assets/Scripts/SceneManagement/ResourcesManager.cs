using System.Collections.Generic;
using UnityEngine;

public class ResourcesManager : Singleton<ResourcesManager>
{
    // Diccionario para almacenar los recursos SpawnOnceSO
    // Clave: itemID, Valor: SpawnOnceSO
    Dictionary<string, SpawnOnceSO> _resources = new Dictionary<string, SpawnOnceSO>();

    protected override void Awake()
    {
        base.Awake();

        // Cargamos los recursos al iniciar el juego
        LoadResources();
    }

    void LoadResources()
    {
        // Cargamos todos los recursos de tipo SpawnOnceSO desde la carpeta Resources/SpawnOnce
        SpawnOnceSO[] loadedResources = Resources.LoadAll<SpawnOnceSO>("SpawnOnce");

        foreach (var resource in loadedResources)
        {
            if (!_resources.ContainsKey(resource.itemID))
            {
                // Clonamos el recurso para evitar problemas de referencia
                _resources.Add(resource.itemID, ScriptableObject.Instantiate(resource));
            }
            else
            {
                Debug.LogWarning($"Resource with name {resource.name} already exists. Skipping duplicate.");
            }
        }
    }

    public SpawnOnceSO GetResource(string resourceID)
    {
        if (_resources.TryGetValue(resourceID, out SpawnOnceSO resource))
        {
            return resource;
        }
        else
        {
            Debug.LogError($"Resource {resourceID} not found.");
            return null;
        }
    }

    public List<SpawnOnceSO> GetSceneResources(int sceneID)
    {
        List<SpawnOnceSO> sceneResources = new List<SpawnOnceSO>();
        foreach (var resource in _resources.Values)
        {
            if (resource.sceneID == sceneID)
            {
                sceneResources.Add(resource);
            }
        }
        return sceneResources;
    }
}
