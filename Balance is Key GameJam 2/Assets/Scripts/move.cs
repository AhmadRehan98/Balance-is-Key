using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour
{
    private float _userVerticalInput, _userHorizontalInput;

    private float speed = 0.01f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _userVerticalInput = Input.GetAxis("Vertical");
        _userHorizontalInput = Input.GetAxis("Horizontal");
        Debug.Log(_userVerticalInput);
        gameObject.GetComponent<Transform>().position += transform.forward * _userVerticalInput * speed;
        gameObject.GetComponent<Transform>().position += transform.right * _userHorizontalInput * speed;
    }
}
