using System;
using UnityEngine;
using Object = UnityEngine.Object;

public class TrailProducer : MonoBehaviour
{
    public GameObject trailPrefab;
    public GameObject trailsContainer;

    private GameObject currentTrail;
    private Vector3 _parentBackBorder;

    public void StartProducing()
    {
        _parentBackBorder = GetBackBorder(this.gameObject);

        currentTrail = Object.Instantiate(trailPrefab, trailsContainer.transform);
        var currentTrailTransform = currentTrail.transform;
        currentTrailTransform.position = _parentBackBorder;
        currentTrailTransform.localScale = new Vector3(1, 1, 0.0f);
    }

    private void Update()
    {
        if (currentTrail == null)
            return;

        var currentParentBackBorder = GetBackBorder(this.gameObject);
        var newLength = (currentParentBackBorder - _parentBackBorder).z;
        var currentTrailTransform = currentTrail.transform;
        currentTrailTransform.position = _parentBackBorder + Vector3.forward * (newLength/2.0f);
        currentTrailTransform.localScale = new Vector3(1, 1, newLength);
    }

    private Vector3 GetBackBorder(GameObject obj) // TODO: dup with Test
    {
        var transform = obj.transform;
        var position = transform.position;
        var scale = transform.localScale;

        return position + (Vector3.back * scale.z / 2.0f);
    }

}