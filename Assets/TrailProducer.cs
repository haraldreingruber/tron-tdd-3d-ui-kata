using UnityEngine;

/*
 * Idea:
 * we start with a TrailInteraction
 * - knows current trail wall behind, creates it and such knows it
 * - creates new wall behind us length 0 from prefab, different game object
 *   - wall is half thickness, full height, different colour (styling, no test)
 * - when move the wall gets longer
 *   - holt distanz von seinem game object seit letztem update und update its current trail
 */
public class TrailProducer : MonoBehaviour
{
    private Vector3 _parentBackBorder;

    private GameObject currentTrail;
    public GameObject trailPrefab;
    public GameObject trailsContainer;

    public void StartProducing()
    {
        _parentBackBorder = GeometryUtils.GetBackBorder(gameObject);

        currentTrail = Instantiate(trailPrefab, trailsContainer.transform);
        var currentTrailTransform = currentTrail.transform;
        currentTrailTransform.position = _parentBackBorder;
        currentTrailTransform.localScale = new Vector3(1, 1, 0.0f);
    }

    private void Update()
    {
        if (currentTrail == null)
            return;

        var currentParentBackBorder = GeometryUtils.GetBackBorder(gameObject);
        var newLength = (currentParentBackBorder - _parentBackBorder).z;
        var currentTrailTransform = currentTrail.transform;
        currentTrailTransform.position = _parentBackBorder + Vector3.forward * (newLength / 2.0f);
        currentTrailTransform.localScale = new Vector3(1, 1, newLength);
    }
}

// TODO (clean) - check and fix all warnings
