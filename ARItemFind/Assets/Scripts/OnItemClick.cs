using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnItemClick : MonoBehaviour {

    public Quest ItemQuest;

    private void OnMouseDown()
    {
        ItemQuest.ItemsFindCount++;
    }

    void Start () {

	}
	
	void Update () {
		
	}
}
