using UnityEngine;

public class Tron : MonoBehaviour
{
    public GameObject visualization;
    // public GameObject interaction;
    public RacingInteraction racingInteraction;

    public void StartRace()
    {
        racingInteraction.StartRace();
    }
}