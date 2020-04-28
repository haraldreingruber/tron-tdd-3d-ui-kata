using UnityEngine;

public class Tron : MonoBehaviour
{
    // TODO maybe refactor to use Tron component instead of GameObject here
    public GameObject visualization;

    public RacingInteraction racingInteraction;

    public void StartRace()
    {
        racingInteraction.StartRace();
    }
}