// ----------------------------------------------------------------------
// -------------------- 3D Spell to Distract or freeze NPCs
// -------------------- David Dorrington, UEL Games, 2017
// -----------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class DD_3D_Spell_Caster: MonoBehaviour
{

    // -----------------------------------------------------------------
    private GameObject GO_spell_panel;
    private Text text_spells;
    private string st_spell_selected = "Distract";
    public GameObject GO_Distract_Spell;

  
    public float fl_spell_length = 5;
    private float fl_end_time;
    private bool bl_powerUpStarted;
    public float fl_new_value = 20;
    private float fl_initial_value;
    private bool bl_initial_value_set;

    // -----------------------------------------------------------------
    // Use this for initialization
	void Start () {

        GO_spell_panel = GameObject.Find("GameManager").transform.Find("SpellPanel").gameObject;
        text_spells = GO_spell_panel.transform.Find("SpellText").gameObject.GetComponent<Text>();	

	}//-----


    // -----------------------------------------------------------------
	// Update is called once per frame
	void Update () 
    {
	
        if (Input.GetKeyDown("m") )
        {
            CastSpell();
        }


       if (bl_powerUpStarted) PowerUp();

       if (bl_disguise_started) Disguise();
      

        SwapSpell();
        UpdateSpellPanelText();

	}//-------


   


    // -----------------------------------------------------------------
     void PowerUp()
    {
        if (!bl_initial_value_set)
        {
            fl_initial_value = GetComponent<DD_3D_PC_Move>().fl_initial_speed;
            GetComponent<DD_3D_PC_Move>().fl_initial_speed = fl_new_value;
            bl_initial_value_set = true;
        }           

                
        // Spell over
        if (Time.time > fl_end_time)
        {
            bl_powerUpStarted = false;
            bl_initial_value_set = false;
            GetComponent<DD_3D_PC_Move>().fl_initial_speed = fl_initial_value;
        }

    }//----


    public string st_disguise = "RedSash";
    private bool bl_disguise_started;

  
     // -----------------------------------------------------------------
     void Disguise()
     {
         print("Disguise");
         
         tag = st_disguise;

            // Spell over
         if (Time.time > fl_end_time)
         {
             bl_disguise_started = false;
             tag = "Player";
         }

     }//-----



    // -----------------------------------------------------------------
    void SwapSpell()
    {

        if (Input.GetKeyDown("n"))
        {

            if (st_spell_selected == "Distract") st_spell_selected = "PowerUp";
            else if (st_spell_selected == "PowerUp") st_spell_selected = "Disguise";
            else if (st_spell_selected == "Disguise") st_spell_selected = "Distract";
        }

    }//-----

    // -----------------------------------------------------------------
    void CastSpell()
    {
        int _in_spells = DD_3D_Resources.inventory.FindIndex(_item => _item.name == "Spell");
       
        if (DD_3D_Resources.inventory[_in_spells].amount_carrying > 0)
        {
            if (st_spell_selected == "Distract")
            {
                Instantiate(GO_Distract_Spell, transform.position + transform.TransformDirection(new Vector3(0, 0f, 2F)), transform.rotation);
             }

            if (st_spell_selected == "PowerUp")
            {
                bl_powerUpStarted = true;
                fl_end_time = Time.time + fl_spell_length;
            }

            if (st_spell_selected == "Disguise")
            {
                bl_disguise_started = true;
                fl_end_time = Time.time + fl_spell_length;
            }

        
            // Reduce Spells carried
            DD_3D_Resources.inventory[_in_spells].amount_carrying--;
        }

    }

    // -----------------------------------------------------------------
    void UpdateSpellPanelText()
    {
        // Display Message
        text_spells.text = "Spells \n ---------";
        text_spells.text += "\nCurrent: ";
        text_spells.text += st_spell_selected ;

        if (bl_powerUpStarted || bl_disguise_started) text_spells.text += "\n Time: " +  Mathf.Round(fl_end_time - Time.time).ToString();

        text_spells.text += "\n\n n-swap  m- use ";
    }

}//====================
