using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float _normalSpeed = 5f;
    [SerializeField] private float _slowSpeed = 2f;

    [Header("Boundary Settings")]
    [SerializeField] private float _boundaryMargin = 0.5f;

    private float _currentSpeed;
    private Camera _mainCamera;
    private float _minX, _maxX, _minY, _maxY;

    private void Start()
    {
        _currentSpeed = _normalSpeed;
        _mainCamera = Camera.main;
        
        if (_mainCamera == null)
        {
            Debug.LogError("No se encontró Main Camera. Asegúrate de que tu cámara tenga el tag 'MainCamera'");
            return;
        }
        
        CalculateBoundaries();
    }

    private void CalculateBoundaries()
    {
        Vector3 bottomLeft = _mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0));
        Vector3 topRight = _mainCamera.ViewportToWorldPoint(new Vector3(1, 1, 0));

        _minX = bottomLeft.x + _boundaryMargin;
        _maxX = topRight.x - _boundaryMargin;
        _minY = bottomLeft.y + _boundaryMargin;
        _maxY = topRight.y - _boundaryMargin;

        Debug.Log($"Límites calculados - X: [{_minX}, {_maxX}] Y: [{_minY}, {_maxY}]");
    }

    private void Update()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        // Detectar si se mantiene presionada la tecla Shift para movimiento lento
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            _currentSpeed = _slowSpeed;
        }
        else
        {
            _currentSpeed = _normalSpeed;
        }

        // Obtener input usando los ejes configurados en Input Manager
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // DEBUG: Descomentar estas líneas si quieres ver los valores en consola
        // if (horizontal != 0 || vertical != 0)
        // {
        //     Debug.Log($"Input detectado - H: {horizontal}, V: {vertical}");
        // }

        // Calcular dirección de movimiento
        Vector2 movement = new Vector2(horizontal, vertical).normalized;

        // Aplicar movimiento
        Vector3 newPosition = transform.position + (Vector3)movement * _currentSpeed * Time.deltaTime;

        // Limitar la posición dentro de los límites
        newPosition.x = Mathf.Clamp(newPosition.x, _minX, _maxX);
        newPosition.y = Mathf.Clamp(newPosition.y, _minY, _maxY);

        transform.position = newPosition;
    }
}