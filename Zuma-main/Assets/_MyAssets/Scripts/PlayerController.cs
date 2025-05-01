using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Ссылка на то, что будет вращаться оружие")]
    [SerializeField] private Transform _gunPivot;
    [SerializeField] private Rigidbody2D _gunRigidbody;

    private Camera _mainCamera;
    private bool _isDefeated = false;

    private void Start()
    {
        _mainCamera = Camera.main;
    }

    private void Update()
    {
        if (_isDefeated || Time.timeScale == 0) return;

        RotateGunToCursor();
    }

    private void RotateGunToCursor()
    {
        Vector3 mousePos = Input.mousePosition;

        if (mousePos.x < 0 || mousePos.y < 0 ||
            mousePos.x > Screen.width || mousePos.y > Screen.height)
        {
            return;
        }

        Vector3 worldPos = _mainCamera.ScreenToWorldPoint(mousePos);
        worldPos.z = 0f;

        Vector3 direction = worldPos - _gunPivot.position;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        _gunPivot.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    public void Defeat()
    {
        _isDefeated = true;
        _gunPivot.rotation = Quaternion.Euler(0f, 0f, _gunPivot.rotation.eulerAngles.z);

        if (_gunRigidbody != null)
        {
            _gunRigidbody.freezeRotation = true;
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
    }
}
