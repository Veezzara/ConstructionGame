using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingScript : MonoBehaviour {

    Ray ray;
    RaycastHit hit;
    GameObject[] engines;
    Rigidbody rb;
    int enginesCount;
    public BuildMenuScript bms;
    public GameObject transparentCube;
    public GameObject[] blocks;
    public GameObject construction;
    public bool playmode;
    public GameObject BuildingUI;
    public GameObject MapperWindow;

    void Start()
    {
        playmode = false;
        enginesCount = 0;
    }

    void Update()
    {
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
        if (Input.GetKeyDown("space"))
        {
            if (!playmode)
            {
                BuildingUI.SetActive(false);
                playmode = true;
                GameObject[] objects = GameObject.FindGameObjectsWithTag("Block");
                for (int i = 0; i < objects.Length; i++)
                {
                    objects[i].GetComponent<BoxCollider>().enabled = false;
                    MeshCollider mc = objects[i].AddComponent<MeshCollider>();
                    mc.convex = true;
                }
                rb = construction.AddComponent<Rigidbody>();
                rb.mass = GameObject.FindGameObjectsWithTag("Block").Length + 1;
                rb.useGravity = false;
                engines = findEngines();
                for (int i = 0; i < engines.Length; i++)
                {
                    engines[i].AddComponent<EngineScript>();
                }
            } else
            {
                BuildingUI.SetActive(true);
                playmode = false;
                GameObject[] objects = GameObject.FindGameObjectsWithTag("Block");
                for (int i = 0; i < objects.Length; i++)
                {
                    objects[i].GetComponent<BoxCollider>().enabled = true;
                    Destroy(objects[i].GetComponent<MeshCollider>());
                }
                Destroy(rb);
                construction.transform.position = new Vector3();
                construction.transform.rotation = new Quaternion();
                engines = findEngines();
                for (int i = 0; i < engines.Length; i++)
                {
                    Destroy(engines[i].GetComponent<EngineScript>());
                }
            }
        }
        if (!playmode)
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit) && !bms.deleteToggle.isOn && !bms.mapperToggle.isOn)
            {
                transparentCube.SetActive(true);
                transparentCube.transform.position = newCoords(hit);
                //это можно вообще убрать
                //для двигателя и кабины добавить проверку на препятствие
                if (bms.index == 1)
                {
                    transparentCube.transform.rotation = Quaternion.LookRotation((hit.collider.transform.position - transparentCube.transform.position).normalized);
                }
                else
                {
                    if (Input.GetKeyDown("r"))
                    {
                        transparentCube.transform.Rotate(0, 90, 0);
                    }
                    if (Input.GetKeyDown("t"))
                    {
                        transparentCube.transform.Rotate(90, 0, 0);
                    }
                }
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    GameObject newOb = Instantiate(blocks[bms.index], transparentCube.transform.position, transparentCube.transform.rotation);
                    newOb.transform.SetParent(construction.transform);
                    if (bms.index == 1)
                    {
                        newOb.name = "Engine " + enginesCount;
                        ++enginesCount;
                    }
                }
            }
            else
            {
                transparentCube.SetActive(false);
            }
            if (Physics.Raycast(ray, out hit) && bms.deleteToggle.isOn && !bms.mapperToggle.isOn)
            {
                if (Input.GetKeyDown(KeyCode.Mouse0) && hit.collider.CompareTag("Block"))
                {
                    if (hit.collider.name == "col")
                    {
                        Destroy(hit.collider.transform.parent.gameObject, 0f);
                    }
                    else
                    {
                        Destroy(hit.collider.gameObject, 0f);
                    }
                }
            }
            if (Physics.Raycast(ray, out hit) && !bms.deleteToggle.isOn && bms.mapperToggle.isOn)
            {
                if (Input.GetKeyDown(KeyCode.Mouse0) && hit.collider.name.Contains("Engine "))
                {
                    //нажатие на col - косяк, нужно добавить еще один if с hit.collider.transform.parent.contains
                    MapperWindowOpen(hit);
                }
            }
        }
    }

    void MapperWindowOpen(RaycastHit hit)
    {
        MapperWindow.SetActive(true);
        MapperWindow.transform.Find("Text").GetComponent<Text>().text = hit.collider.name;
        Dropdown dd = MapperWindow.transform.Find("EnginesDropdown").GetComponent<Dropdown>();
        dd.ClearOptions();
        GameObject[] allengines = findEngines();
        List<string> options = new List<string>();
        options.Add("none");
        for (int i = 0; i < allengines.Length; i++)
        {
            if (allengines[i].name != hit.collider.name)
            {
                options.Add(allengines[i].name);
            }
        }
        dd.AddOptions(options);
        if (hit.collider.GetComponent<KeyMapper>().oppositeEngine == null)
        {
            dd.value = 0;
        }
        else
        {
            dd.value = options.IndexOf(hit.collider.GetComponent<KeyMapper>().oppositeEngine.name);
        }
        Text btn = MapperWindow.transform.Find("Button").Find("Text").GetComponent<Text>();
        btn.text = hit.collider.GetComponent<KeyMapper>().keycode.ToString();
        MapperWindow.transform.Find("MultiplierSlider").GetComponent<Slider>().value = hit.collider.GetComponent<KeyMapper>().multiplier;
    }

    Vector3 newCoords(RaycastHit hit)
    {
        Vector3 n = hit.point - hit.collider.transform.position;
        float x = n.x * 2 - 2 * n.x % 1;
        float y = n.y * 2 - 2 * n.y % 1;
        float z = n.z * 2 - 2 * n.z % 1;
        return new Vector3(x, y, z) + hit.collider.transform.position;
    }

    public GameObject[] findEngines()
    {
        GameObject[] blocks = GameObject.FindGameObjectsWithTag("Block");
        int length = 0;
        for (int i = 0; i < blocks.Length; i++)
        {
            if (blocks[i].name.Contains("Engine "))
            {
                ++length;
            }
        }
        GameObject[] engines = new GameObject[length];
        int cur = 0;
        for (int i = 0; i < blocks.Length; i++)
        {
            if (blocks[i].name.Contains("Engine "))
            {
                engines[cur] = blocks[i];
                ++cur;
            }
        }
        return engines;
    }
}
