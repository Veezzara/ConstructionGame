using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EngineScript : MonoBehaviour {
    
    public KeyCode keycode;
    public float speed;
    GameObject oppositeEngine;
    KeyCode opkeycode;
    Vector3 direction;
    Rigidbody rb;
    ParticleSystem ps;
    Vector3 prevpos;

    void Start()
    {
        oppositeEngine = GetComponent<KeyMapper>().oppositeEngine;
        if (oppositeEngine != null)
        {
            opkeycode = oppositeEngine.GetComponent<KeyMapper>().keycode;
        }
        ps = transform.Find("Trail").GetComponent<ParticleSystem>();
        rb = transform.parent.GetComponent<Rigidbody>();
        keycode = GetComponent<KeyMapper>().keycode;
        speed = GetComponent<KeyMapper>().speed * GetComponent<KeyMapper>().multiplier;
        prevpos = transform.position;
        ps.startLifetime = GetComponent<KeyMapper>().multiplier;
    }

    void FixedUpdate () {
        direction = transform.position - transform.Find("col").transform.position;
        if (Input.GetKey(keycode) && !Input.GetKey(opkeycode))
        {
            rb.AddForceAtPosition(direction * speed, transform.position, ForceMode.Force);
            ps.Play();
        }

        if (!Input.GetKey(opkeycode) && oppositeEngine)
        {
            Vector3 movement = prevpos - transform.position;
            Vector3 movementnormalized = movement.normalized;
            float mp = movementnormalized.x * direction.x + movementnormalized.y * direction.y + movementnormalized.z * direction.z;
            float angle = Mathf.Acos(mp) * Mathf.Rad2Deg;

            //Debug.Log("angle: " + angle + " Name: " + transform.name);
            Debug.Log(transform.name + ": " + movement);
            if (mp > 0)
            {
                rb.AddForceAtPosition(direction * speed, transform.position, ForceMode.Force);
                ps.Play();
            }
        }

        prevpos = transform.position;
    }
}