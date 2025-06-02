using UnityEngine;

namespace Game.Scripts
{
    public class CursorHider : MonoBehaviour
    {
        private void Awake()
        {
            Cursor.visible = false;
        }
    }
}