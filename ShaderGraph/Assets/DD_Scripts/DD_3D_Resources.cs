// ----------------------------------------------------------------------
// -------------------- 3D Resource Manager
// -------------------- David Dorrington, UEL Games, 2017
// ----------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DD_3D_Resources : MonoBehaviour {

    // ----------------------------------------------------------------------
    public static List<item> inventory;
    private GameObject GO_resource_panel;
    private Text text_resources;

    // ----------------------------------------------------------------------
    // Use this for initialization
    void Start ()
    {             
        // Create the Inventory
        inventory = new List<item>
        {
            new item() { name="Arrow",  cost = 1,    amount_carrying = 100,  maximum = 1000, weight = 1, component1 = "Wood",  comp1_amount = 1 , component2 = "Iron", comp2_amount = 1 },
            new item() { name="Bomb",   cost = 10,   amount_carrying = 5,    maximum = 5,    weight = 10 },
            new item() { name="Gold",   cost = 1,    amount_carrying = 100,  maximum = 1000, weight = 10 },
            new item() { name="Spell",  cost = 5,    amount_carrying = 10,   maximum = 10,   weight = 2 },
            new item() { name="Wood",   cost = 1,    amount_carrying = 10,   maximum = 15,  weight = 2 },
            new item() { name="Iron",   cost = 1,    amount_carrying = 10,   maximum = 100,  weight = 2 },
        };

        // Find Game Objects
        GO_resource_panel = transform.Find("ResourcePanel").gameObject;
        text_resources = GO_resource_panel.transform.Find("ResourceText").gameObject.GetComponent<Text>();

    }//-----

     // ----------------------------------------------------------------------
     // Define the item objects for the invertory
    public class item
    {
        public string name { get; set; }
        public int amount_carrying { get; set; }
        public int maximum { get; set; }
        public float cost { get; set; }
        public int weight { get; set; }
        public string component1 { get; set; }
        public int comp1_amount { get; set; }
        public string component2 { get; set; }
        public int comp2_amount { get; set; }
    }//-----


    // ----------------------------------------------------------------------
    // Update is called once per frame
    void Update ()
    {
        UpdateGUI();      

    }//-----
    
    
    // ----------------------------------------------------------------------
    void UpdateGUI()
    {
        // Toggle Panel On / Off -------------

        if (Input.GetKeyDown("i"))
        {            
            if (GO_resource_panel.activeInHierarchy)
                GO_resource_panel.SetActive(false);
            else
                GO_resource_panel.SetActive(true);          
        }

        // Update the Resource Stats ---------------------

        // Local string
        string _st_resources = "Inventory \n-------------------";

        // Loop thrught the list and display the contents
        foreach (item _item in inventory)
        {
            _st_resources +=  "\n" + _item.name + ": "+ _item.amount_carrying ;
        }

        // Update the panel text
        text_resources.text = _st_resources;
                 
    }//-----

}//=========
