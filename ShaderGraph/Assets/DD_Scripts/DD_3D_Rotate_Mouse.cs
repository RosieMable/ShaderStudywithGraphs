// ----------------------------------------------------------------------
// -------------------- 3D Object Rotate on Mouse Click
// -------------------- David Dorrington, UEL Games, 2017
// ----------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DD_3D_Rotate_Mouse : MonoBehaviour {

    // ----------------------------------------------------------------------
   
    public float fl_rotation_amount = 45;
    public float fl_PC_distance = 5;
    GameObject GO_PC;

	// Use this for initialization
	void Start () {

         GO_PC = GameObject.Find("PC");

	}//-----
   
    // ----------------------------------------------------------------------
   	// Update is called once per frame
	void Update () {

       if (Vector3.Distance(GO_PC.transform.position, transform.position) <fl_PC_distance )
       {
           RotateObject();
       }

	}//-------


    // ----------------------------------------------------------------------
   	void RotateObject()
    {
        if (Input.GetMouseButtonDown(0))
        { // if left button pressed...
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                print(hit.collider.gameObject.name);

                if (hit.collider.gameObject == gameObject)
                {
                    transform.Rotate(0, 0, fl_rotation_amount);
                }
            }
        }

    }//-----

}//=========
