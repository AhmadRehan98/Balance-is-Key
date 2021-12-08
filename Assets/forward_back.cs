using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class forward_back : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject rock;
    public int pushpeed = 10;
    private Vector3 direction;
    private int upperlimit = 399;
    private int lowerlimit = 386;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (rock.transform.localPosition.x > upperlimit)
        {
            direction = new Vector3(-1,0,-1);
        }
        else if (rock.transform.localPosition.x < lowerlimit)
        {
            direction= new Vector3(1,0,1);
        }
        rock.transform.Translate(direction*Time.deltaTime*pushpeed);

        }
}
