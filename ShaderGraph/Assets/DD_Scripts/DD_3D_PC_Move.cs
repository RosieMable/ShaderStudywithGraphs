using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DD_3D_PC_Move : MonoBehaviour
{
    //-------------------------------------------------------------------------
    // ----- Movement Variables
    public float fl_speed = 6.0F;
    public float fl_jump_force = 8.0F;
    public float fl_gravity = 20.0F;
    private Vector3 V3_move_direction = Vector3.zero;
    public float fl_initial_speed;
    private CharacterController CC_PC;
    private bool bl_climbing;

    //-------------------------------------------------------------------------
    // Use this for initialization
    void Start()
    {
        // Find the attached Character Controller
        CC_PC = GetComponent<CharacterController>();
        fl_initial_speed = fl_speed;
    }//-----

    //-------------------------------------------------------------------------
    // Update is called once per frame
    void Update()
    {
        // Only move if the game state allows
        if (DD_3D_Game_Manager.st_game_state == "free")
        {
            MovePC();
        }

        //Updates the _PlayerPos variable in all the shaders
        //Be aware that the parameter name has to match the one in your shaders or it wont' work
        Shader.SetGlobalVector("_PlayerPos", transform.position); //"transform" is the transform of the Player
    }//-----

    //-------------------------------------------------------------------------
    //  PC Movement control
    void MovePC()
    {               
        // If the run key is down double the speed
        if (Input.GetKey(KeyCode.LeftShift)) 
            fl_speed = fl_initial_speed * 2;
        else 
            fl_speed = fl_initial_speed;
        
        //  PC Ground Movement
        if (CC_PC.isGrounded)
        {
            // Add X & Z movement to the direction vector based input axes (W,A,S,D or Cursor) 
            V3_move_direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

            // Convert world coordinates to local for the PC and multiply by speed
            V3_move_direction = fl_speed * transform.TransformDirection(V3_move_direction);

            // if the jump key is pressed add jump force to the direction vector
            if (Input.GetButtonDown("Jump")) V3_move_direction.y = fl_jump_force;
        }

       

        // PC Climb Movement
        if (bl_climbing)
        {
            // Add Y movement to the direction vector based on Vertical input (W,S or Cursor U,D) 
            V3_move_direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            V3_move_direction = fl_speed / 2 * transform.TransformDirection(V3_move_direction);
            if (Input.GetButton("Jump")) V3_move_direction.y = fl_speed;
        }
        else
        {   // Add fl_gravity to the direction vector
                                       

           V3_move_direction.y -= fl_gravity * Time.deltaTime;
        }

        // Move the character controller with the direction vector
        CC_PC.Move(V3_move_direction * Time.deltaTime);

    }// -----


    //-------------------------------------------------------------------------
    // When PC enters a trigger
    void OnTriggerStay(Collider cl_trigger_collider)
    {
        if (cl_trigger_collider.gameObject.tag == "Moving") transform.parent = cl_trigger_collider.transform;
        if (cl_trigger_collider.gameObject.tag == "ClimbZone") bl_climbing = true;
    }//-----


    //-------------------------------------------------------------------------
    // PC Leaving the Trigger
    void OnTriggerExit(Collider cl_trigger_collider)
    {
        if (cl_trigger_collider.gameObject.tag == "Moving") transform.parent = null;
        if (cl_trigger_collider.gameObject.tag == "ClimbZone") bl_climbing = false;
    }//-----    


    //-------------------------------------------------------------------------
    // Simple PC Movement with a chracter controller
    // David Dorrington, UEL Game 2017
}//================
