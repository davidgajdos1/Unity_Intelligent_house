using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class teplomer : MonoBehaviour
{
    float teplota_vonkajsia;
    // Start is called before the first frame update
    void Start()
    {
		//teplota_vonkajsia = GetRandomNumber( -30.0f, 40.0f);
		//InvokeRepeating("Update_time", 0, 1);
		teplota_vonkajsia = 0.1f;
    }
	/*
    // Update is called once per frame
    void Update_time()
    {
        eplota_vonkajsia += GetRandomNumber( -0.5f, 0.5f);

        if(teplota_vonkajsia>40.0) 
        {
            teplota_vonkajsia=40.0f;
        }
        else if (teplota_vonkajsia<-30.0f)
        {
            teplota_vonkajsia=-30.0f;
        }
        //print(teplota_vonkajsia);
    }*/

    /*public float GetRandomNumber(float minimum, float maximum)
    { 
        return UnityEngine.Random.Range(minimum, maximum);
    }*/

	public void setTemperature(float currentTemperature)
	{
		teplota_vonkajsia = currentTemperature;
	}

    public double getTemperature()
    {
        return System.Math.Round(teplota_vonkajsia,1);
    }   

}
