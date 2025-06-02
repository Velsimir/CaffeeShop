using UnityEditor;
using UnityEngine;

namespace Game.Scripts.ExtensionMethods
{
#if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(RequireInterfaceAttribute))]
    public class RequireInterfaceAttributeDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            RequireInterfaceAttribute requiredAttribute = (RequireInterfaceAttribute)attribute;
            EditorGUI.BeginProperty(position, label, property);
            property.objectReferenceValue = EditorGUI.ObjectField(
                position,
                label,
                property.objectReferenceValue,
                typeof(MonoBehaviour),
                true
            );

            if (property.objectReferenceValue != null &&
                !requiredAttribute.RequiredInterface.IsAssignableFrom(property.objectReferenceValue.GetType()))
            {
                Debug.LogError($"Assigned object must implement {requiredAttribute.RequiredInterface.Name}");
                property.objectReferenceValue = null;
            }

            EditorGUI.EndProperty();
        }    
    }
#endif
}