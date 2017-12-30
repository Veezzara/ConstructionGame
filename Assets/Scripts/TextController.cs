using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextController : MonoBehaviour {
    
    private GameObject MainCamera;
    private TextMesh Text;
    
	void Start () {
		Text = GetComponent<TextMesh>();
        Text.text = (string)this.transform.parent.name;
        MainCamera = GameObject.Find("Main Camera");
    }
    
    //как-нибудь бы рамочку сделать
    void LateUpdate()
    {
        transform.LookAt(MainCamera.transform);
        transform.Rotate(0, 180, 0);
        Text.characterSize = MainCamera.GetComponent<CameraController>().distance / 50;
    }
}
