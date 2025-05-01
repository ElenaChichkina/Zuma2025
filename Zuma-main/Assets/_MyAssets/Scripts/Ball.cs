using UnityEngine;

public class Ball : MonoBehaviour
{
    private SpriteRenderer _renderer;
    private float _distance;

    [SerializeField] private GameObject destroyEffectPrefab;

    public Sprite Sprite
    {
        get => _renderer.sprite;
        set => _renderer.sprite = value;
    }

    public float Distance
    {
        get => _distance;
        set
        {
            _distance = value > 0 ? value : 0;
            transform.position = GameManager.Instance.BgMath.CalcPositionByDistance(_distance);
            
            if (_distance == 0)
            {
                DisableBall();
            }
            else if (!gameObject.activeSelf)
            {
                gameObject.SetActive(true);
            }
        }
    }

    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
    }
    
    public void DestroyBall()
    {
        Debug.Log("DestroyBall() called on: " + gameObject.name);

        AudioManager.Instance.PlayEffect(0);

        PlayDestroyEffect();
    
        Destroy(gameObject);
    }


    private void DisableBall()
    {
        gameObject.SetActive(false);
    }

    private void PlayDestroyEffect()
    {
        if (destroyEffectPrefab)
        {
            Instantiate(destroyEffectPrefab, transform.position, Quaternion.identity);
        }
    }
}