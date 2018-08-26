using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GlobelEvents : SingletonMono<GlobelEvents>
{
    public UnityEvent Initialize;

    public UnityEvent GameStart;

    public UnityEvent BackToTitle;

    public UnityEvent GameOver;

	public UnityEvent GameWin;
}
