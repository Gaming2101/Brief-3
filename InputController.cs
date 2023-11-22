using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class InputController : MonoBehaviour
{
    public string grabKey = "space";
    public static UnityEvent grab = new UnityEvent();
    public float grabDelay = 1;
    public float lastGrabTime;

    public void Update()
    {
        if (Input.GetKeyDown(grabKey) & Time.time >= lastGrabTime + grabDelay)
        {
            grab.Invoke();
            lastGrabTime = Time.time;
        }
    }

}
