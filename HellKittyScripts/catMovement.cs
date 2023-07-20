using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class catMovement : MonoBehaviour
{
    public Animator animator;
    public Sprite NotOnHuman;
    public KeyCode moveRightKey = KeyCode.L;
    public KeyCode moveLeftKey = KeyCode.J;
    public KeyCode jumpKey = KeyCode.I;
    public KeyCode stopJump = KeyCode.DownArrow;
    public float speed = 0.1f;
    float direction = 0.0f;
    float directiony = 0.0f;
    public bool isGrounded;
    public bool playerpos;
    bool facingRight = true;
    private float startXpos;
    private float startYpos;
    public GameObject Tries;
    public GameObject breath;


    // Start is called before the first frame update
    void Start()
    {
        Vector3 position = transform.localPosition;
        startXpos = position.x;
        startYpos = position.y;
    }

    // Update is called once per frame
    void Update()
    {
        bool isLeftPressed = Input.GetKey(moveLeftKey);
        bool isRightPressed = Input.GetKey(moveRightKey);
        bool isJumpPressed = Input.GetKey(jumpKey);
        bool isStopJumpPressed = Input.GetKey(stopJump);

        Vector3 position = transform.localPosition;
        position.x += speed * direction;
        position.y += speed * directiony;
        transform.localPosition = position;



        if (isLeftPressed)
        {
            direction = -0.5f;
            animator.SetFloat("Speed", Mathf.Abs(direction));
            if (direction<0 && facingRight)
            {
                flip();
            }
            else if (direction>0 && !facingRight)
            {
                flip();
            }

        }
        else if (isRightPressed)
        {
            direction = 0.5f;
            animator.SetFloat("Speed", Mathf.Abs(direction));
            if (direction>0 && !facingRight)
            {
                flip();
            }
            else if (direction<0 && facingRight)
            {
                flip();
            }

        }
        else
        {
            direction = 0.0f;
            animator.SetFloat("Speed", Mathf.Abs(direction));
        }
        if (isJumpPressed && isGrounded == true)
        {
            animator.SetBool("IsJumping", true);
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, 7f), ForceMode2D.Impulse);
            FindObjectOfType<AudioMAnager>().Play("CatJump");
            isGrounded = false;
        }
        if (position.y < -13)
        {
            FindObjectOfType<AudioMAnager>().Play("CatDeath");
            Invoke("restartLevel", 3);
        }

        if (isGrounded == false && isStopJumpPressed)
        {

            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, -1.5f), ForceMode2D.Impulse); ;

        }
    }

    void flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }

    

    public void movePlayerToStart()
    {
        Vector3 backPosition = transform.localPosition;
        backPosition.x = startXpos;
        backPosition.y = startYpos;
        transform.localPosition = backPosition;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            isGrounded = true;
            animator.SetBool("IsJumping", false);
            animator.SetBool("IsDead", true);
            FindObjectOfType<AudioMAnager>().Play("CatDeath");
            Invoke("restartLevel", 3);
        }
        if (collision.collider.tag == "CatGround")
        {
            isGrounded = true;
            animator.SetBool("IsJumping", false);
        }
        if (collision.collider.tag == "GateOpen")
        {
            isGrounded = true;
            animator.SetBool("IsJumping", false);
        }
        if (collision.collider.tag == "Zoomie")
        {
            animator.SetBool("IsGrey", false);
            animator.SetBool("IsRed", true);
           
        }
        if (collision.collider.tag == "Player")
        {
            isGrounded = true;
            playerpos = true;
            animator.SetBool("IsJumping", false);
        }
        if (collision.collider.tag == "Breath")
        {
            breath.SetActive(true);
        }
        
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            //isGrounded = false;
            playerpos = false;
        }
       
    }

    public void IncreaseSpeed()
    {
        speed = speed * 3;
    }

    public void restartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Tries.GetComponent<triesScript>().incrementTries();
    }
}
