using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnItemClick : MonoBehaviour {

    public Quest ItemQuest;

    private MainScript MainScript;

    private void OnMouseDown()
    {
        ItemQuest.ItemsFindCount++;
        MainScript.ShowQuests();
    }

    void Start () {
        MainScript.GetComponent<MainScript>();
	}
	
	void Update () {
		
	}
}
