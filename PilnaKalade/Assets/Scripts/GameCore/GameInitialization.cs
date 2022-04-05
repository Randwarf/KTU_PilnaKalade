using UnityEngine;

public class GameInitialization : MonoBehaviour
{
    void Awake() {
        CardDatabase.LoadCards();
    }
}
