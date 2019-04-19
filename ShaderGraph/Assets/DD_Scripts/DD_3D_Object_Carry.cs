// ----------------------------------------------------------------------
// -------------------- 3D Object Carry
// -------------------- David Dorrington, UEL Games, 2017
// ----------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DD_3D_Object_Carry : MonoBehaviour {


    public string st_message = "Press f to carry / drop";
    private GameObject GO_PC;
    public bool bl_carrying;
    public float fl_distance = 2;

    private GameObject GO_message_panel;
    private Text text_message;
    private Rigidbody RB_attached_to_this_object;

    // ----------------------------------------------------------------------
    // Use this for initialization
    void Start()
    {
        GO_PC = GameObject.FindWithTag("Player");
        GO_message_panel = GameObject.Find("GameManager").transform.Find("SmallMessagePanel").gameObject;
        text_message = GO_message_panel.transform.Find("SmallMessageText").gameObject.GetComponent<Text>();
	

        if (GetComponent<Rigidbody>() != null) RB_attached_to_this_object = GetComponent<Rigidbody>();

    }//-----

    // ----------------------------------------------------------------------
    // Update is called once per frame
    void Update()
    {
        // In trigger distance
        if (Vector3.Distance(GO_PC.transform.position, transform.position) < fl_distance)
        {
            // Is the panel active
            if (!GO_message_panel.activeInHierarchy && !bl_carrying)
            {
                // Turn on Message Panel
                GO_message_panel.SetActive(true);
                text_message.text = st_message;
            }

            // Move them message on
            if (Input.GetKeyDown("f") && !bl_carrying)
            {
                bl_carrying = true;
                GO_message_panel.SetActive(false);
                // child this object to the target and postition it in front
                if (RB_attached_to_this_object) RB_attached_to_this_object.isKinematic = true;
                transform.position = GO_PC.transform.position + GO_PC.transform.TransformDirection(0, 0, 1.2F);
                transform.rotation = GO_PC.transform.rotation;
                transform.parent = GO_PC.transform;
            }
            else if (Input.GetKeyDown("f") && bl_carrying)
            {  //drop the object              
                transform.parent = null;
                if (RB_attached_to_this_object) RB_attached_to_this_object.isKinematic = false;
                bl_carrying = false;
            }

        }
        else if (GO_PC && Vector3.Distance(GO_PC.transform.position, transform.position) < fl_distance + 1)
        {
            GO_message_panel.SetActive(false);
        }

    }//-----

}//=====

