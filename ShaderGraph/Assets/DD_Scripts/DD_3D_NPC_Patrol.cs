// ----------------------------------------------------------------------
// -------------------- 3D NPC Waypoint Patrol 
// -------------------- David Dorrington, UEL Games, 2017
// ----------------------------------------------------------------------


using UnityEngine;
using System.Collections;

public class DD_3D_NPC_Patrol : MonoBehaviour 
{
    // ----------------------------------------------------------------------
    // Combat Variables
    public GameObject GO_projectile;
    public float fl_range = 15;
    public float fl_cool_down = 1;
    public int in_ammo = 10000;
    public float fl_accuracy = 100;
    private float fl_delay;
    public bool bl_line_of_sight;
    public string st_target_class = "Player";

    // Movement
    public GameObject[] GOS_waypoints;
    public float fl_speed = 3;
    private int in_next_wp = 0; 
     
    public GameObject GO_target;
    private CharacterController CC_NPC;

    // ----------------------------------------------------------------------
    // Use this for initialization
    void Start () 
    {   // Find the Game Objects we need to interact with
        CC_NPC = GetComponent<CharacterController>();   
         // if no target is set find the first tagged as the enemy
        if (!GO_target)
            GO_target = GameObject.FindWithTag(st_target_class);
    }//-----

    // ----------------------------------------------------------------------
    // Update is called once per frame
    void Update () 
    {   
 	    // Functions called when the game state allows
        if (DD_3D_Game_Manager.st_game_state == "free")
        {

             if (Vector3.Distance(transform.position, GO_target.transform.position) < fl_range)
                 AttackTarget();
             else
                 Patrol();        
        }

    }//-----
    
    // ----------------------------------------------------------------------
    void Patrol()
    {
        
        //Are there any waypoints defined?
        if (GOS_waypoints.Length > 0)
        {   // Look at the next WP
            transform.LookAt(GOS_waypoints[in_next_wp].transform.position);
           
            // Move towards the WP
            CC_NPC.SimpleMove(fl_speed * transform.TransformDirection(Vector3.forward));

            // if we get close move to WP target the next
            if (Vector3.Distance(GOS_waypoints[in_next_wp].transform.position, transform.position ) < 1)
            {
                if (in_next_wp < GOS_waypoints.Length - 1)
                    in_next_wp++;                
                else               
                    in_next_wp = 0;         
            }
        }
    }//-----


    // ----------------------------------------------------------------------
    void AttackTarget()
    {
      // Does the NPC have ammo and has the cooldown passed
        if ( in_ammo > 0 && Time.time > fl_delay)
        {
            // Face the Target
            transform.LookAt(GO_target.transform.position);
            CC_NPC.SimpleMove(Vector3.zero);

            // Is the line of sight flag checked? 
            if (bl_line_of_sight)
            {
                // Cast a Ray to check if Target in is view of NPC
                RaycastHit _RC_hit;
                Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out _RC_hit, fl_range);

                // if the Target is in sight create an arrow
                if (_RC_hit.collider != null && _RC_hit.collider.gameObject == GO_target) FireProjectile();
                
            }
            else
            {   // Shoot at Target even if there is something in the way            
                FireProjectile();
            }
        }
    }//------

     // ----------------------------------------------------------------------
    void FireProjectile()
    {
        // Spawn an arrow     
        GameObject _GO_projectile_clone =  Instantiate(GO_projectile, transform.position + transform.TransformDirection(new Vector3(0, 0, 1F)), transform.rotation);

        // Create a random rotation based on accuracy
        Vector3 _V3_accuracy_offset = new Vector3( Random.Range(-100 + fl_accuracy, 100 - fl_accuracy)/10 , Random.Range(-100 + fl_accuracy, 100 - fl_accuracy) /10 , 0);
      
        _GO_projectile_clone.transform.Rotate(_V3_accuracy_offset);

        fl_delay = Time.time + fl_cool_down;
        in_ammo--;
    }//-----


}//==========
