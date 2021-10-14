using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour
{
    private Rigidbody[] childRbs;
    private GameObject[] arms;
    private float _userVerticalInput, _userHorizontalInput;
    public string player;

    private float speed = 0.01f;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(transform.Find("Arms"));
        childRbs = GetComponentsInChildren<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        _userVerticalInput = Input.GetAxis(player + "Vertical");
        _userHorizontalInput = Input.GetAxis(player + "Horizontal");

        foreach(Rigidbody rb in childRbs)
        {
            gameObject.GetComponent<Transform>().position += transform.forward * _userVerticalInput * speed;
            gameObject.GetComponent<Transform>().position += transform.right * _userHorizontalInput * speed;
        }
        
    }
}
