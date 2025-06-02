using TMPro;
using UnityEngine;

namespace Game.Scripts
{
    public class FPSCounter : MonoBehaviour
    {
        [SerializeField] private TMP_Text _fpsText;

        private float _deltaTime;
        private float _totalTime;
        private int _frameCount;
        private float _minFPS = float.MaxValue;
        private float _maxFPS = float.MinValue;

        void Update()
        {
            float currentFrameTime = Time.unscaledDeltaTime;
            float fps = 1.0f / currentFrameTime;

            _deltaTime += (currentFrameTime - _deltaTime) * 0.1f;
            _totalTime += currentFrameTime;
            _frameCount++;

            _minFPS = Mathf.Min(_minFPS, fps);
            _maxFPS = Mathf.Max(_maxFPS, fps);

            float averageFPS = _frameCount / _totalTime;

            _fpsText.text = string.Format(
                "FPS: {0:0} (avg: {1:0}, min: {2:0}, max: {3:0})",
                fps, averageFPS, _minFPS, _maxFPS
            );
        }

        public void ResetStats()
        {
            _totalTime = 0f;
            _frameCount = 0;
            _minFPS = float.MaxValue;
            _maxFPS = float.MinValue;
        }
    }
}