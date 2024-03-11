using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Animator playerAnim;
    private Rigidbody2D rbPlayer;
    public float speed;
    private SpriteRenderer sr;
    public float jumpForce;
    public bool inFloor = true;
    public bool doubleJump;
    
    private GameController gcPlayer;

    public AudioSource audioS;
    public AudioClip[] SoundS;

    void Start()
    {
        playerAnim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        rbPlayer = GetComponent<Rigidbody2D>();
        gcPlayer = Object.FindFirstObjectByType<GameController>();
        if(gcPlayer == null)
        {
            Debug.LogError("GameController não encontrado!");
        }
        else
        {
            gcPlayer.coins = 0;
            //gcPlayer.porcoes = 0;
        }
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    void Update()
    {
        Jump();
    }

    void MovePlayer()
    {
        float horizontalMovement = Input.GetAxisRaw("Horizontal");
        rbPlayer.velocity = new Vector2(horizontalMovement * speed, rbPlayer.velocity.y);
        
        if (horizontalMovement > 0)
        {
            playerAnim.SetBool("Andar", true);
            sr.flipX = true;
        }
        else if (horizontalMovement < 0)
        {
            playerAnim.SetBool("Andar", true);
            sr.flipX = false;
        }
        else
        {
            playerAnim.SetBool("Andar", false);
        }
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (inFloor)
            {
                rbPlayer.velocity = Vector2.zero;
                rbPlayer.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
                inFloor = false;
                doubleJump = true;
            }
            else if (!inFloor && doubleJump)
            {
                rbPlayer.velocity = Vector2.zero;
                rbPlayer.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
                inFloor = false;
                doubleJump = false;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Ground")
        {
            inFloor = true;
            doubleJump = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Coins")
        {
            Destroy(collision.gameObject);
            if(gcPlayer != null)
            {
                audioS.clip = SoundS[0];
                audioS.Play();
                //gcPlayer.coins++;
                gcPlayer.SetCoins(1);
                GameController.gc.RefreshScreen();
                //gcPlayer.textoMoeda.text = gcPlayer.coins.ToString();
            }
        }

        if(collision.gameObject.tag == "Enemy")
            {
                audioS.clip = SoundS[1];
                audioS.Play();
                rbPlayer.velocity = Vector2.zero;
                rbPlayer.AddForce(Vector2.up*5, ForceMode2D.Impulse);
                collision.gameObject.GetComponent<SpriteRenderer>().flipY = true;
                collision.gameObject.GetComponent<Enemy>().enabled = false;
                collision.gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
                collision.gameObject.GetComponent<BoxCollider2D>().enabled = false;
                collision.gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
                Destroy(collision.gameObject, 1f);
            }

        //if (collision.gameObject.tag == "Porcoes")
        //{
        //    Destroy(collision.gameObject);
        //    if(gcPlayer != null)
        //    {
        //        gcPlayer.porcoes++;
        //        gcPlayer.textoPorcao.text = gcPlayer.porcoes.ToString();
        //    }
            
        //}
    }
}



