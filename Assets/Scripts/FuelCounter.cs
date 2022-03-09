using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelCounter : MonoBehaviour
{
    [SerializeField] private float maxFuel = 30;

    private float currentFuel;

    private void Start()
    {
        currentFuel = maxFuel;
    }

    private void Update()
    {
        currentFuel -= Time.deltaTime;
        if (currentFuel <= 0)
        {
            currentFuel = Mathf.Clamp(currentFuel, 0, maxFuel);
          
        }
    }

    public void AddFuel(float amount)
    {
        currentFuel += amount;
        currentFuel = Mathf.Clamp(currentFuel, 0, maxFuel);
    }
}

