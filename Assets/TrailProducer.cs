using UnityEngine;

public class TrailProducer : MonoBehaviour
{
    public GameObject trailPrefab;
    public GameObject trailsContainer;

    private GameObject currentTrail;

    public void StartProducing()
    {
        var parentTransform = this.transform;
        var parentBachBorder = GetBackBorder(this.gameObject);

        currentTrail = Object.Instantiate(trailPrefab, trailsContainer.transform);
        var currentTrailTransform = currentTrail.transform;
        currentTrailTransform.position = parentBachBorder;
        currentTrailTransform.localScale = new Vector3(1, 1, 0.0f);
    }

    private Vector3 GetBackBorder(GameObject obj)
    {
        var transform = obj.transform;
        var position = transform.position;
        var scale = transform.localScale;

        return position + (Vector3.back * scale.z / 2.0f);
    }

}