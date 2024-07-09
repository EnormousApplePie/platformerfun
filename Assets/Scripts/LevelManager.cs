using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    public static LevelManager instance;

    public float respawn_delay = 1.0f;
    [SerializeField] private GameObject player;
    [SerializeField] private Transform[] respawn_points;
    private int current_respawn_point_index = 0;

    [SerializeField] private Camera camera;
    [SerializeField] private Transform[] camera_positions;
    private int current_camera_position_index = 0;
    private Transform current_spawn_point;

    [SerializeField] private GameObject[] level_objects;
    private int current_level_index = 0;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        //upon start, set everything to the first level

        camera.transform.position = camera_positions[current_camera_position_index].position;
        current_spawn_point = respawn_points[current_respawn_point_index];
        //set the level objects to the first level
        for (int i = 0; i < level_objects.Length; i++)
        {
            if (i == current_level_index)
            {
                level_objects[i].SetActive(true);
            }
            else
            {
                level_objects[i].SetActive(false);
            }
        }

    }
    
    //-----------------------------------------------------------------------------------------------
    /// <summary>
    /// Respawns the player at the respawn point after a delay.
    /// </summary>
    public void respawn_player()
    {
        StartCoroutine(respawn_player_coroutine());
    }

    private IEnumerator respawn_player_coroutine()
    {
        yield return new WaitForSeconds(respawn_delay);
        Instantiate(player, current_spawn_point.position, Quaternion.identity);
    }
    //-----------------------------------------------------------------------------------------------
    /// <summary>
    /// Set the next level in the game.
    /// </summary>
    public void set_level(bool next_level)
    {

        int previous_level_index = current_level_index;

        // Advance the camera
        Vector3 previous_camera_position = camera.transform.position;
        if (next_level) current_camera_position_index += 1 ;
        else current_camera_position_index -= 1;
       
        if (current_camera_position_index <= camera_positions.Length - 1)
        {
            StartCoroutine(LerpCamera(previous_camera_position, camera_positions[current_camera_position_index].position, 0.5f, previous_level_index));
        }

        if (next_level) current_respawn_point_index += 1;
        else current_respawn_point_index -= 1;
        if (current_respawn_point_index <= respawn_points.Length - 1)
        {
            current_spawn_point = respawn_points[current_respawn_point_index];
        }

        if (next_level) current_level_index += 1;
        else current_level_index -= 1;
        Debug.Log(current_level_index);
        if (current_level_index <= level_objects.Length - 1)
        {
            level_objects[current_level_index].SetActive(true);
        }

    }
    //-----------------------------------------------------------------------------------------------
    /// <summary>
    /// Lerp the camera from the start position to the end position over a specified time.
    /// </summary>
    private IEnumerator LerpCamera(Vector3 start_position, Vector3 end_position, float lerp_time, int previous_level_index)
    {
        float time_elapsed = 0;
        while (time_elapsed < lerp_time)
        {
            camera.transform.position = new Vector3(Mathf.SmoothStep(start_position.x, end_position.x, time_elapsed / lerp_time), end_position.y, end_position.z);
            time_elapsed += Time.deltaTime;

            // set the previous level objects to non-active when the lerping is done
            if (time_elapsed >= lerp_time)
            {
                level_objects[previous_level_index].SetActive(false);
            }
            yield return null;
        }
    }
    //-----------------------------------------------------------------------------------------------
}
