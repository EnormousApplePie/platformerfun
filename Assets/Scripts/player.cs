using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody rigid_body;
    private Collider player_collider;
    [SerializeField] private ParticleSystem death_particle = default;

    [SerializeField] private AudioClip[] death_clips;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Death")
        {
            Instantiate(death_particle, transform.position, Quaternion.identity);
            LevelManager.instance.respawn_player();
            SoundManager.instance.play_random_sound_clip(death_clips, transform, 1f);
            Destroy(gameObject);
        }
    }
}

