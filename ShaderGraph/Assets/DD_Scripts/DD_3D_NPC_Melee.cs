// ----------------------------------------------------------------------
// -------------------- 3D NPC Melee
// -------------------- David Dorrington, UEL Games, 2017
// ----------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DD_3D_NPC_Melee : MonoBehaviour 
{
    // ----------------------------------------------------------------------
    public GameObject GO_target; 
    public string st_target_class = "Player";
    public float fl_attack_radius = .25F;
    public float fl_engagement_range = 5;
    public float fl_damage = 30;
    public float fl_cooldown = 1F;
    public float fl_attack_success = 100;
    private float fl_next_attack_time;
    public float fl_move_speed = 1;
    public GameObject GO_hit_text;
    public GameObject GO_weapon;
    public GameObject GO_home;
    private CharacterController CC_NPC;

    // ----------------------------------------------------------------------
    // Use this for initialization
	void Start () {

        CC_NPC = GetComponent<CharacterController>();

        // if no target is set find the first tagged as the enemy
        if (!GO_target)
            GO_target = GameObject.FindWithTag(st_target_class);		
	}//-----

    // ----------------------------------------------------------------------
	// Update is called once per frame
	void Update () {

        // Functions called when the game state allows
        if (DD_3D_Game_Manager.st_game_state == "free")
        {

            ManageStates();
        }

    }//-----


    // ----------------------------------------------------------------------
	
    void ManageStates()
    {
        // Is the target in engagement range
        if (Vector3.Distance(GO_target.transform.position, transform.position) < fl_engagement_range)
        {
            // Is target close enough to hit
            if (Vector3.Distance(GO_target.transform.position, transform.position) < 1.5 && Time.time > fl_next_attack_time)
            {
                Attack();
            }
            else
            {   // Move towards the target
                Move();
            }
        }
        else
        {   // Head home and rest
            Idle();
        }

        // Reset Sword position
        if (fl_next_attack_time - 0.5F < Time.time && GO_weapon.transform.eulerAngles.x != 270)
            GO_weapon.transform.eulerAngles = new Vector3(270, transform.eulerAngles.y, transform.eulerAngles.z);

    }//-----


    // ----------------------------------------------------------------------
    void Idle()
    {
        if (Vector3.Distance(GO_home.transform.position , transform.position) > 2)
        {
            transform.LookAt(GO_home.transform.position);
            CC_NPC.Move(transform.TransformDirection(Vector3.forward * fl_move_speed * Time.deltaTime));
        }
    }//-----

    // ----------------------------------------------------------------------
    void Move()
    {       
            transform.LookAt(GO_target.transform.position);
            CC_NPC.Move(transform.TransformDirection(Vector3.forward * fl_move_speed * Time.deltaTime));      

    }//-----


    // ----------------------------------------------------------------------
    void Attack()
    {
        transform.LookAt(GO_target.transform.position);
       
        // Define attack position
        Vector3 _V3_Attack_Centre = transform.position + transform.TransformDirection(new Vector3(0.25f, 0f, 1.25F));
        // Rotate Weapon
        GO_weapon.transform.Rotate(90, 0, 0);

        // Generate random number and compare it to success variable
        if (fl_attack_success > Random.Range(0, 100))
        {
            // Search of all objects in range 
            Collider[] _col_hits = Physics.OverlapBox(_V3_Attack_Centre, new Vector3(fl_attack_radius, fl_attack_radius, fl_attack_radius), transform.rotation);
                               
            // loop through all and send damage
            foreach (Collider _col_hit in _col_hits)
            {
                _col_hit.SendMessage("Damage", fl_damage, SendMessageOptions.DontRequireReceiver);
            }
        }

        // Reset Cooldown
        fl_next_attack_time = fl_cooldown + Time.time;
    }//-----

}//==========
