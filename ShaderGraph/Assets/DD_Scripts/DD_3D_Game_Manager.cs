// ----------------------------------------------------------------------
// -------------------- 3D Game Manager
// -------------------- David Dorrington, UEL Games, 2017
// ----------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DD_3D_Game_Manager : MonoBehaviour {
    // ----------------------------------------------------------------------

    public static string st_game_state = "free";
    private string st_last_state;
    private Text text_status;

    public static bool bl_option1;
    public static bool bl_option2;
    public static bool bl_option3;
    public static bool bl_end;

    public static string st_current_event = "A1";
    // ----------------------------------------------------------------------

	// Use this for initialization
	void Start () 
    {
        text_status = GameObject.Find("StatusText").GetComponent<Text>() ;

	}//-----
    
    
    // ----------------------------------------------------------------------
	// Update is called once per frame
	void Update () {

        UpdateGameState();
        ResetButtonStates();   

	}//-----


    // ----------------------------------------------------------------------
	// Button Receivers - These are called from the button on the UI Large Message Panel
    public void Button1Pressed()
    {       
        bl_option1 = true;
    }//-----

    public void Button2Pressed()
    {
        bl_option2 = true;
    }//-----

    public void Button3Pressed()
    {
        bl_option3 = true;
    }//-----

    public void CloseButtonPressed()
    {
        bl_end = true;
    }//-----

    // ----------------------------------------------------------------------
    void ResetButtonStates()
    {
        // Reset Buttons -----------------------
        bl_option1 = false;
        bl_option2 = false;
        bl_option3 = false;
        bl_end = false;

    }//-----

    // ----------------------------------------------------------------------
    void UpdateGameState()
    {
        // Set the Status Text on screenm 
        text_status.text = "status: " + st_game_state;
        text_status.text += "  Event: " + st_current_event;

                
        // Pause ---------------------------
        if (Input.GetKeyDown("p"))
        {
            if (st_game_state != "pause")
            {
                st_last_state = st_game_state;
                st_game_state = "pause";
            }
            else
            {
                st_game_state = st_last_state;
            }
        }
    }//-----

}//==========
