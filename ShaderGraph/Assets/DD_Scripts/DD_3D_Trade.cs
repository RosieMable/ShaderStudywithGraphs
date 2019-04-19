// ----------------------------------------------------------------------
// -------------------- 3D Trade Resource 
// -------------------- David Dorrington, UEL Games, 2017
// ----------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DD_3D_Trade : MonoBehaviour {

    // ----------------------------------------------------------------------
    public float fl_activation_distance = 5;

    public string st_title = "Trader";
    public string st_message = " Hello, what would you like to buy today? ";
    public string st_item1 = "Bomb";
    public int in_item1_cost = 10;
    public string st_item2 = "Arrow";
    public int in_item2_cost = 1;
    public string st_item3 = "Spell";
    public int in_item3_cost = 5;
    private GameObject GO_PC;
    private GameObject GO_message_panel;
    private Text text_message;
    private float fl_reactivation_time;

    // ----------------------------------------------------------------------
	// Use this for initialization
	void Start () 
    {   // Find the gameobjects we need
        GO_PC = GameObject.FindGameObjectWithTag("Player");
        GO_message_panel = GameObject.Find("GameManager").transform.Find("LargeMessagePanel").gameObject;
        text_message = GO_message_panel.transform.Find("LargeMessageText").gameObject.GetComponent<Text>();	
	}//-----


    // ----------------------------------------------------------------------
	// Update is called once per frame
	void Update () {
		
         if ( Time.time > fl_reactivation_time &&  Vector3.Distance(GO_PC.transform.position, transform.position) < fl_activation_distance )
         {
             // Turn on Message Panel
             GO_message_panel.SetActive(true);          
             // Call the trade function
             Trade();         
         }
        else if (Vector3.Distance(GO_PC.transform.position, transform.position) < fl_activation_distance + 1)
        {
           GO_message_panel.SetActive(false);
        }

	}//-----

    // ----------------------------------------------------------------------
    void Trade()
    {
        // Set the game state to static
        DD_3D_Game_Manager.st_game_state = "static";

        // Display Message
        text_message.text = st_title + "\n----------------------------\n\n";
        text_message.text += st_message + "\n\n";
        text_message.text += "Option 1 -  " +  st_item1 + ": will cost  " + in_item1_cost.ToString() + "  Gold \n\n";
        text_message.text += "Option 2 -  " + st_item2 + ": will cost  " + in_item2_cost.ToString() + "  Gold \n\n";
        text_message.text += "Option 3 -  " + st_item3 + ": will cost  " + in_item3_cost.ToString() + "  Gold ";

        

        int _in_gold = DD_3D_Resources.inventory.FindIndex(_item => _item.name == "Gold");
        
        // Option 1 -----------------------
        if (DD_3D_Game_Manager.bl_option1)
        {
            if ( DD_3D_Resources.inventory[_in_gold].amount_carrying >= in_item1_cost)
            {
                // Find the index in the Resource Manager of the resource we want to modify
                int _index = DD_3D_Resources.inventory.FindIndex(_item => _item.name == st_item1);
                // Add to the amount carrying
                DD_3D_Resources.inventory[_index].amount_carrying += 1;
                DD_3D_Resources.inventory[_in_gold].amount_carrying -= in_item1_cost;
            }
        }

        // Option 2 -----------------------
        if (DD_3D_Game_Manager.bl_option2)
        {
            if (DD_3D_Resources.inventory[_in_gold].amount_carrying >= in_item2_cost)
            {
                // Find the index in the Resource Manager of the resource we want to modify
                int _index = DD_3D_Resources.inventory.FindIndex(_item => _item.name == st_item2);
                // Add to the amount carrying
                DD_3D_Resources.inventory[_index].amount_carrying += 1;
                DD_3D_Resources.inventory[_in_gold].amount_carrying -= in_item2_cost;
            }
        }
        // Option 3 -----------------------
        if (DD_3D_Game_Manager.bl_option3)
        {
            if (DD_3D_Resources.inventory[_in_gold].amount_carrying >= in_item3_cost)
            {
                // Find the index in the Resource Manager of the resource we want to modify
                int _index = DD_3D_Resources.inventory.FindIndex(_item => _item.name == st_item3);
                // Add to the amount carrying
                DD_3D_Resources.inventory[_index].amount_carrying += 1;
                DD_3D_Resources.inventory[_in_gold].amount_carrying -= in_item3_cost;
            }
        }      

        // Finished  ---------------------
        
        if (DD_3D_Game_Manager.bl_end)
        {
            GO_message_panel.SetActive(false);
            fl_reactivation_time = Time.time + 5;
            DD_3D_Game_Manager.st_game_state = "free";
        }

    }//----

}//==========
