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
    
    public void OnMove(InputValue value)
    {
        // Read value from control. The type depends on what type of controls.
        // the action is bound to.
        var v = value.Get<Vector2>();

        // IMPORTANT: The given InputValue is only valid for the duration of the callback.
        //            Storing the InputValue references somewhere and calling Get<T>()
        //            later does not work correctly.
    }
}