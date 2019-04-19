using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DD_3D_PC_Hit_Colour : MonoBehaviour {

    public Color col_change = Color.cyan;
	
    // ----------------------------------------------------------------------
    // Char Controller has Hit something 
    void OnControllerColliderHit(ControllerColliderHit _CC_Hit)
    {
        //Can we changes its colour
       // if (_CC_Hit.collider.tag == "Colourable")
            // Set the colpur 
            _CC_Hit.collider.gameObject.GetComponent<Renderer>().material.color = col_change;
    }//-----
        

   //-------------------------------------------------------------------------
    // Simple PC Movement with a chracter controller
    // David Dorrington, UELGames 2017
}//================
