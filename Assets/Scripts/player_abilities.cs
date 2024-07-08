using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_abilities : MonoBehaviour
{
    private float ability_grenade_timer = 0.0f;
    private float ability_grenade_cooldown = 1.0f;

    [SerializeField] private GameObject grenade_prefab;

    private void Update()
    {
        if (Input.GetButtonDown("Fire1") && ability_grenade_timer == 0)
        {
            ability_grenade_timer = ability_grenade_cooldown;

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
