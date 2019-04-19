// ----------------------------------------------------------------------
// -------------------- 3D Spell to Distract or freeze NPCs
// -------------------- David Dorrington, UEL Games, 2017
// -----------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DD_3D_Spell_NPC_Distract : MonoBehaviour
{


    // ----------------------------------------------------------------------
    // Variables
    public float fl_range = 10;
    public GameObject GO_hit_text;

    public float fl_delay = 2;
    public float fl_spell_length = 5;
    private float fl_start_time;
    private float fl_end_time;
    public bool bl_targets_found;
    public bool bl_targets_disabled;
    public bool bl_distract;

    public List<GameObject> GO_list_Enemies = new List<GameObject>();
    
    
    // ----------------------------------------------------------------------
    // Use this for initialization
	void Start () {
        fl_start_time = Time.time + fl_delay;
        fl_end_time = fl_start_time + fl_spell_length;
      
        // Show countdown
        InvokeRepeating("ShowCount", 0, 1);

	}//-----

    // ----------------------------------------------------------------------
    // Update is called once per frame
	void Update () {

        // Activate Spell after start delay
        if (Time.time > fl_start_time)
        {
           if (!bl_targets_found)
           {
               FindTargets();            
           }
           else
           {
              // Disable all NPCs in the list scripts
               if (!bl_targets_disabled)  
                  DisableComponents();

               // if distract is checked in the inspector distract the NPCs 
              if (bl_distract) 
                  DistractNPCs();

               // Spell time is over
               if (Time.time > fl_end_time)
               {
                   EnableComponents();
                   Destroy(gameObject);
               }
           }     
      } 
	}//-----


    // -----------------------------------------------------------------
    void DistractNPCs()
    {
        foreach (var _GO_enemy in GO_list_Enemies)
        {
            // Make each Enemy look at the spell location
            _GO_enemy.transform.LookAt(transform.position);


            // Move each enemy to the spell loacation
            if (Vector3.Distance(transform.position, _GO_enemy.transform.position) > 1)
            {
                _GO_enemy.GetComponent<CharacterController>().SimpleMove(_GO_enemy.transform.TransformDirection(Vector3.forward));
            }
        }

    }//-----


    // -----------------------------------------------------------------
    void EnableComponents()
    {
        foreach (var _GO_enemy in GO_list_Enemies)
        {
            var components = _GO_enemy.gameObject.GetComponentsInChildren<Component>();
            
            foreach (var component in components)
            {
                var behaviour = component as Behaviour;
                if (behaviour) behaviour.enabled = true;
            }
        }
    }//-----


    // -----------------------------------------------------------------
    void DisableComponents()
    {
        foreach (var _GO_enemy in GO_list_Enemies)
        {
            var components = _GO_enemy.gameObject.GetComponentsInChildren<Component>();

            foreach (var component in components)
            {
                var behaviour = component as Behaviour;
                if (behaviour) behaviour.enabled = false;
            }
        }

        bl_targets_disabled = true;
    }//-----


    // -----------------------------------------------------------------
    void ShowCount()
    {
        // Create text mesh to show countdown
        GameObject _GO_hit_text = Instantiate(GO_hit_text, transform.position + Vector3.up, transform.rotation) as GameObject;
                
        if (!bl_targets_disabled)  // countdown to spell start
        {
            _GO_hit_text.GetComponent<TextMesh>().color = Color.blue;
            _GO_hit_text.GetComponent<TextMesh>().text = Mathf.Round(fl_start_time - Time.time).ToString();
        }
        else // countdown for spell duration
        {
            _GO_hit_text.GetComponent<TextMesh>().color = Color.red;
            _GO_hit_text.GetComponent<TextMesh>().text = Mathf.Round(fl_end_time - Time.time).ToString();
        }
     
    }//-----

    
    // -----------------------------------------------------------------
    void FindTargets()
    {
        // Search of all objects in range 
        Collider[] _col_hits = Physics.OverlapSphere(transform.position, fl_range);

        // loop through all objects found
        foreach (Collider _col_hit in _col_hits)
        {
            // Add the objects with the tag to the enemy list
            if (_col_hit.tag == "Enemy")
            {
                GO_list_Enemies.Add(_col_hit.gameObject);
            }
        }

        bl_targets_found = true;
        
    }//------
    
}//=========
