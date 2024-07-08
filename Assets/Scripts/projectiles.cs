using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Projectiles : MonoBehaviour
{

    private float projectile_speed = 10.0f;
    private float projectile_timer = 0.0f;
    private float projectile_lifetime = 2.0f;

    [SerializeField] private float explosion_radius = 1.0f;
    [SerializeField] private float explosion_force = 30.0f;

    public LayerMask player_layer;

    [SerializeField] private GameObject game_object;
    private Rigidbody2D rigid_body;

    [SerializeField] private ParticleSystem explosion_particle = default;

    private void Start()
    {
        
        rigid_body = game_object.GetComponent<Rigidbody2D>();
        rigid_body.velocity = transform.up * projectile_speed;
        rigid_body.AddTorque(Random.Range(-0.5f, 0.5f));

        projectile_timer = projectile_lifetime;
        
    }

    private void Update()
    {
        projectile_timer -= Time.deltaTime;
        if (projectile_timer <= 0)
        {
            Debug.Log("Projectile Destroyed");
            explode();
            //
        }
    }

    private void explode()
    {
       
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosion_radius, player_layer);

        foreach (Collider2D nearby_object in colliders)
        {
            Debug.Log(nearby_object.name);
            Rigidbody2D nearby_rigid_body = nearby_object.GetComponent<Rigidbody2D>();
            Vector2 direction = nearby_rigid_body.position - rigid_body.position;
            if (nearby_rigid_body != null)
            {
                
                nearby_rigid_body.AddForce(direction.normalized * explosion_force, ForceMode2D.Impulse);

            }
        }

        Instantiate(explosion_particle, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosion_radius);
    }

}
