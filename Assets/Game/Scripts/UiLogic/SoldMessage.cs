using DG.Tweening;
using UnityEngine;

namespace Game.Scripts.UiLogic
{
    public class SoldMessage : MonoBehaviour
    {
        [SerializeField] private float _jumpHeight = 0.5f;
        [SerializeField] private float _jumpDuration = 0.2f;
        [SerializeField] private float _fallDuration = 0.4f;
        [SerializeField] private float _fadeDuration = 0.3f;
        [SerializeField] private float _delayBeforeReset = 0.2f;
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private AudioSource _audioSource;

        private Tween _currentTween;
        
        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();

            if (_canvasGroup == null)
            {
                _canvasGroup = gameObject.AddComponent<CanvasGroup>();
            }

            _canvasGroup.alpha = 0;
        }

        private void OnEnable()
        {
            CoffeeAcceptor.CoffeeSold += Replace;
        }

        private void OnDisable()
        {
            CoffeeAcceptor.CoffeeSold -= Replace;
        }

        private void Replace(Vector3 newPosition)
        {
            transform.position = newPosition;
            Play(newPosition);
        }

        private void Play(Vector3 newPosition)
        {
            _currentTween?.Kill();

            transform.localPosition = newPosition;
            _canvasGroup.alpha = 1;

            Sequence seq = DOTween.Sequence();

            Vector3 upPos = newPosition + Vector3.up * _jumpHeight;

            seq.Append(transform.DOLocalMove(upPos, _jumpDuration).SetEase(Ease.OutQuad))
                .Append(transform.DOLocalMove(newPosition, _fallDuration).SetEase(Ease.InQuad))
                .Join(_canvasGroup.DOFade(0, _fadeDuration).SetEase(Ease.InQuad))
                .AppendInterval(_delayBeforeReset)
                .OnComplete(() =>
                {
                    // Готов к следующему показу
                    transform.localPosition = newPosition;
                    _canvasGroup.alpha = 0;
                });

            _currentTween = seq;
            
            _audioSource.Play();
        }
    }
}