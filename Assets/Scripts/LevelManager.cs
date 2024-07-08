using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public float respawn_delay = 1.0f;
    [SerializeField] private GameObject player;
    [SerializeField] private Transform respawn_point;

    public void respawn_player()
    {
        StartCoroutine(respawn_player_coroutine());
    }

    private IEnumerator respawn_player_coroutine()
    {
        yield return new WaitForSeconds(respawn_delay);
        Instantiate(player, respawn_point.position, Quaternion.identity);
    }
}
