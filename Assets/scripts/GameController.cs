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
    public int porcoes;

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
    }

    void Start()
    {
        textoMoeda.text = coins.ToString();
        textoPorcao.text = porcoes.ToString();
    }

    void Update()
    {

    }
}



