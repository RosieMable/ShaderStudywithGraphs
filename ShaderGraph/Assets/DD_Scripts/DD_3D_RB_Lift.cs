using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DD_3D_RB_Lift : MonoBehaviour {
        //-------------------------------------------------------------------------
    public float fl_height_max = 10;
    public float fl_height_min = 1;
    public float fl_speed = 100;
    private Rigidbody RB_lift;
    
    //-------------------------------------------------------------------------
    // Use this for initialization
	void Start () 
    {
        // Get the attached Rigid body and start moving up
        RB_lift = GetComponent<Rigidbody>();
        RB_lift.useGravity = false;
        RB_lift.velocity = new Vector3(0, fl_speed * Time.deltaTime, 0);         

	}//-----
	
	//-------------------------------------------------------------------------
    // Update is called once per frame
	void Update () {
        
        // At the height max enable gravity
        if (transform.position.y > fl_height_max)
        {
            RB_lift.velocity = Vector3.zero;
            RB_lift.useGravity = true;
        }
       
        // At the height min turn of Gravity and move up
        if (transform.position.y < fl_height_min)
        {
            RB_lift.useGravity = false;
            RB_lift.velocity = new Vector3(0, fl_speed * Time.deltaTime, 0);
        }      
    }//-----
		
        //-------------------------------------------------------------------------
        // Simple Rigidbody Lift
        // David Dorrington, UELGames 2017
}//================
