using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUIValue : SingletonMono<GameUIValue>
{
    private int life = 5;
    public int Life
    {
        get
        {
            return life;
        }
        set
        {
            if (value < 0 || value > 5)
            {
                return;
            }

            life = value;
            for (int i = 0; i < ImagesLife.Length; i++)
            {
                ImagesLife[i].sprite = SpritesLife[0];
            }
            for (int i = 0; i < life; i++)
            {
                ImagesLife[i].sprite = SpritesLife[1];
            }
        }
    }
    public Image[] ImagesLife;
    public Sprite[] SpritesLife;
    //============================================================
    private float speed = 0;
    public float Speed
    {
        get
        {
            return speed;
        }
        set
        {
            speed = value;
            TextSpeed.text = speed.ToString();
        }
    }
    public Text TextSpeed = null;
    //============================================================
    private float far = 0;
    public float Far
    {
        get
        {
            return far;
        }
        set
        {
            far = value;
            TextFar.text = far.ToString()+"/M";
        }
    }
    public Text TextFar = null;
    //============================================================
    public RectTransform RectTransformGirl = null;

    void Awake()
    {

    }

    void Start()
    {

    }
    
    void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.U))
        {
            Life--;
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            Speed += 100;
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            Far += 100;
        }
        */
    }

    public void ResetGameValue()
    {
        Life = 5;
        Speed = 0;
        Far = 0;
    }

    public void SetProgress(float now, float total)
    {
        float progressTotal = 440;
        float percent = now / total;

        if (percent > 1)
        {
            percent = 1;
        }

        float nowPoint = progressTotal * percent;
        RectTransformGirl.anchoredPosition = Vector2.right * (-540 + nowPoint) + Vector2.up * 75;
    }
}