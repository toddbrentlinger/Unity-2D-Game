using UnityEngine;
using System.Collections;

public class Mover : MonoBehaviour {

    public float minSpeedX;
    public float maxSpeedX;
    public float speedY;

    // private Rigidbody2D rb2d;
    private float horizSpeed;

    void Start()
    {
        // rb2d = GetComponent<Rigidbody2D>();
        // rb2d.velocity = new Vector2(horizSpeed, speedY);

        SetHorizontalSpeed();
        SetEnemyDirection();
    }

    void Update()
    {
        // rb.velocity = new Vector2(horizSpeed, speed.y);
        transform.Translate(Vector2.right * horizSpeed * Time.deltaTime);
        transform.Translate(Vector2.down * speedY * Time.deltaTime);
    }

    void FixedUpdate()
    {

    }

    void OnTriggerEnter2D (Collider2D other)
    {
        if (other.gameObject.CompareTag("Boundary"))
            return;

        if (other.gameObject.CompareTag("SideBoundary"))
        {
            // rb2d.velocity = new Vector2(-rb2d.velocity.x, rb2d.velocity.y);
            if ( (transform.position.x > 0 && horizSpeed > 0) ||
                (transform.position.x < 0 && horizSpeed < 0) )
            {
                horizSpeed *= -1;
                SetEnemyDirection();
            } 
        }
    }

    void SetHorizontalSpeed()
    {
        float speedDiff = maxSpeedX - minSpeedX;
        horizSpeed = Random.Range(-speedDiff, speedDiff);
        if (horizSpeed > 0)
            horizSpeed += minSpeedX;
        else
            horizSpeed -= minSpeedX;
    }

    void SetEnemyDirection()
    {
        if (horizSpeed > 0)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }
}
