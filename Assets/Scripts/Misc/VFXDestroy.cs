using UnityEngine;

public class VFXDestroy : MonoBehaviour
{
    ParticleSystem ps;

    void Awake()
    {
        ps = GetComponent<ParticleSystem>();
    }

    void Update()
    {
        if(ps && !ps.IsAlive())
        {
            Destroy(gameObject);
        }        
    }
}
