using UnityEngine;

public class RacingInteraction : MonoBehaviour
{
    private const float SPEED_IN_METER_PER_S = 1.0f;

    public void StartRace()
    {
        IsRacing = true;
    }

    private bool IsRacing { get; set; }

    public void FixedUpdate()
    {
        if (IsRacing)
        {
            transform.position += Vector3.forward * (SPEED_IN_METER_PER_S * Time.fixedDeltaTime);
        }
    }
}