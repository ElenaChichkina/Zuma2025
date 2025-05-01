using UnityEngine;

public class Bullet : MonoBehaviour
{
    private SpriteRenderer _renderer;
    private Rigidbody2D _rigidbody2D;
    private bool hitFlag;

    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!hitFlag)
        {
            var hittedBall = other.gameObject.GetComponent<Ball>();
            if (hittedBall)
            {
                hitFlag = true;
                GameManager.Instance.MoveBallsScript.AddNewBallOnHit(hittedBall, _renderer.sprite);
                Destroy(gameObject);
            }
        }
    }

    public void SetSprite(Sprite sprite)
    {
        _renderer.sprite = sprite;
    }

    public void Fire()
    {
        transform.SetParent(null);

        Vector3 target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        target.z = 0f;

        Vector2 direction = (target - transform.position).normalized;
        _rigidbody2D.AddForce(direction * GameManager.Instance.BulletSpeed);
    }
}