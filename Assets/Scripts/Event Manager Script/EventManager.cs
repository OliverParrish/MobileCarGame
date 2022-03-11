using System;
using UnityEngine;

public static class EventManager
{ 

    public static event Action onFuelEmpty;
    public static void FuelEmpty() { onFuelEmpty?.Invoke(); }

    public static event Action onFuelPickup;
    public static void FuelPickup() { onFuelPickup?.Invoke(); }
}
