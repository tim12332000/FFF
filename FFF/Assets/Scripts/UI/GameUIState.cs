using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUIState : MonoBehaviour
{
    public GameObject GameGroup = null;
    //===========================================================
    public GameObject StartGroup = null;
    public GameObject GameObjectButtonStart = null;
    //===========================================================
    public GameObject EndGroup = null;

    void Start()
    {
        GlobelEvents.Instance.Initialize.AddListener(Initialize);
        GlobelEvents.Instance.GameStart.AddListener(GameStart);
        GlobelEvents.Instance.BackToTitle.AddListener(BackToTitle);
        GlobelEvents.Instance.GameOver.AddListener(GameOver);

        EventTriggerListener.Get(GameObjectButtonStart).onClick += ClickStart;

		GlobelEvents.Instance.Initialize.Invoke();
    }

    public void ClickStart(GameObject click)
    {
		GlobelEvents.Instance.GameStart.Invoke();
    }

    public void Initialize()
    {
        GameMain.Initialize();
        GameGroup.SetActive(false);
        StartGroup.SetActive(true);
        EndGroup.SetActive(false);
    }

    public void GameStart()
    {
        GameMain.GameStart();
        GameGroup.SetActive(true);
        StartGroup.SetActive(false);
        EndGroup.SetActive(false);
    }

    public void BackToTitle()
    {
        GameMain.BackToTitle();
        GameGroup.SetActive(false);
        StartGroup.SetActive(true);
        EndGroup.SetActive(false);
    }

    public void GameOver()
    {
        GameMain.GameOver();
        GameGroup.SetActive(false);
        StartGroup.SetActive(false);
        EndGroup.SetActive(true);
    }
}