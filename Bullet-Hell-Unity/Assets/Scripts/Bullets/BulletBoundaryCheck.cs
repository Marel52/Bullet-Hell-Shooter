using UnityEngine;

public class BulletBoundaryCheck : MonoBehaviour
{
    private Camera _mainCamera;
    private float _minX, _maxX, _minY, _maxY;
    private float _boundaryMargin = 1f;

    private void Start()
    {
        _mainCamera = Camera.main;
        
        if (_mainCamera == null)
        {
            Debug.LogError("No se encontró Main Camera");
            return;
        }
        
        CalculateBoundaries();
    }

    private void CalculateBoundaries()
    {
        Vector3 bottomLeft = _mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0));
        Vector3 topRight = _mainCamera.ViewportToWorldPoint(new Vector3(1, 1, 0));

        _minX = bottomLeft.x - _boundaryMargin;
        _maxX = topRight.x + _boundaryMargin;
        _minY = bottomLeft.y - _boundaryMargin;
        _maxY = topRight.y + _boundaryMargin;
    }

    private void LateUpdate()
    {
        if (IsOutOfBounds())
        {
            gameObject.SetActive(false);
        }
    }

    private bool IsOutOfBounds()
    {
        Vector3 pos = transform.position;
        return pos.x < _minX || pos.x > _maxX || pos.y < _minY || pos.y > _maxY;
    }
}