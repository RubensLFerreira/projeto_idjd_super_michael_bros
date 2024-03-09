using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    public float speed;
    public bool ground = true;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public bool facingRight = true;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
        ground = Physics2D.Linecast(transform.position, groundCheck.position, groundLayer); 
        // Debug.Log(ground);

        if (!ground){
            speed *= -1;
            Flip();
        }
        
        if (speed > 0 && !facingRight)
        {
            Flip();
        }
        else if (speed < 0 && facingRight)
        {
            Flip();
        }
    }
    
    void Flip()
    {
        facingRight = !facingRight;
        Vector3 Scale = transform.localScale;
        Scale.x *= -1;
        transform.localScale = Scale;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Animator>().SetTrigger("Dead");
            collision.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            collision.gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
            collision.gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
            collision.gameObject.GetComponent<Player>().enabled = false;
            collision.gameObject.GetComponent<Animator>().SetBool("Andar",false);
            Invoke("LoadScene",0.5f);
        }
    }

    void LoadScene()
    {
        SceneManager.LoadScene("Fase1");
    }
}


