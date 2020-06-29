using UnityEngine;
using UnityEngine.InputSystem;

public class Tron : MonoBehaviour
{
    public RacingInteraction racingInteraction;
    public TrailProducer trailProducer;

    public void StartRace()
    {
        racingInteraction.StartRace();
        trailProducer.StartProducing();
    }
    
    public void OnMove(InputAction.CallbackContext context)
    {
        var m_Move = context.ReadValue<Vector2>();
        Debug.Log(m_Move);
        racingInteraction.TurnRight();
    }

}