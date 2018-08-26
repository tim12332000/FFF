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
    public GameObject ClearGroup = null;
    public GameObject GameObjectButtonClearRestart = null;
    public Text TextClear = null;
    public Image ImageButtonClearRestart = null;
    public Text TextButtonClearRestart = null;
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

        EventTriggerListener.Get(GameObjectButtonStart).onClick += ClickStart;
        EventTriggerListener.Get(GameObjectButtonRestart).onClick += ClickRestart;
        EventTriggerListener.Get(GameObjectButtonClearRestart).onClick += ClickRestart;

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
        GlobelEvents.Instance.BackToTitle.Invoke();
    }

    public void Initialize()
    {
        GameMain.Initialize();
        GameGroup.SetActive(false);
        StartGroup.SetActive(true);
        EndGroup.SetActive(false);
        ClearGroup.SetActive(false);

        PlayBGM(0);
    }

    public void GameStart()
    {
        GameUIValue.Instance.ResetGameValue();

        GameMain.GameStart();
        GameGroup.SetActive(true);
        EndGroup.SetActive(false);
        ClearGroup.SetActive(false);

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
        TextButtonStart.color = Color.white;
        TextTitle.color = Color.white;

        EndGroup.SetActive(false);
        ClearGroup.SetActive(false);

        PlayBGM(0);
    }

    public void GameOver()
    {
        GameMain.GameOver();
        GameGroup.SetActive(false);
        StartGroup.SetActive(false);
        EndGroup.SetActive(true);
        ClearGroup.SetActive(false);

        TextGameOver.color = Color.clear;
        TextGameOver.DOFade(1, 0.5f);
        TextButtonRestart.color = Color.clear;
        TextButtonRestart.DOFade(1, 0.5f);
        ImageButtonRestart.color = new Color(1, 1, 1, 0);
        ImageButtonRestart.DOFade(1, 0.5f);

        PlayBGM(2);
    }

    public void Clear()
    {
        GameMain.GameOver();
        GameGroup.SetActive(false);
        StartGroup.SetActive(false);
        EndGroup.SetActive(false);
        ClearGroup.SetActive(true);

        TextClear.color = Color.clear;
        TextClear.DOFade(1, 0.5f);
        TextButtonClearRestart.color = Color.clear;
        TextButtonClearRestart.DOFade(1, 0.5f);
        ImageButtonClearRestart.color = new Color(1, 1, 1, 0);
        ImageButtonClearRestart.DOFade(1, 0.5f);

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