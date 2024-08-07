using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilities : MonoBehaviour
{
    private float ability_grenade_timer = 0.0f;
    private float ability_grenade_cooldown = 1.0f;

    [SerializeField] private GameObject grenade_prefab;
    [SerializeField] private AudioClip[] grenade_throw_clips;

    public static bool grenade_ability = false;

    private void Update()
    {

        // cheat, press P to unlock grenade ability
        if (Input.GetKeyDown(KeyCode.P))
        {
            grenade_ability = true;
        }

        if (Input.GetButtonDown("Fire1") && ability_grenade_timer == 0 && grenade_ability)
        {
            ability_grenade_timer = ability_grenade_cooldown;

            SoundManager.instance.play_random_sound_clip(grenade_throw_clips, transform, 1f);
            Instantiate(grenade_prefab, transform.position, transform.rotation);

            
        }

        if (ability_grenade_timer > 0)
        {
            ability_grenade_timer -= Time.deltaTime;
            if (ability_grenade_timer <= 0)
            {
                ability_grenade_timer = 0;
            }
        }
    }

}
