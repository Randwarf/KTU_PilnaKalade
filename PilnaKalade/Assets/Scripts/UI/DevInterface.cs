using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevInterface : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            TransitionController.TransitionTo(0);
        }
    }
}
