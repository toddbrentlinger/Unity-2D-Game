using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public float verticalForce;
    public float horizontalForce;

    private Rigidbody2D rb2d;
    private Animator anim;
    private float groundHeightY;
    private bool isDead;
    private GameControllerV3 gameController;

    void Start()
    {
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameControllerV3>();
        }
        if (gameController == null)
        {
            Debug.Log("Can't find 'GameController' script");
        }

        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        groundHeightY = transform.position.y;
        isDead = false;

    }

    void Update()
    {
        if (!isDead)
        {
            if (Input.GetKeyDown("w") || Input.GetKeyDown("up"))
            {
                Flap();
                ResetVelocity();
                rb2d.AddForce(new Vector2(0, verticalForce), ForceMode2D.Impulse);
            }
            else if (Input.GetKeyDown("a") || Input.GetKeyDown("left"))
            {
                transform.localScale = new Vector3(-3f, 3f, 1f);
                Flap();
                ResetVelocity();
                rb2d.AddForce(new Vector2(-horizontalForce, verticalForce), ForceMode2D.Impulse);
            }
            if (Input.GetKeyDown("s") || Input.GetKeyDown("down"))
            {
                ResetVelocity();
                rb2d.AddForce(new Vector2(0, -verticalForce), ForceMode2D.Impulse);
            }
            else if (Input.GetKeyDown("d") || Input.GetKeyDown("right"))
            {
                transform.localScale = new Vector3(3f, 3f, 1f);
                Flap();
                ResetVelocity();
                rb2d.AddForce(new Vector2(horizontalForce, verticalForce), ForceMode2D.Impulse);
            }
        }

        GroundCheck();
    }

    void ResetVelocity()
    {
        rb2d.velocity = new Vector2(0, 0);
    }

    void Flap()
    {
        anim.SetTrigger("Flap");
    }

    void GroundCheck()
    {
        if (transform.position.y < groundHeightY)
            transform.position = new Vector3 (transform.position.x, groundHeightY, transform.position.z);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Boundary")
        {
            return;
        }
        if (other.tag == "Enemy" && !isDead)
        {
            isDead = true;
            anim.SetBool("Dead", true);
            transform.localScale = new Vector3(transform.localScale.x, -transform.localScale.y, transform.localScale.z);
            ResetVelocity();
            rb2d.AddForce(new Vector2(0, verticalForce) * 0.7f, ForceMode2D.Impulse);
            gameController.GameOver();
        }
    }

    public bool GetIsDead()
    {
        return isDead;
    }
}
