using Assets.Scripts.Settings;
using UnityEngine;

public class GameInitialization : MonoBehaviour
{
    void Awake() {
        CardDatabase.LoadCards();
        SettingsController.LoadSettings();
        EnemyDatabase.LoadEnemies();
    }
}
