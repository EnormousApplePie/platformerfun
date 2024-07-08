using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_animation : MonoBehaviour
{
    private Rigidbody2D rigid_body;

    [SerializeField] private float jump_animation_scale = 0.1f;
    private Vector3 current_scale;
    private Vector3 current_position;
    private GameObject parent_object;

    private float jump_animation_counter;
    [SerializeField] private float jump_animation_time = 0.2f;



    private void Awake()
    {
        
        rigid_body = GetComponentInParent<Rigidbody2D>();

        current_scale = transform.localScale;
        current_position = transform.localPosition;
    }
    void FixedUpdate()
    {
        
        //get the y velocity and convert it to a number in between 0 and 1
        float y_velocity = rigid_body.velocity.y;
        float y_velocity_normalized = ((Mathf.Abs(y_velocity) / 10) /2);
        //Debug.Log(y_velocity_normalized);

        //increase the the y scale based on the y velocity of the player
        
        if (y_velocity_normalized > 0.05f)
        {
           transform.localScale = new Vector3(current_scale.x, current_scale.y + y_velocity_normalized, current_scale.z);
           transform.localPosition = new Vector3(current_position.x, current_position.y + y_velocity_normalized / 2, current_position.z);
           
        }
        else if (y_velocity_normalized <= 0.05f)
        {
            transform.localScale = current_scale;
            transform.localPosition = current_position;
        }



    }

    public void jump_animation()
    {
        
    }
}
