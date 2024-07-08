using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Rigidbody2D rigid_body;

    private Vector3 current_scale;
    private Vector3 current_position;

    private void Awake()
    {
        
        rigid_body = GetComponentInParent<Rigidbody2D>();

        current_scale = transform.localScale;
        current_position = transform.localPosition;
    }
    void Update()
    {

        // Normalize the y velocity to a value between 0 and 1.
        float y_velocity = rigid_body.velocity.y;
        float y_velocity_normalized = ((Mathf.Abs(y_velocity) / 10) /2);
        
        // Stretch the player sprite when jumping.
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
}
