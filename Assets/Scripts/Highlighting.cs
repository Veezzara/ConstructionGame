using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Highlighting : MonoBehaviour {
    
    public Material highlightMaterial;
    public bool highlighting;
    Material cubeMaterial;
    Material startMaterial;
    public Material glassMaterial;

    void Start () {
        if (transform.name == "col")
        {
            cubeMaterial = transform.parent.GetComponent<Renderer>().material;
        }
        else
        {
            cubeMaterial = GetComponent<Renderer>().material;
        }
        startMaterial = cubeMaterial;
        highlighting = false;
    }

    private void OnMouseEnter()
    {
        if (transform.Find("Glass") != null)
        {
            transform.Find("Glass").GetComponent<Renderer>().material = highlightMaterial;
        }
        if (transform.parent.Find("Glass") != null)
        {
            transform.parent.Find("Glass").GetComponent<Renderer>().material = highlightMaterial;
        }
        if (transform.name == "col")
        {
            transform.parent.GetComponent<Renderer>().material = highlightMaterial;
        } else
        {
            GetComponent<Renderer>().material = highlightMaterial;
        }
    }

    private void OnMouseExit()
    {
        if (transform.Find("Glass") != null)
        {
            transform.Find("Glass").GetComponent<Renderer>().material = glassMaterial;
        }
        if (transform.parent.Find("Glass") != null)
        {
            transform.parent.Find("Glass").GetComponent<Renderer>().material = glassMaterial;
        }
        if (transform.name == "col")
        {
            transform.parent.GetComponent<Renderer>().material = startMaterial;
        }
        else
        {
            GetComponent<Renderer>().material = startMaterial;
        }
    }
}
