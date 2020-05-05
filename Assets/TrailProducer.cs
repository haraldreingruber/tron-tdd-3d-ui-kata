using UnityEngine;

public class TrailProducer : MonoBehaviour
{
    public GameObject trailPrefab;

    public void StartProducing()
    {
        Object.Instantiate(trailPrefab);
    }
}