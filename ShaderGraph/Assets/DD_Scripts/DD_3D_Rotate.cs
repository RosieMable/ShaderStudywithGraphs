using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DD_3D_Rotate : MonoBehaviour {

    public Vector3 V3_Rotation_Axis = new Vector3 (0,90,0);  
 
    //-------------------------------------------------------------------------
    void Update()
    {
         // Functions called when the game state allows
        if (DD_3D_Game_Manager.st_game_state == "free")
        {

            transform.Rotate(V3_Rotation_Axis * Time.deltaTime);
        }

    }//-----    

    //-------------------------------------------------------------------------
    // Simple Rotate
    // David Dorrington, UELGames 2017
}//================
