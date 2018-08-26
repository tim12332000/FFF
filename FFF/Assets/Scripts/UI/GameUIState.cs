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
    public GameObject GameObjectButtonRestart = null;
    public Text TextGameOver = null;
    public Image ImageButtonRestart = null;
    public Text TextButtonRestart = null;
    //===========================================================
    public GameObject WinGroup = null;
    public GameObject GameObjectButtonWinRestart = null;
    public Image ImageClear = null;
    public Text TextWin = null;
    public Image ImageButtonWinRestart = null;
    public Text TextButtonWinRestart = null;
    //===========================================================
    private AudioSource AudioSourceBGM = null;
    public AudioClip[] AudioClipsBGM;

    void Start()
    {
        GetAudioSourceBGM();

        GlobelEvents.Instance.Initialize.AddListener(Initialize);
        GlobelEvents.Instance.GameStart.AddListener(GameStart);
        GlobelEvents.Instance.BackToTitle.AddListener(BackToTitle);
        GlobelEvents.Instance.GameOver.AddListener(GameOver);
        GlobelEvents.Instance.GameWin.AddListener(GameWin);

        EventTriggerListener.Get(GameObjectButtonStart).onClick += ClickStart;
        EventTriggerListener.Get(GameObjectButtonRestart).onClick += ClickRestart;
        EventTriggerListener.Get(GameObjectButtonWinRestart).onClick += ClickRestart;

        GlobelEvents.Instance.Initialize.Invoke();
    }

    public void GetAudioSourceBGM()
    {
        GameObject gamesystem = GameObject.Find("GameSystem");
        if (gamesystem != null)
        {
            AudioSourceBGM = gamesystem.GetComponent<AudioSource>();
        }
    }

    public void ClickStart(GameObject click)
    {
        GlobelEvents.Instance.GameStart.Invoke();
    }

    public void ClickRestart(GameObject click)
    {
		GlobelEvents.Instance.GameReStart.Invoke();
		GlobelEvents.Instance.BackToTitle.Invoke();
    }

    public void Initialize()
    {
        GameMain.Initialize();
        GameGroup.SetActive(false);
        StartGroup.SetActive(true);
        EndGroup.SetActive(false);
        WinGroup.SetActive(false);

        PlayBGM(0);
    }

    public void GameStart()
    {
        GameUIValue.Instance.ResetGameValue();

        GameMain.GameStart();
        GameGroup.SetActive(true);
        EndGroup.SetActive(false);
        WinGroup.SetActive(false);

        ImageTitle.DOFade(0, 0.5f).OnComplete(() =>
        {
            StartGroup.SetActive(false);
            PlayBGM(1);
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
        TextButtonStart.color = Color.black;
        TextTitle.color = Color.black;

        EndGroup.SetActive(false);
        WinGroup.SetActive(false);

        PlayBGM(0);
    }

    public void GameOver()
    {
        GameMain.GameOver();
        GameGroup.SetActive(false);
        StartGroup.SetActive(false);
        EndGroup.SetActive(true);
        WinGroup.SetActive(false);

        TextGameOver.color = Color.clear;
        TextGameOver.DOFade(1, 0.5f);
        TextButtonRestart.color = Color.clear;
        TextButtonRestart.DOFade(1, 0.5f);
        ImageButtonRestart.color = new Color(1, 1, 1, 0);
        ImageButtonRestart.DOFade(1, 0.5f);

        PlayBGM(2);
    }

    public void GameWin()
    {
        GameMain.GameOver();
        GameGroup.SetActive(false);
        StartGroup.SetActive(false);
        EndGroup.SetActive(false);
        WinGroup.SetActive(true);

        TextWin.color = Color.clear;
        TextWin.DOFade(1, 0.5f);
        TextButtonWinRestart.color = Color.clear;
        TextButtonWinRestart.DOFade(1, 0.5f);
        ImageButtonWinRestart.color = new Color(1, 1, 1, 0);
        ImageButtonWinRestart.DOFade(1, 0.5f);
        ImageClear.color = new Color(1, 1, 1, 0);
        ImageClear.DOFade(1, 0.5f);

        PlayBGM(3);
    }

    public void PlayBGM(int index)
    {
        if (AudioSourceBGM == null)
        {
            return;
        }

        AudioSourceBGM.clip = AudioClipsBGM[index];
        AudioSourceBGM.Play();
    }
}