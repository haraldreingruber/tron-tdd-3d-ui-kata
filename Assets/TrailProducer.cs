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
        _parentBackBorder = GeometryUtils.GetBackBorder(this.gameObject);

        currentTrail = Object.Instantiate(trailPrefab, trailsContainer.transform);
        var currentTrailTransform = currentTrail.transform;
        currentTrailTransform.position = _parentBackBorder;
        currentTrailTransform.localScale = new Vector3(1, 1, 0.0f);
    }

    private void Update()
    {
        if (currentTrail == null)
            return;

        var currentParentBackBorder = GeometryUtils.GetBackBorder(this.gameObject);
        var newLength = (currentParentBackBorder - _parentBackBorder).z;
        var currentTrailTransform = currentTrail.transform;
        currentTrailTransform.position = _parentBackBorder + Vector3.forward * (newLength/2.0f);
        currentTrailTransform.localScale = new Vector3(1, 1, newLength);
    }
}