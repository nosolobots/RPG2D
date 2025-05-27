using System.Collections;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
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
            // Disable player controls
            InputManager.Instance.Controls.Player.Disable();

            // Hide player sprite
            PlayerController.Instance.gameObject.SetActive(false);

            // Load the next scene
            StartCoroutine(LoadScene());

            // Set the destination portal name in SceneManagement
            SceneManagement.Instance.SetDestPortalName(destPortalName); 

            // Fade to black
            UIFade.Instance.FadeToBlack();
        }

        IEnumerator LoadScene()
        {
            // Wait for the fade to complete
            yield return new WaitForSeconds(UIFade.Instance.FadeDuration);
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
