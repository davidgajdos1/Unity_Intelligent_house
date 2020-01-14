using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class termostat : MonoBehaviour
{
    float teplota_vnutorna;
    // Start is called before the first frame update
    void Start()
    {
        teplota_vnutorna = GetRandomNumber( 15.0f, 25.0f);
        InvokeRepeating("Update_time", 0, 1);
    }

    // Update is called once per frame
    void Update_time()
    {
        teplota_vnutorna += GetRandomNumber( -0.15f, 0.15f);

        if(teplota_vnutorna<15.0) 
        {
            teplota_vnutorna=15.0f;
        }
        else if (teplota_vnutorna>25.0f)
        {
            teplota_vnutorna=25.0f;
        }
        //print(teplota_vnutorna);
    }

    public float GetRandomNumber(float minimum, float maximum)
    { 
        return UnityEngine.Random.Range(minimum, maximum);
    }

    public double getTemperature()
    {
        return System.Math.Round(teplota_vnutorna,1);
    }
}
