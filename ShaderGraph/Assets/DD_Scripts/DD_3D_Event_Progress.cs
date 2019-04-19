// ----------------------------------------------------------------------
// -------------------- 3D Game Manager
// -------------------- David Dorrington, UEL Games, 2017
// ----------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DD_3D_Event_Progress : MonoBehaviour {

    // ----------------------------------------------------------------------
    public GameObject[] GO_Targets;
    private GameObject GO_pointer;

    // ----------------------------------------------------------------------
	// Use this for initialization
	void Start () 
    {
        GO_pointer = GameObject.Find("pointer");
	}//-----

    // ----------------------------------------------------------------------
	// Update is called once per frame
	void Update () 
    {
        PointAtTarget();
	}//-----

    // ----------------------------------------------------------------------
    void PointAtTarget()
    {

        if (DD_3D_Game_Manager.st_current_event == "A1")
        {
           GO_pointer.transform.LookAt(GO_Targets[0].transform.position);
        }
        if (DD_3D_Game_Manager.st_current_event == "A2")
        {
            GO_pointer.transform.LookAt(GO_Targets[1].transform.position);
        }
        if (DD_3D_Game_Manager.st_current_event == "A3")
        {
            GO_pointer.transform.LookAt(GO_Targets[2].transform.position);
        }
        if (DD_3D_Game_Manager.st_current_event == "A4")
        {
            GO_pointer.transform.LookAt(GO_Targets[3].transform.position);
        }

    }//-----


}//==========
