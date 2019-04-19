// ----------------------------------------------------------------------
// -------------------- 3D Teleport
// -------------------- David Dorrington, UEL Games, 2017
// ----------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DD_3D_Teleport : MonoBehaviour {

    // ---------------------------------------------------------------------
    public float fl_distance = 2;
    public string st_message = "Press E to Enter";
    public GameObject GO_Teleport_Target;
    private GameObject GO_PC;
    private bool bl_message_on;
    private GameObject GO_message_panel;
    private Text text_message;
   
    // Use this for initialization
	void Start () {

        GO_PC = GameObject.FindWithTag("Player");
        GO_message_panel = GameObject.Find("GameManager").transform.Find("SmallMessagePanel").gameObject;
        text_message = GO_message_panel.transform.Find("SmallMessageText").gameObject.GetComponent<Text>();
 
	}//-----
	
	// Update is called once per frame
	void Update () {

        // In trigger distance
        if (Vector3.Distance(GO_PC.transform.position, transform.position) < fl_distance)
        {
            // Turn on Message Panel
            GO_message_panel.SetActive(true); 
            text_message.text = st_message;

            if (Input.GetKeyDown("e"))
            {
                GO_PC.transform.position = GO_Teleport_Target.transform.position;
                GO_message_panel.SetActive(false);
            }

        }
        else if (Vector3.Distance(GO_PC.transform.position, transform.position) < fl_distance + 1)
        {
           GO_message_panel.SetActive(false);
        }
        
	}//-----

}//=========
