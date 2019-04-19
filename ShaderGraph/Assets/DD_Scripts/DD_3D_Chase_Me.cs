// ----------------------------------------------------------------------
// -------------------- 3D NPC HChase 
// -------------------- David Dorrington, UEL Games, 2017
// ----------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DD_3D_Chase_Me : MonoBehaviour {

    // Movement
    public GameObject[] GOS_waypoints;
    public GameObject GO_PC_Start;
    public float fl_speed = 3;
    private int in_next_wp = 0;
    public bool bl_catch_NPC;

    public float fl_min_distance = 2;
    public float fl_max_distance = 5;

    private CharacterController CC_NPC;
    private GameObject GO_PC;
    private bool bl_chase_started;
    private bool bl_goal_reached;
    private Vector3 V3_start_pos;


    // ----------------------------------------------------------------------
    // Use this for initialization
    void Start()
    {   // Find the Game Objects we need to interact with
        CC_NPC = GetComponent<CharacterController>();
        GO_PC = GameObject.FindWithTag("Player");

        V3_start_pos = transform.position;

    }//-----

    // ----------------------------------------------------------------------
    // Update is called once per frame
    void Update()
    {
        // Functions called when the game state allows
        if (DD_3D_Game_Manager.st_game_state == "free")
        {
         
            if (!bl_goal_reached)
                CheckDistance();

            if (bl_chase_started)
                NPCMove();           
        }

    }//-----


    // ----------------------------------------------------------------------
    void Success()
    {
       // // what would you like the game to do next?
       // drop an object
       // move on the events in game manager?
       // add some resources

       // destroy this NPC?

    }///-----



    // ----------------------------------------------------------------------
    void CheckDistance()
    {
        // Is the PC near to start the chase
        if (!bl_chase_started &&  Vector3.Distance (GO_PC.transform.position, GO_PC_Start.transform.position ) < 1  )
        {
            bl_chase_started = true;
        }


        // Is the PC within distance range
        if (bl_chase_started && !bl_goal_reached)
        {
            if (Vector3.Distance(GO_PC.transform.position, transform.position) < fl_min_distance ||
               Vector3.Distance(GO_PC.transform.position, transform.position) > fl_max_distance)
            {
                Restart();
            }
        }

        if (bl_goal_reached) Success();

    }//----


    // ----------------------------------------------------------------------
  
    void Restart()
    {
        // reset positions
        transform.position = V3_start_pos;
        in_next_wp = 0;

        GO_PC.transform.position = GO_PC_Start.transform.position;
    }

    // ----------------------------------------------------------------------
    void NPCMove()
    {
        //Are there any waypoints defined?
        if (GOS_waypoints.Length > 0 && !bl_goal_reached)
        {   // Look at the next WP
            transform.LookAt(GOS_waypoints[in_next_wp].transform.position);

            // Move towards the WP
            CC_NPC.SimpleMove(fl_speed * transform.TransformDirection(Vector3.forward));

            // if we get close move to WP target the next
            if (Vector3.Distance(GOS_waypoints[in_next_wp].transform.position, transform.position) < 1)
            {
                if (in_next_wp < GOS_waypoints.Length - 1)
                    in_next_wp++;
                else
                {
                    bl_goal_reached = true;
                }
            }
        }
    }//-----
		
}//======================
