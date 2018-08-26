using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Gameplay.Utility;

public class GameUIState : MonoBehaviour
{
    public GameObject GameGroup = null;
    //===========================================================
    public GameObject StartGroup = null;
    public GameObject GameObjectButtonStart = null;
    public Image ImageTitle = null;
    public Image ImageButtonStart = null;
    public Text TextButtonStart = null;
    public Text TextTitle = null;
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
        EndGroup.SetActive(false);

        ImageTitle.DOFade(0, 0.5f).OnComplete(() =>
        {
            StartGroup.SetActive(false);
        });
        ImageButtonStart.DOFade(0, 0.5f);
        TextButtonStart.DOFade(0, 0.5f);
        TextTitle.DOFade(0, 0.5f);

		ScheduleHelper.Instance.DelayDo(StageManager.Instance.Go, 1f);
    }

    public void BackToTitle()
    {
        GameMain.BackToTitle();
        GameGroup.SetActive(false);

        StartGroup.SetActive(true);
        ImageTitle.color = Color.white;
        ImageButtonStart.color = Color.white;
        TextButtonStart.color = Color.white;
        TextTitle.color = Color.white;

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