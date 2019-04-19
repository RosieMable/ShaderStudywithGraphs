// ----------------------------------------------------------------------
// -------------------- 3D Bomb
// -------------------- David Dorrington, UEL Games, 2017
// -----------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DD_3D_Bomb : MonoBehaviour {

    // ----------------------------------------------------------------------
    // Variables
    public float fl_range = 5;
    public float fl_damage = 200F;
    public float fl_accuracy = 100;
    public float fl_delay = 5;
    private float fl_timer;
    public GameObject GO_hit_text;
  

    // -----------------------------------------------------------------
    // Use this for initialization
    void Start()
    {
        fl_timer = Time.time + fl_delay;
        InvokeRepeating("ShowCount", 0, 1);

    }//-----

    // -----------------------------------------------------------------
    // Update is called once per frame
    void Update()
    {       
        // Show blast area
        if (Time.time > fl_timer - 0.5F)
            transform.localScale = new Vector3(fl_range, fl_range, fl_range);
        
        // Activate bomb
        if (Time.time > fl_timer)
        {
            FindTargets();
            Destroy(gameObject);            
        }     
    }//-----


   void ShowCount()
    { 
      // Create text mesh to show countdown
        GameObject _GO_hit_text = Instantiate(GO_hit_text, transform.position + Vector3.up, transform.rotation) as GameObject;
        _GO_hit_text.GetComponent<TextMesh>().text = Mathf.Round(fl_timer - Time.time).ToString();
        _GO_hit_text.GetComponent<TextMesh>().color = Color.red;        
   }//-----
    
    // -----------------------------------------------------------------
    void FindTargets()
    {  
        // Search of all objects in range 
        Collider[] _col_hits = Physics.OverlapSphere(transform.position, fl_range);
      
        // loop through all and send damage
         foreach (Collider _col_hit in _col_hits)
        {
            _col_hit.SendMessage("Damage", fl_damage, SendMessageOptions.DontRequireReceiver);
        }

    }//------

}//==========
