using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController gc;
    public Text textoMoeda;
    public Text textoPorcao;
    public int coins;
    public int porcoes = 2;

    void Awake()
    {
        if (gc == null)
        {
            gc = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (gc != this)
        {
            Destroy(gameObject);
        }
        RefreshScreen();
    }

    public void SetLives(int porcao)
    {
        porcoes += porcao;
        if(porcoes >= 0)
        {
            RefreshScreen();
        }
    }

    public void SetCoins(int coin)
    {
        coins += coin;
        if(coins >= 10)
        {
            coins = 0;
            porcoes += 1;
        }
        RefreshScreen();
    }
        
    public void RefreshScreen()
    {
        textoMoeda.text = coins.ToString();
        textoPorcao.text = porcoes.ToString();
    }

    void Start()
    {
        //textoMoeda.text = coins.ToString();
        //textoPorcao.text = porcoes.ToString();
    }

    void Update()
    {

    }
}




