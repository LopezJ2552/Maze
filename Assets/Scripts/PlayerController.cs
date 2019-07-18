using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private int count;
    private int lives;
    public GameObject camera;
    public float speed;
    public Text countText;
    public Text livesText;
    public Text winText;
    public float jumpForce;
    public AudioClip winSound;
    public AudioSource winSource;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        camera = GameObject.Find("Main Camera");
        count = 0;
        lives = 3;
        winText.text = "";
        SetAllText ();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");

        Vector2 movement = new Vector2(moveHorizontal, 0);

        rb2d.AddForce(movement * speed);
    }

    void OnTriggerEnter2D (Collider2D other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive (false);
            count = count + 1;
            SetAllText ();
        }

        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.SetActive (false);
            lives = lives - 1;
            SetAllText ();
        }

        if (count == 4)
        {
            transform.position = new Vector2(50.0f, transform.position.y);
            lives = 3;
            camera.transform.position = new Vector3(45.0f, 0.0f, -10.0f);
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.collider.tag == "Ground")
        {
            if(Input.GetKey(KeyCode.UpArrow))
            {
                rb2d.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            }
        }
    }
    
    void SetAllText ()
    {
        countText.text = "Count: " + count.ToString ();
        livesText.text = "Lives: " + lives.ToString ();
        if (count >= 8)
        {
            winText.text = "You win!";
            winSource.clip = winSound;
            winSource.Play();
        }

        if (lives == 0)
        {
            winText.text = "You lose!";
            Destroy(gameObject);
        }
    }
}
