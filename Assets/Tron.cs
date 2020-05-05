﻿using UnityEngine;

public class Tron : MonoBehaviour
{
    // TODO (Unity design) - refactor to use Tron component instead of GameObject here
    public GameObject visualization;

    public RacingInteraction racingInteraction;
    public TrailProducer trailProducer;

    public void StartRace()
    {
        racingInteraction.StartRace();
        trailProducer.StartProducing();
    }
}