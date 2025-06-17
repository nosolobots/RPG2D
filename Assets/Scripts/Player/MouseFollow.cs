using UnityEngine;

public class MouseFollow : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        FaceMouse();
    }

    void FaceMouse()
    {
        Vector3 mousePosition = Input.mousePosition;
        
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);


        Vector2 direction = mousePosition - transform.position;

        
        transform.right = direction.normalized; // Rotate the object to face the mouse
    }
}


