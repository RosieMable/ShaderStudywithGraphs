// ----------------------------------------------------------------------
// -------------------- 3D Investigate
// -------------------- David Dorrington, UEL Games, 2017
// ----------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DD_3D_Investigate : MonoBehaviour 
{
    // ----------------------------------------------------------------------
    public float fl_distance = 4;
    public Color col_highlight = Color.yellow;
    private Color col_original;
    private GameObject GO_PC;
    private Renderer rn_attached;

    private bool bl_message_on;
    private GameObject GO_message_panel;
    private Text text_message;
    public string st_message1 = "Press e to Examine";
    public string st_message2 = "You discover ....";
    public string st_message3 = "Nothing to see";

    public string st_next_event = "A2";
    public bool bl_progress_event;
    public bool bl_release_object;
    public GameObject GO_object_to_release;
    
    private bool bl_activated;

    // ----------------------------------------------------------------------
	// Use this for initialization
	void Start () 
    {
        GO_PC = GameObject.FindWithTag("Player");
        rn_attached = GetComponent<Renderer>();
        col_original = rn_attached.material.color;

        GO_message_panel = GameObject.Find("GameManager").transform.Find("SmallMessagePanel").gameObject;
        text_message = GO_message_panel.transform.Find("SmallMessageText").gameObject.GetComponent<Text>();
        
        // Deactivate release oject
      if (bl_release_object)  GO_object_to_release.SetActive(false);

	}//-----

    // ----------------------------------------------------------------------
	// Update is called once per frame
	void Update () 
    {
          // In trigger distance change colour of object
        if (Vector3.Distance(GO_PC.transform.position, transform.position) < fl_distance)
            rn_attached.material.color = col_highlight;
        else    
            rn_attached.material.color = col_original;
      
        // As we get closer the message appears
        if (Vector3.Distance(GO_PC.transform.position, transform.position) < fl_distance/2)
        {
             // Turn on Message Panel
            GO_message_panel.SetActive(true);

            // only show inital message
            if (!bl_activated)
            {
                // First message
                text_message.text = st_message1;

                if (Input.GetKeyDown("e"))
                {   // Second message
                    text_message.text = st_message2;
                    bl_activated = true;

                    // Release object if set
                    if (bl_release_object) GO_object_to_release.SetActive(true);
                    // update event progress
                    if (bl_progress_event) DD_3D_Game_Manager.st_current_event = st_next_event;
                }
            }           
        }
        else if (Vector3.Distance(GO_PC.transform.position, transform.position) < fl_distance + 1)
        {
            GO_message_panel.SetActive(false);
            text_message.text = st_message3;
        }

	}//-----






}//==========
