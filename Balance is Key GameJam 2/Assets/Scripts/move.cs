using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour
{
    private float _userVerticalInput, _userHorizontalInput;
    public string player;

    private float speed = 0.01f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _userVerticalInput = Input.GetAxis(player + "Vertical");
        _userHorizontalInput = Input.GetAxis(player + "Horizontal");
        Debug.Log(_userVerticalInput);
        gameObject.GetComponent<Transform>().position += transform.forward * _userVerticalInput * speed;
        gameObject.GetComponent<Transform>().position += transform.right * _userHorizontalInput * speed;
    }
}
