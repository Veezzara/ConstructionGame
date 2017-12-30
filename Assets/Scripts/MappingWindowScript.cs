using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class MappingWindowScript : MonoBehaviour {

    Slider slider;
    InputField field;
    Dropdown dd;
    bool mouseOnButton;
    Text btntxt;


    void Start()
    {
        slider = transform.Find("MultiplierSlider").GetComponent<Slider>();
        field = transform.Find("InputField").GetComponent<InputField>();
        btntxt = transform.Find("Button").Find("Text").GetComponent<Text>();
        dd = transform.Find("EnginesDropdown").GetComponent<Dropdown>();
    }
    
    void Update () {
        if (mouseOnButton)
        {
            foreach (KeyCode kcode in Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(kcode))
                {
                    btntxt.text = kcode.ToString();
                }
            }
        }
    }

    public void pointerEnter()
    {
        mouseOnButton = true;
    }

    public void pointerExit()
    {
        mouseOnButton = false;
    }

    public void Save()
    {
        GameObject engine = GameObject.Find(transform.Find("Text").GetComponent<Text>().text);
        KeyMapper km = engine.GetComponent<KeyMapper>();
        //km.multiplier = slider.value;
        km.multiplier = float.Parse(field.text);
        km.keycode = (KeyCode) Enum.Parse(typeof(KeyCode), btntxt.text);
        //выбор oppositeEngine
        km.oppositeEngine = GameObject.Find(dd.options[dd.value].text);
        CloseWindow(); 
    }

    public void CloseWindow()
    {
        this.gameObject.SetActive(false);
    }

    public void SliderChange()
    {
        field.text = slider.value.ToString();
    }

    public void FieldChange()
    {
        if (field.text == "" || field.text == ".")
        {
            field.text = "0";
        }
        if (float.Parse(field.text) > 2)
        {
            field.text = "2";
        }
        if (float.Parse(field.text) < 0)
        {
            field.text = "0";
        }
        slider.value = float.Parse(field.text);
    }
}
