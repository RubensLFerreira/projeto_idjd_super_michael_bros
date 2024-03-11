using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{
    public bool alive = true;
    void Start()
    {
        GameController.gc.RefreshScreen();
    }

    // Update is called once per frame
    void Update()
    {
        GameController.gc.RefreshScreen();
    }
    public void LoseLife()
    {
        if (alive)
        {
            alive = false;
            gameObject.GetComponent<Animator>().SetTrigger("Dead");
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
            gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
            gameObject.GetComponent<Player>().enabled = false;
            gameObject.GetComponent<Animator>().SetBool("Andar",false);
            GameController.gc.SetLives(-1);
            

            if(GameController.gc.porcoes >= 0)
            {
                Invoke("LoadScene",0.5f);
            }
            else
            {
                Invoke("LoadGameOver", 0.5f);
                //Debug.Log("Game Over!");
                GameController.gc.porcoes = 2;
            }
        }
    }

    void LoadGameOver()
    {
        SceneManager.LoadScene("GameOver");
    }
    void LoadScene()
    {
        SceneManager.LoadScene("Fase1");
    }
}
