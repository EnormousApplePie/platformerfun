using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    private Rigidbody rigid_body;
    private Collider player_collider;
    [SerializeField] private ParticleSystem death_particle = default;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Death")
        {
            Instantiate(death_particle, transform.position, Quaternion.identity);
            FindObjectOfType<LevelManager>().respawn_player();
            Destroy(gameObject);
        }
    }
}

