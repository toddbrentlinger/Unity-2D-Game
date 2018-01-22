using UnityEngine;
using System.Collections;

public class DestroyByContact : MonoBehaviour {

    private GameControllerV2 gameController;

    void Start()
    {
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameControllerV2>();
        }
        if (gameController == null)
        {
            Debug.Log("Can't find 'GameController' script");
        }
    }

    void OnTriggerEnter2D (Collider2D other)
    {
        if (other.tag == "Boundary")
        {
            return;
        }
        if (other.tag == "Enemy")
        {
            Destroy(gameObject);
            gameController.GameOver();
        }
    }
}
