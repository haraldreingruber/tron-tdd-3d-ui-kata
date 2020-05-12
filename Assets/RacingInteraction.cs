using UnityEngine;

public class RacingInteraction : MonoBehaviour
{
    private const float SPEED_IN_METER_PER_S = 1.0f;
    public float speedMeterPerSec = SPEED_IN_METER_PER_S;

    public virtual void StartRace()
    {
        IsRacing = true;
    }

    private bool IsRacing { get; set; }

    public virtual void TurnRight()
    {
        throw new System.NotImplementedException();
    }

    public virtual void FixedUpdate()
    {
        if (IsRacing)
        {
            transform.position += Vector3.forward * (speedMeterPerSec * Time.fixedDeltaTime);
        }
    }
}