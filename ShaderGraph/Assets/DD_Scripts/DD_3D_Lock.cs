// ----------------------------------------------------------------------
// -------------------- 3D Lock
// -------------------- David Dorrington, UEL Games, 2017
// ----------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DD_3D_Lock : MonoBehaviour {


    // ----------------------------------------------------------------------
    public float fl_distance = 3;
    public string st_message = "You need a red key to open this door";
    public GameObject GO_key;
    private GameObject GO_PC;
    private GameObject GO_message_panel;
    private Text text_message;
    private bool bl_activated;


    // ----------------------------------------------------------------------
    // Use this for initialization
	void Start () 
    {
        GO_PC = GameObject.FindWithTag("Player");
        GO_message_panel = GameObject.Find("GameManager").transform.Find("SmallMessagePanel").gameObject;
        text_message = GO_message_panel.transform.Find("SmallMessageText").gameObject.GetComponent<Text>();
		
	}//-----

    // ----------------------------------------------------------------------
    // Update is called once per frame
	void Update () 
    {
        // if the PC is close the message appears
        if (Vector3.Distance(GO_PC.transform.position, transform.position) < fl_distance)
        {
            // Turn on Message Panel
            GO_message_panel.SetActive(true);

            // only show inital message
            if (!bl_activated)
            {
                // First message
                text_message.text = st_message;                
            }
        }
        else if (Vector3.Distance(GO_PC.transform.position, transform.position) < fl_distance + 1)
        {
            GO_message_panel.SetActive(false);
        }
          
        // Remove this obstacle

         // Is the key close 
        if (Vector3.Distance(GO_key.transform.position, transform.position) < fl_distance)
        {
            Destroy(GO_key);
            Destroy(gameObject);
        }      

	
	}//-----

}//==========
