using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    Transform player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        float newZ = player.position.z + transform.localScale.z / 2.1f;
     transform.position = new Vector3 (transform.position.x, transform.position.y, newZ); 
    }
}
