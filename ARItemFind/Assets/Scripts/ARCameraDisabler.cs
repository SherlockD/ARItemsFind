using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class ARCameraDisabler : MonoBehaviour {

    public GameObject ARCamera;

    private VuforiaBehaviour VB;

    public void VuforiaBehaviourSetOn()
    {
        VB = ARCamera.GetComponent<VuforiaBehaviour>();
        VB.enabled = true;
    }

    public void VuforiaBehaviourSetOff()
    {
        VB = ARCamera.GetComponent<VuforiaBehaviour>();
        VB.enabled = false;
    }

    void Start () {        

    }
	
	// Update is called once per frame
	void Update () {
	}
}
