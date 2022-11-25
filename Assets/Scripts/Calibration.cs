using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Calibration : MonoBehaviour
{

    BodyController controller;

    bool calibrated = false;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<BodyController>();        
    }

    // Update is called once per frame
    void Update()
    {

    }


}
