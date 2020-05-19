using UnityEngine;

public class Tron : MonoBehaviour
{
    public RacingInteraction racingInteraction;
    public TrailProducer trailProducer;

    public void StartRace()
    {
        racingInteraction.StartRace();
        trailProducer.StartProducing();
    }
}