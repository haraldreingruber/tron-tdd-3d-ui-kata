using System;
using UnityEngine;

public class Tron : MonoBehaviour
{
    public GameObject visualization;
    public GameObject interaction;

    public void StartRace()
    {
        this.IsRacing = true;
    }

    private bool IsRacing { get; set; }

    private void FixedUpdate()
    {
        if (IsRacing)
        {
            this.transform.position += Vector3.forward * (1.0f * Time.fixedDeltaTime);
        }
    }
}