using EasyTextEffects;
using TMPro;
using UnityEngine;

namespace Game.Scripts.UiLogic
{
    public class TextChanger : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;
        [SerializeField] private TextEffect _effect;

        public void OnDisable()
        {
            _text.text = "I WANT MORE COFFEE!!!";
            _effect.enabled = true;
        }
    }
}