// ----------------------------------------------------------------------
// -------------------- 3D PC Health
// -------------------- David Dorrington, UEL Games, 2017
// ----------------------------------------------------------------------
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DD_3D_PC_Health : MonoBehaviour{
    // ----------------------------------------------------------------------
    public Vector3 V3_respawn_position;
    public float fl_HP = 100;
    public float fl_max_HP = 100; 
    public float fl_max_safe_fall_height = 5;
    public float fl_fall_damage = 20;
    public float fl_fatal_fall_height = 10;
    private float fl_fall_start_height;

    public GameObject GO_hit_text;
    private Transform Tx_HP_bar;
    private CharacterController CC_PC;

    // ----------------------------------------------------------------------
    // Use this for initialization
    void Start()
    {
        Tx_HP_bar = gameObject.transform.Find("HP_Bar");
        CC_PC = GetComponent<CharacterController>();        
        // Set initial respawn position
        V3_respawn_position = transform.position;        
    }//-----

    // ----------------------------------------------------------------------
    // Update is called once per frame
    void Update()
    {
         // Functions called when the game state allows
        if (DD_3D_Game_Manager.st_game_state == "free")
        {
            CheckFalling();
            CheckHealth();
            ResizeBar();
        }
    }//-----

    // ----------------------------------------------------------------------
    void CheckFalling()
    {
        if (!CC_PC.isGrounded) // in the air
        {  // set the maximum height the PC has reached in the air 
           if (transform.position.y > fl_fall_start_height) fl_fall_start_height = transform.position.y;
        }
        else // on the ground
        {
            // is the fall height damaging
            if (fl_fall_start_height - transform.position.y > fl_max_safe_fall_height)
                fl_HP -= fl_fall_damage; // subtract damage from HP
           
            if (fl_fall_start_height - transform.position.y > fl_fatal_fall_height)
                fl_HP = 0; // Kill the PC     
           // reset the fall height
            fl_fall_start_height = transform.position.y;
        }
    }//-----
   
    // ----------------------------------------------------------------------
    // Resize the HP Bar
    void ResizeBar()
    {   // is there am HP bar attached
        if (Tx_HP_bar)
        {   // Resize and colour the bar based on current HP
            Tx_HP_bar.localScale = new Vector3((fl_HP / fl_max_HP), 0.1F, 0.1F);
            if (fl_HP > fl_max_HP / 2) Tx_HP_bar.GetComponent<Renderer>().material.color = Color.green;
            if (fl_HP > fl_max_HP / 4 && fl_HP < fl_max_HP / 2) Tx_HP_bar.GetComponent<Renderer>().material.color = Color.yellow;
            if (fl_HP < fl_max_HP / 4) Tx_HP_bar.GetComponent<Renderer>().material.color = Color.red;
        }
    }//-----

    // ----------------------------------------------------------------------
    // 
    void CheckHealth()
    {
        if (fl_HP <= 0) // health depleted
        {
            transform.position = V3_respawn_position;
            fl_HP = fl_max_HP;
        }
    }//------

    // ----------------------------------------------------------------------
    // Damage Receiver
    public void Damage(float _fl_damage)
    {
       // Subtract the damage sent from HP
        fl_HP -= _fl_damage;
        // Create text mesh to show hit damage
        GameObject _GO_hit_text =  Instantiate(GO_hit_text, transform.position + Vector3.up, transform.rotation) as GameObject;
        _GO_hit_text.GetComponent<TextMesh>().text = _fl_damage.ToString();
        _GO_hit_text.GetComponent<TextMesh>().color = Color.red;
    }//-----

    // ----------------------------------------------------------------------
    // Health Receiver
    public void Health(float _fl_health)
    {
       // Add the health pichup to HP
        fl_HP += _fl_health;
        // Create text mesh to show health
        GameObject _GO_hit_text = Instantiate(GO_hit_text, transform.position + Vector3.up, transform.rotation) as GameObject;
        _GO_hit_text.GetComponent<TextMesh>().text = _fl_health.ToString();
        _GO_hit_text.GetComponent<TextMesh>().color = Color.green ;
    }//-----

 }//==========