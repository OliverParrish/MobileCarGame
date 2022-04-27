using System;
using UnityEngine;

public static class EventManager
{ 

    public static event Action onFuelEmpty;
    public static void FuelEmpty() { onFuelEmpty?.Invoke(); }

    public static event Action onFuelPickup;
    public static void FuelPickup() { onFuelPickup?.Invoke(); }

    public static event Action showGameOverMenu;
    public static void ShowGameOverMenu() { showGameOverMenu?.Invoke(); }

    public static event Action showAdOnRestart;
    public static void ShowAdOnRestart() { showAdOnRestart?.Invoke(); }

    public static event Action onContinue;
    public static void Continue() { onContinue?.Invoke(); }


}
