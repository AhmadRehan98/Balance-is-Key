using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class LevelLayoutGenerator : MonoBehaviour
{
    public static int level = 1;
    public int delta = 3; // level+delta= # of obstacles
    public GameObject[] obstacles;
    public GameObject start_pad; //don't think we need this.

    public GameObject end_pad;
    public GameObject level_geometry;
    
    // Start is called before the first frame update

    void Start()
    {
        
        foreach(Transform child in level_geometry.transform)
        {
            if (child.gameObject.tag != "start_pad")
            {
                Destroy(child.gameObject);
            }
        }


        Vector3 start_pad_position = GameObject.Find("start_pad").transform.position;
        Vector3 position_update = start_pad_position;

        GameObject temp_obstacle;

        for (int i = 0; i < level + delta + 1; i++)
        {
            if (i == 0)
            {
                position_update += new Vector3(30, 0, 0);
            }
            else if (i<level+delta)
            {
                position_update += new Vector3(105, 0, 0);
            }
            else
            {
                position_update += new Vector3(140, 0, 0);
            }

            if (i != level + delta)
            {
                temp_obstacle = obstacles[Random.Range(0, obstacles.Length)];
                Instantiate(temp_obstacle, position_update, Quaternion.identity, level_geometry.transform);
                temp_obstacle.name = "obstacle" + i.ToString();
            }
            else
            {
                temp_obstacle = end_pad;
                Instantiate(temp_obstacle, position_update, Quaternion.identity, level_geometry.transform);
                temp_obstacle.name = "end_pad";
            }

            // temp_obstacle.transform.parent = GameObject.Find("Level Geometry").transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}