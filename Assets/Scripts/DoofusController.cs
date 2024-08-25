using UnityEngine;

public class DoofusController : MonoBehaviour
{
    public float speed = 5.0f; // Speed of the character, can be set from JSON

    void Update()
    {
        // Get input from arrow keys or WASD
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Create a Vector3 for movement direction
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        // Move the character
        transform.Translate(movement * speed * Time.deltaTime, Space.World);
    }
}