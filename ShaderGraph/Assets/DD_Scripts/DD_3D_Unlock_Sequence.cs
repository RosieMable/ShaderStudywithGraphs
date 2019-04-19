// ----------------------------------------------------------------------
// -------------------- 3D Rotational Lock
// -------------------- David Dorrington, UEL Games, 2017
// ----------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DD_3D_Unlock_Sequence : MonoBehaviour {

    // ----------------------------------------------------------------------
    public float fl_distance = 2;
    public GameObject[] GOS_locks;
    public float[] fl_solutions;
    public GameObject GO_barrier;
    public bool bl_enable = true;
    public int in_correct;
    public int in_index;

    private bool bl_message_on;
    private GameObject GO_message_panel;
    private Text text_message;
    public string st_message = "Rotate the objects to unlock the door";

   private  GameObject GO_PC;

   // ----------------------------------------------------------------------
   // Use this for initialization
    void Start()
    {
        GO_PC = GameObject.Find("PC");
        GO_message_panel = GameObject.Find("GameManager").transform.Find("SmallMessagePanel").gameObject;
        text_message = GO_message_panel.transform.Find("SmallMessageText").gameObject.GetComponent<Text>();

    }//-----


    // ----------------------------------------------------------------------
    // Update is called once per frame
	void Update () {

        // In trigger distance
        if (Vector3.Distance(GO_PC.transform.position, transform.position) < fl_distance)
        {
            // Turn on Message Panel
            GO_message_panel.SetActive(true);
            text_message.text = st_message;       


        }
        else if (Vector3.Distance(GO_PC.transform.position, transform.position) < fl_distance + 1)
        {
            GO_message_panel.SetActive(false);
        }

        CheckRotation();

	}//-----

    // ----------------------------------------------------------------------
    void CheckRotation()
    {
        // Reset Index        
        in_index = 0;
        in_correct = 0;

        // Loop through all objects and check rotation
        foreach (GameObject _GO in GOS_locks)
        {
            if ((_GO.transform.eulerAngles.z > fl_solutions[in_index] - 5) && (_GO.transform.eulerAngles.z < fl_solutions[in_index] + 5))
            {
                in_correct++;                
            }

            in_index++;
        }

        // All in correct position - Remove Barrier
        if (in_correct >= GOS_locks.Length)
        {
            GO_barrier.SetActive(false);
            Destroy(gameObject);
        }
    }//-----

    
}//==========
