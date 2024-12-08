using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public float playerSpeed = 2f;
    public float horizontalSpeed = 3f;
    public float rightLimit = 5.5f;
    public float leftLimit = -5.5f;

    private bool isPlayerAlive = true;

    void Update()
    {
        if (!isPlayerAlive) return;  // Stop all movement when player is "dead"

        // Move the player forward constantly
        transform.Translate(Vector3.forward * Time.deltaTime * playerSpeed, Space.World);

        // Move left when A or Left Arrow is pressed, within left limit
        if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) && transform.position.x > leftLimit)
        {
            transform.Translate(Vector3.left * Time.deltaTime * horizontalSpeed);
        }

        // Move right when D or Right Arrow is pressed, within right limit
        if ((Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) && transform.position.x < rightLimit)
        {
            transform.Translate(Vector3.right * Time.deltaTime * horizontalSpeed);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            Debug.Log("Player collided with obstacle. Stopping movement and restarting game...");
            StopMovement();

            // Call RestartGame after a delay
            Invoke("RestartGame",0.1f);
        }
    }

    void StopMovement()
    {
        isPlayerAlive = false;   // This stops movement in Update
        GetComponent<Renderer>().enabled = false;   // Hide player model
        GetComponent<Collider>().enabled = false;   // Disable player collider
    }


    void RestartGame()
    {
        Debug.Log("Restarting game...");

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}