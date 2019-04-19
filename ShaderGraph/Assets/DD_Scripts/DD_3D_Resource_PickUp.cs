// ----------------------------------------------------------------------
// -------------------- 3D Resource Pickup
// -------------------- David Dorrington, UEL Games, 2017
// ----------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DD_3D_Resource_PickUp : MonoBehaviour {
    // ----------------------------------------------------------------------
    public string st_resource = "Wood";
    public int in_amount = 10;
    public GameObject GO_hit_text;

    // ----------------------------------------------------------------------
    void Start()
    {
        //Set the text 
        GetComponentInChildren<TextMesh>().text = in_amount.ToString() + " " +  st_resource;

    }//----

    // ----------------------------------------------------------------------
    // Detect if something enters the Trigger
    void OnTriggerEnter (Collider _cl_collider)
    {
        // has the PC entered the trigger
        if (_cl_collider.gameObject.tag == "Player")
        {
            // Find the index in the Resource Manager of the resource we want to modify
            int _index = DD_3D_Resources.inventory.FindIndex(_item => _item.name == st_resource);

            // Add to the amount carrying
            DD_3D_Resources.inventory[_index].amount_carrying += in_amount;

            // Cap Resources
            if ( DD_3D_Resources.inventory[_index].amount_carrying > DD_3D_Resources.inventory[_index].maximum )
            {
                DD_3D_Resources.inventory[_index].amount_carrying = DD_3D_Resources.inventory[_index].maximum; 
            }
                     
            // Spawn Hit text to give player feedback
            GameObject _GO_hit_text = Instantiate(GO_hit_text, transform.position + Vector3.up , _cl_collider.transform.rotation) as GameObject;
            _GO_hit_text.GetComponent<TextMesh>().text = in_amount.ToString();
            _GO_hit_text.GetComponent<TextMesh>().color = Color.magenta;

            // Remove the object from the scene            
            Destroy(this.gameObject);
        }

    }//-----	

}//==========
