using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildMenuScript : MonoBehaviour {

    public int index;
    public Toggle[] menu;
    public Toggle deleteToggle;
    public Toggle mapperToggle;
    public GameObject Builder;
    public GameObject transparentCube;
    public GameObject MapperWindow;

    public void choose()
    {
        for (int i = 0; i < menu.Length; i++)
        {
            if (menu[i].isOn)
            {
                index = i;
            }
        }
        transparentCube.GetComponent<MeshFilter>().mesh = Builder.GetComponent<BuildingScript>().blocks[index].GetComponent<MeshFilter>().sharedMesh; ;
    }

    public void MapperToggleEvent()
    {
        if (mapperToggle.isOn)
        {
            GameObject[] engines = Builder.GetComponent<BuildingScript>().findEngines();
            for (int i = 0; i < engines.Length; i++)
            {
                engines[i].transform.Find("Name").gameObject.SetActive(true);
            }
        }
        else
        {
            GameObject[] engines = Builder.GetComponent<BuildingScript>().findEngines();
            for (int i = 0; i < engines.Length; i++)
            {
                engines[i].transform.Find("Name").gameObject.SetActive(false);
            }
            MapperWindow.SetActive(false);
        }
    }
}
