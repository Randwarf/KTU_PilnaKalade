using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {
    // Start is called before the first frame update
    void Start() {
        int[,] figure = new int[,] {
            { 1, 1, 1 },
            { 0, 1, 0 },
            { 0, 1, 0}
        };
        FigureMaker.SpawnFigure(figure, FindObjectOfType<Canvas>(), Vector2.zero);
    }

    // Update is called once per frame
    void Update() {

    }
}
