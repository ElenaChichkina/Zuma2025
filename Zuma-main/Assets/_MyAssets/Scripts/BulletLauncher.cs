using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class BulletLauncher : MonoBehaviour
{
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private GameObject _muzzleFlashPrefab;

    private Bullet _bullet;

    private void Start()
    {
        InstantiateBullet();
    }

    private void Update()
    {
        if (GameManager.Instance.IsPaused())
        {
            return;
        }
        
        if (Input.GetMouseButtonDown(0) && _bullet)
        {
            if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }

            AudioManager.Instance.PlaySound(0);

            if (_muzzleFlashPrefab != null)
            {
                Instantiate(_muzzleFlashPrefab, transform.position, transform.rotation);
            }

            _bullet.Fire();
            _bullet = null;
            StartCoroutine(Reload());
        }
    }

    private IEnumerator Reload()
    {
        yield return new WaitForSeconds(GameManager.Instance.BulletLoadTime);
        InstantiateBullet();
    }

    private void InstantiateBullet()
    {
        _bullet = Instantiate(_bulletPrefab, transform).GetComponent<Bullet>();
        _bullet.SetSprite(GameManager.Instance.GetRandomSprite());
    }
}