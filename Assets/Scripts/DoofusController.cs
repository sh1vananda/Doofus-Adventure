using UnityEngine;

public class DoofusController : MonoBehaviour
{
    public float speed = 5f;
    private bool gameStarted = false;

    void Update()
    {
        if (!gameStarted) return;

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        transform.Translate(movement * speed * Time.deltaTime, Space.World);
    }

    public void StartGame()
    {
        gameStarted = true;
    }
}