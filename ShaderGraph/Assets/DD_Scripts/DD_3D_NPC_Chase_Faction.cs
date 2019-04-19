// ----------------------------------------------------------------------
// -------------------- 3D NPC Chase / Attack based on Faction
// -------------------- David Dorrington, UEL Games, 2017
// ----------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DD_3D_NPC_Chase_Faction : MonoBehaviour 
{    

    // ----------------------------------------------------------------------
    // Combat Variables
    public GameObject GO_projectile;
    public float fl_attack_range = 20;
    public float fl_cool_down = 1;
    public float fl_accuracy = 100;
    public int in_ammo = 100000;
    public bool bl_line_of_sight;
    public string st_target_class = "Player";
    public string st_target_class2 = "City_Guard";
    private float fl_delay;   
    
    // Movement
    public bool bl_chase = true;
    public float fl_chase_dist_max = 10;
    public float fl_chase_dist_min = 3;
    public float fl_chase_speed = 3;

    public GameObject GO_home;
    public GameObject GO_target;
    private CharacterController CC_NPC;

    // ----------------------------------------------------------------------

    // Use this for initialization
    void Start () 
    {   
        CC_NPC = GetComponent<CharacterController>();

       // Cap accuracy
        if (fl_accuracy > 100) fl_accuracy = 100;

        // if no target is set find the first tagged as the enemy
        if (!GO_target)
            GO_target = GameObject.FindWithTag(st_target_class);
	}//-----

    // ----------------------------------------------------------------------
    // Update is called once per frame
    void Update () 
    {
        if (DD_3D_Game_Manager.st_game_state == "free")
        {
            if (bl_chase)
            {
                NPC_Move();
            }

            if (GO_target)
                AttackTarget();
            else
                FindTarget();
        }
    }//-----



    
    // ----------------------------------------------------------------------
    void FindTarget()
    {

        // temp variables
        float _dist = Mathf.Infinity;
        GameObject _GO_nearest = null;

        // Find the nearest Target       

            // Create a List of potential targets
            GameObject[] _GO_Enemies = GameObject.FindGameObjectsWithTag(st_target_class);

            // Are there any tagged targets in the scene?
            if (_GO_Enemies.Length > 0)
            {
                // Loop through the list of targets
                foreach (GameObject _GO in _GO_Enemies)
                {
                    float _cur_dist = Vector3.Distance(_GO.transform.position, transform.position);
                    if (_cur_dist < _dist)
                    {
                        _GO_nearest = _GO;
                        _dist = _cur_dist;
                    }
                }
            }

            // Create a List of potential targets
            GameObject[] _GO_Enemies2 = GameObject.FindGameObjectsWithTag(st_target_class2);

            // Are there any tagged targets in the scene?
            if (_GO_Enemies.Length > 0)
            {
                // Loop through the list of targets
                foreach (GameObject _GO in _GO_Enemies2)
                {
                    float _cur_dist = Vector3.Distance(_GO.transform.position, transform.position);
                    if (_cur_dist < _dist)
                    {
                        _GO_nearest = _GO;
                        _dist = _cur_dist;
                    }
                }
            }
    
        // Set the Target
         GO_target = _GO_nearest;
        
    }//-----


    // ----------------------------------------------------------------------
    void NPC_Move()
    {
        // Is the target in Range
        if (GO_target && Vector3.Distance(transform.position, GO_target.transform.position) < fl_chase_dist_max)
        {   // Face the Target
            transform.LookAt(GO_target.transform.position);

            if (Vector3.Distance(transform.position, GO_target.transform.position) > fl_chase_dist_min)
            {   // Move towards the target
                CC_NPC.SimpleMove(fl_chase_speed * transform.TransformDirection(Vector3.forward));
            }
        }
        else if (GO_home) // is there a home object set
        {
            if (Vector3.Distance(transform.position, GO_home.transform.position) > fl_chase_dist_min * 2)
            {
                // Head Home
                transform.LookAt(GO_home.transform.position);
                transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
                CC_NPC.SimpleMove(fl_chase_speed * transform.TransformDirection(Vector3.forward));
            }
        }
        else
        {  // stop moving
            CC_NPC.SimpleMove(Vector3.zero);            
        }
    }//-----


    // ----------------------------------------------------------------------
    void AttackTarget()
    {
        if (in_ammo > 0 && Time.time > fl_delay && Vector3.Distance(transform.position, GO_target.transform.position ) < fl_attack_range )
        {
            // Face the Target
            transform.LookAt(GO_target.transform.position);
            
            // Is the line of sight flag checked? 
            if (bl_line_of_sight)
            {    // Cast a Ray to check if Target in is view of NPC
                RaycastHit _RC_hit;
                Physics.Raycast( transform.position, transform.TransformDirection(Vector3.forward), out _RC_hit, fl_attack_range) ;

                // if the Target is in sight fire a projectile
                if (_RC_hit.collider != null  && _RC_hit.collider.gameObject == GO_target) FireProjectile();                               
            }
            else
            {
                FireProjectile();  
            }            
        }

        if (Vector3.Distance(transform.position, GO_target.transform.position) > fl_attack_range + 1)
            GO_target = null;


    }//------

    // ----------------------------------------------------------------------
    void FireProjectile()
    {
        // Spawn an arrow     
        GameObject _GO_projectile_clone =  Instantiate(GO_projectile, transform.position + transform.TransformDirection(new Vector3(0, 0, 1F)), transform.rotation);

        // Create a random rotation based on accuracy
        Vector3 _V3_accuracy_offset = new Vector3( Random.Range(-100 + fl_accuracy, 100 - fl_accuracy)/10 , Random.Range(-100 + fl_accuracy, 100 - fl_accuracy) /10 , 0);
        _GO_projectile_clone.transform.Rotate(_V3_accuracy_offset);

        //Reset Cooldown
        fl_delay = Time.time + fl_cool_down;
        // Reduce ammo
        in_ammo--;
    }//-----
 
}//==========

