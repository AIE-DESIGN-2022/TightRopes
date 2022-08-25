﻿using UnityEngine;
using System.Collections.Generic;
using System.Collections;

// Written by Steve Streeting 2017
// License: CC0 Public Domain http://creativecommons.org/publicdomain/zero/1.0/

/// <summary>
/// Component which will flicker a linked light while active by changing its
/// intensity between the min and max values given. The flickering can be
/// sharp or smoothed depending on the value of the smoothing parameter.
///
/// Just activate / deactivate this component as usual to pause / resume flicker
/// </summary>
public class LightFlickerEffect : MonoBehaviour
{
    Battery battery;
    [Tooltip("External light to flicker; you can leave this null if you attach script to a light")]
    public new Light[] lights;
    public List<float> intensities;
    [Tooltip("Minimum random light intensity")]
    public float minIntensity = 0f;
    [Tooltip("Maximum random light intensity")]
    public float maxIntensity = 1f;
    [Tooltip("How much to smooth out the randomness; lower values = sparks, higher = lantern")]
    [Range(1, 50)]
    public int smoothing = 5;

    // Continuous average calculation via FIFO queue
    // Saves us iterating every time we update, we just change by the delta
    Queue<float> smoothQueue;
    float lastSum = 0;

    public bool running;
    /// <summary>
    /// Reset the randomness and start again. You usually don't need to call
    /// this, deactivating/reactivating is usually fine but if you want a strict
    /// restart you can do.
    /// </summary>
    public void Reset()
    {
        smoothQueue.Clear();
        lastSum = 0;
    }

    void Start()
    {
        smoothQueue = new Queue<float>(smoothing);
        for(int i = 0; i< lights.Length; i++)
        {
            intensities.Add(lights[i].intensity);
        }
        battery = FindObjectOfType<Battery>();
    }

    void Update()
    {
        //Chance stuff to start
        if ((battery.flashlightBatteryCharge <= battery.maxBatteryCharge/2) && (battery.flashlightBatteryCharge > battery.maxBatteryCharge / 4))
        {
            running = true;
            //Chance to stop
            if (running)
            {
                while (smoothQueue.Count >= smoothing)
                {
                    lastSum -= smoothQueue.Dequeue();
                }

                // Generate random new item, calculate new average
                float newVal = Random.Range(minIntensity, maxIntensity);
                smoothQueue.Enqueue(newVal);
                lastSum += newVal;

                // Calculate new smoothed average
                foreach (Light light in lights)
                {
                    light.intensity = lastSum / (float)smoothQueue.Count;
                }
            }

        }
        else if((battery.flashlightBatteryCharge <= battery.maxBatteryCharge / 4))
        {
            if (running)
            {
                while (smoothQueue.Count >= smoothing)
                {
                    lastSum -= smoothQueue.Dequeue();
                }

                // Generate random new item, calculate new average
                float newVal = Random.Range(0, minIntensity);
                smoothQueue.Enqueue(newVal);
                lastSum += newVal;

                // Calculate new smoothed average
                foreach (Light light in lights)
                {
                    light.intensity = lastSum / (float)smoothQueue.Count;
                }
                if((battery.flashlightBatteryCharge <= battery.maxBatteryCharge / 5))
                {
                    float random = Random.Range(0, 10);
                    if (random == 10)
                    {
                        battery.flashlightBatteryCharge = 0;
                        Debug.Log(random);
                    }
                }
            }
        }
        else
        {
            StopFlicker();
        }
       
    } 
    void StopFlicker()
    {
        running = false;
        for (int i = 0; i < lights.Length; i++)
        {
            lights[i].intensity = intensities[i];
        }
    }
}