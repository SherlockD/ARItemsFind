using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneFinderDisabler : MonoBehaviour {

    public GameObject PlaneFinder;
    public GameObject Button;
    public GameObject Text;

    public void PlaneFinderDisable()
    {
        PlaneFinder.SetActive(false);
        Button.SetActive(false);
        Text.SetActive(false);
    }

    public void PlaneFinderEnable()
    {
        PlaneFinder.SetActive(true);
        Button.SetActive(true);
        Text.SetActive(true);
    }

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
