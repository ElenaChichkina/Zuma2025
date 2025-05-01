using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(RectTransform))]
public class ButtonParticleEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Настройки префаба частиц")]
    public ParticleSystem particleSystemPrefab;

    private ParticleSystem _spawnedSystem;

    private void Awake()
    {
        if (particleSystemPrefab == null)
        {
            Debug.LogError("[ButtonParticleEffect] Не назначен ParticleSystem префаб!");
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_spawnedSystem == null)
        {
            _spawnedSystem = Instantiate(particleSystemPrefab, transform);

            _spawnedSystem.transform.localPosition = Vector3.zero;
            _spawnedSystem.transform.localScale = Vector3.one;

            var mainModule = _spawnedSystem.main;
            mainModule.useUnscaledTime = true;
        }

        Debug.Log($"[ButtonParticleEffect] OnPointerEnter -> '{transform.name}' Play Particles");
        _spawnedSystem.Play(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (_spawnedSystem != null)
        {
            Debug.Log($"[ButtonParticleEffect] OnPointerExit -> '{transform.name}' Stop Particles");
            _spawnedSystem.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        }
    }
}
