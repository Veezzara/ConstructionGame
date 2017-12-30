using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyMapper : MonoBehaviour {
    
    public KeyCode keycode;
    public GameObject oppositeEngine;
    public float speed;
    public float multiplier;

    private void Start()
    {
        oppositeEngine = null;
        keycode = KeyCode.G;
        multiplier = 1;
    }
}
