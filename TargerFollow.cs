using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargerFollow : MonoBehaviour
{

    public Transform target;
    public float speed=5;

    void Update()
    {

           
       transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
        

    }
}
