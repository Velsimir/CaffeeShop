using UnityEngine;
using Zenject;

namespace Game.Scripts.UiLogic
{
    public class Billboard : MonoBehaviour
    {
        private Camera _camera;
        
        [Inject]
        private void Construct(Camera camera)
        {
            _camera = camera;
        }

        private void FixedUpdate()
        {
            RotateToCamera();
        }

        private void RotateToCamera()
        {
            transform.LookAt(_camera.transform);
            transform.Rotate(0, 180, 0);
        }
    }
}