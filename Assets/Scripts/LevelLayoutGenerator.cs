using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using Random = UnityEngine.Random;

public class LevelLayoutGenerator : MonoBehaviour
{
    public int delta = 2; // level+delta= # of obstacles

    public Transform playerSetupObject;

    public GameObject[] obstacles;
    public int[] difficulty;
    public GameObject start_pad; //don't think we need this.

    public GameObject end_pad;
    public GameObject level_geometry;
    private Vector3 forward_increment = new Vector3(0, 0, 105);
    private Vector3 forward_increment_curly_turn = new Vector3(0, 0, 43);
    private Vector3 start_pad_increment = new Vector3(0, 0, 30);
    private Vector3 forward_end_pad_increment = new Vector3(0, 0, 140);
    private Vector3 joint_increment = new Vector3(0, 0, 1078 - 945);
    private Vector3 curly_increment = new Vector3(105, 0, 0);
    private Vector3 curly_end_pad_increment = new Vector3(140, 0, 0);
    private Vector3 joint1_increment = new Vector3(-175, 0, -499);
    public GameObject joint, joint1;

    private Boolean isForward = true;

    private Boolean isTurn = false;
    // Start is called before the first frame update

    private int numberOfPrefabs()
    {
        return (StaticClass.levelsCompleted + 1) * delta; // TODO: change this
    }

    void Start()
    {
        if (playerSetupObject == null)
        {
            Debug.LogError("no player setup");
        }

        foreach (Transform child in level_geometry.transform)
        {
            if (child.gameObject.tag != "start_pad")
            {
                Destroy(child.gameObject);
            }
        }

        Vector3 start_pad_position = GameObject.Find("start_pad").transform.position;
        playerSetupObject.transform.position = GameObject.Find("start_pad").transform.Find("start_floor").position +
                                               new Vector3(0, 1, 0);
        Vector3 latest_forward = start_pad_position;
        Vector3 latest_curly = start_pad_position;
        GameObject temp_obstacle, joint_clone, clone;
        int obstacleIndex;

        for (int i = 0; i < numberOfPrefabs() + 1; i++)
        {
            if (i == 0)
            {
                do
                {
                    obstacleIndex = Random.Range(0, obstacles.Length);
                //} while (obstacleIndex != 11);
                } while (difficulty[obstacleIndex] > StaticClass.levelsCompleted);

                temp_obstacle = obstacles[obstacleIndex];
                clone = Instantiate(temp_obstacle, latest_forward + start_pad_increment, Quaternion.identity,
                    level_geometry.transform);
                latest_forward += start_pad_increment;
                clone.name = "obstacle" + i.ToString();
            }
            else if (i < numberOfPrefabs())
            {
                do
                {
                    obstacleIndex = Random.Range(0, obstacles.Length);
                //} while (obstacleIndex != 11);
                } while (difficulty[obstacleIndex] > StaticClass.levelsCompleted);

                temp_obstacle = obstacles[obstacleIndex];
                if (isForward)
                {
                    if (isTurn)
                    {
                        joint_clone = Instantiate(joint, latest_forward + joint_increment, Quaternion.identity,
                            level_geometry.transform);
                        clone = Instantiate(temp_obstacle, latest_forward + forward_increment, Quaternion.identity,
                            level_geometry.transform);
                        clone.transform.RotateAround(clone.transform.Find("rotate_right").position, Vector3.up, 90);
                        latest_curly = clone.transform.position;
                        isForward = (!isForward);
                        joint_clone.name = "joint-" + (i - 1).ToString() + "-" + i.ToString();
                    }
                    else
                    {
                        clone = Instantiate(temp_obstacle, latest_forward + forward_increment, Quaternion.identity,
                            level_geometry.transform);
                        latest_forward += forward_increment;
                    }
                }
                else
                {
                    if (isTurn)
                    {
                        joint_clone = Instantiate(joint1, latest_curly + joint1_increment, Quaternion.identity,
                            level_geometry.transform);
                        latest_forward = latest_curly + joint1_increment;
                        clone = Instantiate(temp_obstacle, latest_forward + forward_increment_curly_turn,
                            Quaternion.identity,
                            level_geometry.transform);
                        latest_forward += forward_increment_curly_turn;
                        isForward = (!isForward);
                        joint_clone.name = "joint-" + (i - 1).ToString() + "-" + i.ToString();
                    }
                    else
                    {
                        clone = Instantiate(temp_obstacle, latest_forward + forward_increment, Quaternion.identity,
                            level_geometry.transform);
                        clone.transform.RotateAround(clone.transform.Find("rotate_right").position, Vector3.up, 90);
                        clone.transform.position = latest_curly + curly_increment;
                        latest_curly += curly_increment;
                    }
                }

                clone.name = "obstacle" + i.ToString();
            }
            else
            {
                if (isForward)
                {
                    clone = Instantiate(end_pad, latest_forward + forward_end_pad_increment, Quaternion.identity,
                        level_geometry.transform);
                }
                else
                {
                    clone = Instantiate(end_pad, latest_forward + forward_increment, Quaternion.identity,
                        level_geometry.transform);
                    clone.transform.RotateAround(clone.transform.Find("rotate_right").position, Vector3.up, 90);
                    clone.transform.position = latest_curly + curly_end_pad_increment;
                }

                clone.name = "end_pad";
            }

            isTurn = (!isTurn);

            // temp_obstacle.transform.parent = GameObject.Find("Level Geometry").transform;
        }
    }

