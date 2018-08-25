using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUIValue : SingletonMono<GameUIValue>
{
    private int life = 3;
    public int Life
    {
        get
        {
            return life;
        }
        set
        {
            life = value;

            for (int i = 0; i < ImagesLife.Length; i++)
            {
                ImagesLife[i].color = Color.clear;
            }
            for (int i = 0; i < life; i++)
            {
                ImagesLife[i].color = Color.white;
            }
        }
    }
    public Image[] ImagesLife;
    //============================================================
    private int time = 0;
    public int Time
    {
        get
        {
            return time;
        }
        set
        {
            time = value;
            TextTime.text = time.ToString();
        }
    }
    public Text TextTime = null;
    //============================================================
    private int far = 0;
    public int Far
    {
        get
        {
            return far;
        }
        set
        {
            far = value;
            TextFar.text = far.ToString();
        }
    }
    public Text TextFar = null;
    //============================================================

    void Awake()
    {
        Life = 3;
        Time = 0;
        Far = 0;
    }

    void Start()
    {

    }
}