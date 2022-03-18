using UnityEngine;

public class StatsController : MonoBehaviour
{
    public Bar HealthBar;
    public Bar ManaBar;

    // Mock values for demonstration purposes
    void Start()
    {
        HealthBar.SetMaxValue(100);
        HealthBar.SetValue(42);

        ManaBar.SetMaxValue(100);
        ManaBar.SetValue(30);
    }
}