    /*
     void Start1()
    {
        
        foreach(Transform child in level_geometry.transform)
        {
            if (child.gameObject.tag != "start_pad")
            {
                Destroy(child.gameObject);
            }
        }

        Vector3 temp_increase=forward_increment;

        Vector3 start_pad_position = GameObject.Find("start_pad").transform.position;
        Vector3 position_update = start_pad_position;

        GameObject temp_obstacle;

        for (int i = 0; i < level + delta + 1; i++)
        {
            if (i == 0)
            {
                temp_obstacle = obstacles[Random.Range(0, obstacles.Length)];
                Instantiate(temp_obstacle, position_update+start_pad_increment, Quaternion.identity, level_geometry.transform);
                position_update += start_pad_increment;
            }
            else if (i < level + delta)
            {

                temp_obstacle = obstacles[Random.Range(0, obstacles.Length)];
                if (isForward)
                {
                    temp_increase = forward_increment;
                }
                else
                {
                    temp_increase = curly_increment;
                }

                GameObject clone = Instantiate(temp_obstacle, position_update + temp_increase, Quaternion.identity,
                    level_geometry.transform);
                GameObject joint_clone;
                if (isTurn)
                {
                    if (isForward)
                    {
                        clone.transform.RotateAround(clone.transform.Find("rotate_right").position, Vector3.up, 90);
                        joint_clone=Instantiate(joint, position_update + joint_increment, Quaternion.identity,
                            level_geometry.transform);
                    }
                    else
                    {
                        clone.transform.RotateAround(clone.transform.Find("rotate_left").position, Vector3.up, 90);
                        joint_clone=Instantiate(joint1, position_update + joint1_increment, Quaternion.identity,
                            level_geometry.transform);
                    }

                    isForward = (!isForward);

                    joint_clone.name = "joint-" + (i - 1).ToString() + "-" + i.ToString();
                    position_update = clone.transform.position;
                }
                else
                {
                    position_update = position_update + temp_increase;
                }
                clone.name="obstacle" + i.ToString();
            }
            else
            {
                Vector3 temp_delta;
                if (isForward)
                {
                    temp_delta = forward_end_pad_increment;
                }
                else
                {
                    temp_delta = curly_end_pad_increment;
                }
                GameObject end_pad_clone= Instantiate(end_pad, position_update+temp_delta, Quaternion.identity, level_geometry.transform);
                end_pad_clone.name = "end_pad";
            }

            isTurn = (!isTurn);
            // temp_obstacle.transform.parent = GameObject.Find("Level Geometry").transform;
        }
    }
     */

    // Update is called once per frame
    void Update()
    {
    }
}