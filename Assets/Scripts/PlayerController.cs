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
    private bool facingRight = true;
     Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator> ();
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

        if (facingRight == false && moveHorizontal > 0)
        {
            Flip();
        }
        else if (facingRight == true && moveHorizontal < 0)
        {
            Flip();
        }

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
            
            if (Input.GetKey (KeyCode.RightArrow))
            {
                anim.SetInteger("State", 1);
            }

            if (Input.GetKey (KeyCode.LeftArrow))
            {
                anim.SetInteger("State", 1);
            }

            if (Input.GetKey (KeyCode.UpArrow))
            {
                anim.SetInteger("State", 2);
            }
        }
        else
        {
             anim.SetInteger("State", 2);
        }
    }
    
    void LateUpdate ()
    {
        if (!Input.anyKey)
        {
            anim.SetInteger("State", 0);
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

    void Flip()
{
        facingRight = !facingRight;
        Vector2 Scaler = transform.localScale;
        Scaler.x = Scaler.x * -1;
        transform.localScale = Scaler;
    }
}
