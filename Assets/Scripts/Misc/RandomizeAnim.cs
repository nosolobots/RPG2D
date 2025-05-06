using UnityEngine;

public class RandomizeAnim : MonoBehaviour
{
    Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Start()
    {
        // Obtiene el AnimatorStateInfo de la capa 0 (primera capa)
        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);    

        // Reproduce la animación actual con un tiempo aleatorio
        anim.Play(stateInfo.fullPathHash, -1, Random.Range(0f, 1f));
    }
}
