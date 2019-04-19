// ----------------------------------------------------------------------
// -------------------- 3D PC Melee Attack
// -------------------- David Dorrington, UEL Games, 2017
// ----------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DD_3D_PC_Melee_Attack : MonoBehaviour {

    public float fl_attack_radius = .25F;
    public float fl_damage = 30;
    public float fl_cooldown = 1F;
    public float fl_attack_success = 100;
    private float fl_next_attack_time;
    public GameObject GO_hit_text;
    public GameObject GO_weapon;

 
    // ----------------------------------------------------------------------
	// Update is called once per frame
	void Update () {

         // Functions called when the game state allows
        if (DD_3D_Game_Manager.st_game_state == "free")
        {
            MeleeAttack();
        }

	}//------

    // ----------------------------------------------------------------------
    void MeleeAttack()
    {

        if (Input.GetButtonDown("Fire2") && Time.time > fl_next_attack_time )
        {
            // Define attack position
            Vector3 _V3_Attack_Centre = transform.position + transform.TransformDirection(new Vector3(0.25f, 0f, 1.25F));

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

                if (_col_hits.Length == 0) MissText(); 

            }
            else
            {
                MissText();    
            }

           // Reset Cooldown
            fl_next_attack_time = fl_cooldown + Time.time;
        }

        if (fl_next_attack_time -.75F < Time.time && GO_weapon.transform.eulerAngles.x != 270)
        {
            GO_weapon.transform.eulerAngles = new Vector3(270, transform.eulerAngles.y, transform.eulerAngles.z);
        }

    }//-----

    void MissText()
    {
    // Create text mesh to show mishit
                GameObject _GO_hit_text = Instantiate(GO_hit_text, transform.position + transform.TransformDirection(new Vector3(0.25f, 0f, 1.25F)) + Vector3.up, transform.rotation) as GameObject;
                _GO_hit_text.GetComponent<TextMesh>().text = "Miss";
                _GO_hit_text.GetComponent<TextMesh>().color = Color.blue;

    }//----


}//=========
