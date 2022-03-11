using TMPro;
using UnityEngine;

public class FuelCounter : MonoBehaviour
{
    [SerializeField] private float maxFuel = 30;
    [SerializeField] private float percentToAdd = 50;
    [SerializeField] TextMeshProUGUI fuelCounter;

    public static float currentFuel;

    private void OnEnable()
    {
        EventManager.onFuelPickup += FuelPickup;   
    }
    private void OnDisable()
    {
        EventManager.onFuelPickup -= FuelPickup;
    }

    private void Start()
    {
        currentFuel = maxFuel;
    }

    private void Update()
    {
        float fuelPercentage = Mathf.FloorToInt((currentFuel * 100f) / maxFuel);

        currentFuel -= Time.deltaTime;
        if (currentFuel <= 0)
        {
            currentFuel = Mathf.Clamp(currentFuel, 0, maxFuel);
            EventManager.FuelEmpty();
        }

        fuelCounter.text = "Fuel: " + fuelPercentage.ToString() + "%";
    }

    public void AddFuel(int fuelToAdd)
    {
        currentFuel += fuelToAdd;
        currentFuel = Mathf.Clamp(currentFuel, 0, maxFuel);
    }

    public void FuelPickup()
    {
        int fuelToAdd = Mathf.FloorToInt(maxFuel / 100 * percentToAdd);
        AddFuel(fuelToAdd);
    }
}

