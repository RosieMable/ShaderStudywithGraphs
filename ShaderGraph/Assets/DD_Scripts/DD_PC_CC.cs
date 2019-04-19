// ----------------------------------------------------------------------
// -------------------- 3D PC Character Control - Doom Movement
// -------------------- David Dorrington, UEL Games, 2018
// ----------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DD_PC_CC : MonoBehaviour
{   
    // ----- Movement Variables     -------------------------------------------
    public float fl_speed = 6.0F;
    public float fl_gravity = 20.0F;
    public float fl_rotation_rate = 180;
    private Vector3 v3_move_direction = Vector3.zero;
    private CharacterController cc_PC;

    //-------------------------------------------------------------------------
    // Use this for initialization
    void Start()
    {   // get a reference to the attached Character Controller
        cc_PC = GetComponent<CharacterController>();
    }//-----

    //-------------------------------------------------------------------------
    // Update is called once per frame
    void Update()
    {
        MovePC();

        //Updates the _PlayerPos variable in all the shaders
        //Be aware that the parameter name has to match the one in your shaders or it wont' work
        Shader.SetGlobalVector("_PlayerPos", transform.position); //"transform" is the transform of the Player
    }//-----

    //-------------------------------------------------------------------------
    //  PC Movement control
    void MovePC()
    {
        //  PC Ground Movement
        if (cc_PC.isGrounded)
        {
            // Rotate PC with Cursor L & R  and A,D
            transform.Rotate(0, Input.GetAxis("Horizontal") * fl_rotation_rate * Time.deltaTime, 0);

            // Add Z movement to the direction vector based input axes (W,S or Cursor) 
            v3_move_direction = new Vector3(0, 0, Input.GetAxis("Vertical"));

            // Convert world coordinates to local for the PC and multiply by speed
            v3_move_direction = fl_speed * transform.TransformDirection(v3_move_direction);
        }
        // Add fl_gravity to the direction vector
        v3_move_direction.y -= fl_gravity * Time.deltaTime;

        // Move the character controller with the direction vector
        cc_PC.Move(v3_move_direction * Time.deltaTime);
    }// ----- 

}//==========
