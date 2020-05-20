using UnityEngine;

// ReSharper disable CommentTypo
/*
 * Idea:
 * we start with a TrailInteraction
 * - knows current trail wall behind, creates it and such knows it
 * - creates new wall behind us length 0 from prefab, different game object
 *   - wall is half thickness, full height, different colour (styling, no test)
 * - when move the wall gets longer
 *   - holt distanz von seinem game object seit letztem update und update its current trail
 */
// ReSharper restore CommentTypo
public class TrailProducer : MonoBehaviour
{
    private Vector3 _parentBackBorder;

    private GameObject _currentTrail;
    public GameObject trailPrefab;
    public GameObject trailsContainer;

    public void StartProducing()
    {
        _parentBackBorder = GeometryUtils.GetBackBorder(gameObject);

        _currentTrail = Instantiate(trailPrefab, trailsContainer.transform);
        var currentTrailTransform = _currentTrail.transform;
        currentTrailTransform.position = _parentBackBorder;

        var parentScale = transform.localScale;
        currentTrailTransform.localScale = new Vector3(parentScale.x / 2.0f, parentScale.y, 0.0f);
    }

    private void Update()
    {
        if (!_currentTrail)
        {
            return;
        }

        var currentParentBackBorder = GeometryUtils.GetBackBorder(gameObject);
        var newLength = (currentParentBackBorder - _parentBackBorder).z;
        var currentTrailTransform = _currentTrail.transform;
        currentTrailTransform.position = _parentBackBorder + Vector3.forward * (newLength / 2.0f);
        var localScale = currentTrailTransform.localScale;
        localScale.z = newLength;
        currentTrailTransform.localScale = localScale;
    }
}