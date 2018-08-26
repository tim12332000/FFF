using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HutongGames.PlayMaker;

public class GroundScrollController : MonoBehaviour {

    public string eventName = "GO";
    public PlayMakerFSM targetFSM;
    public float ScrollSpeed;

	// Use this for initialization
	void ScrollStart() {

        targetFSM.SendEvent(eventName);

	}

}
