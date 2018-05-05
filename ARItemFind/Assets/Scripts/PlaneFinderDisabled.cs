using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneFinderDisabled : MonoBehaviour {

    public GameObject PlaneFinder;
    public GameObject Button;
    public GameObject Text;

    public void PlaneFinderDisable()
    {
        PlaneFinder.SetActive(false);
        Destroy(Button);
        Destroy(Text);
    }

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
