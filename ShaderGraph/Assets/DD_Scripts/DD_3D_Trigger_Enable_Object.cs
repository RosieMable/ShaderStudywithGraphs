using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DD_3D_Trigger_Enable_Object : MonoBehaviour {

	// ----------------------------------------------------------------------
    // Variables in this script
    public GameObject GO_target;
    public GameObject GO_trigger;
    public bool bl_destroy_trigger_when_activated;
    public float fl_delay;
    public bool bl_enable;
    private float fl_time;
    private bool bl_triggered;

    // ----------------------------------------------------------------------
    // Use this for initialization
    void Start()
    {
        // Set the Target object to Disabled if we are to enable it on the trigger 
        if (bl_enable) GO_target.SetActive(false);
    }//-----

    // ----------------------------------------------------------------------
    // Update is called once per frame
    void Update()
    {
        // If the trigger has been activated swap the target state after the time delay
        if (bl_triggered && Time.time > fl_time)
        {
            if (bl_enable)
                GO_target.SetActive(true);
            else
                GO_target.SetActive(false);

            //Destroy this object when complete 
            if (bl_destroy_trigger_when_activated) Destroy(gameObject);
        }
    }//-----  

    // ----------------------------------------------------------------------
    // Detect if something enters the Trigger
    void OnTriggerEnter(Collider cl_trigger)
    {
        // Is the trigger the correct trigger GameObject 
        if (cl_trigger.gameObject == GO_trigger)
        {
            // Set the trigger flag
            bl_triggered = true;

           // Set the time delay
           fl_time = Time.time + fl_delay;
        }
    }//-----

    //-------------------------------------------------------------------------
    // Enabling or Disbale object on Trigger
    // David Dorrington, UELGames 2017
}//================
