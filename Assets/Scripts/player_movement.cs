using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_movement : MonoBehaviour
{

    // Player Components
    private Rigidbody2D rigid_body;
    private Collider2D player_collider;

    // Player movement variables.
    [SerializeField] private float move_speed = 5f;
    [SerializeField] private float jump_force = 5f;
    [SerializeField] private float gravity_scale = 1f;

    // Coyote time variables
    [SerializeField] private float coyote_time = 0.2f;
    private float coyote_counter;

    // Player Jump variables
    [SerializeField] private float jump_buffer_time = 0.1f;
    private float jump_buffer_counter = 0;
    private bool is_grounded;
    public Transform ground_check;
    public LayerMask ground_layer;
    
    private void Awake()
    {
        rigid_body = GetComponent<Rigidbody2D>();
        player_collider = GetComponent<Collider2D>();

        
    }

    private void FixedUpdate()
    {
        // Add gravity
        rigid_body.AddForce(new Vector2(0, -9.81f * gravity_scale));
    }


    private void Update()
    {
        rigid_body.velocity = new Vector2(Input.GetAxis("Horizontal") * move_speed, rigid_body.velocity.y);
        //rigid_body.AddForce(new Vector2(Input.GetAxis("Horizontal") * move_speed, 0));
        is_grounded = Physics2D.Raycast(ground_check.position, Vector2.down, player_collider.bounds.size.y / 2 + 0.01f, ground_layer);
        
        do_coyote_time();
        player_jumping();
    }

    //-----------------------------------------------------------------------------------------------
    /// <summary>
    /// Logic ran in the Update function to handle player jumping.
    /// </summary>
    private void player_jumping()
    {
       
        if (Input.GetButtonDown("Jump"))
        {
            jump_buffer_counter = jump_buffer_time;
        }

        else jump_buffer_counter -= Time.deltaTime;

        if (Input.GetButtonUp("Jump"))
        {
            player_jump(true);
        }

        if (can_jump())
        {
            player_jump(false);
        }
    }

    //-----------------------------------------------------------------------------------------------
    /// <summary>
    /// handles the coyote timer. Allows the player to jump for a short period of time after they have left the ground.
    /// </summary>
    private void do_coyote_time()
    {
        if (is_grounded)
        {
            coyote_counter = coyote_time;
        }
        else
        {
            coyote_counter -= Time.deltaTime;
        }
    }

    //-----------------------------------------------------------------------------------------------
    /// <summary>
    /// Returns true if the player can jump.
    /// </summary>
    private bool can_jump()
    {
        if (coyote_counter > 0 && jump_buffer_counter > 0f) return true;
        
        return false;
    }
    //-----------------------------------------------------------------------------------------------
    /// <summary>
    /// Sets the player's velocity to a jump force. Adds a force or halves it depending on button_up.
    /// </summary>
    private void player_jump(bool button_up)
    {
        // Normal jump
        if (!button_up)
        {
            rigid_body.velocity = new Vector2(rigid_body.velocity.x, jump_force);
            jump_buffer_counter = 0f;
        }
        else if (rigid_body.velocity.y > 0f)
        {
            rigid_body.velocity = new Vector2(rigid_body.velocity.x, rigid_body.velocity.y * 0.5f);
            coyote_counter = 0f;
        }
    }

    //-----------------------------------------------------------------------------------------------


}
