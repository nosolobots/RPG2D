using UnityEngine;
using UnityEngine.SceneManagement;
public class PortalExit : MonoBehaviour
{
    [SerializeField] string sceneToLoad;
    [SerializeField] string destPortalName;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>())
        {
            // Load the next scene
            SceneManager.LoadScene(sceneToLoad);           

            // Set the destination portal name in SceneManagement
            SceneManagement.Instance.SetDestPortalName(destPortalName); 
        }
    }
}
