#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

namespace Utility.Editor {

    [CustomPropertyDrawer(typeof(SphericalPoint))]
    public class SphericalPointPropertyDrawer : PropertyDrawer
    {
        GUIContent[] labels = new GUIContent[] { new GUIContent("R","Radius"), new GUIContent("P","Polar"), new GUIContent("E","Elevation") };

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            SerializedProperty radius = property.FindPropertyRelative(SphericalPoint.RADIUS_PROPERTY);
            SerializedProperty polar = property.FindPropertyRelative(SphericalPoint.POLAR_PROPERTY);
            SerializedProperty elevation = property.FindPropertyRelative(SphericalPoint.ELEVATION_PROPERTY);
            float[] values = new float[] { radius.floatValue, polar.floatValue, elevation.floatValue };

            position = EditorGUI.PrefixLabel(position, label);

            EditorGUI.MultiFloatField(position, labels, values);

            radius.floatValue = values[0];
            polar.floatValue = values[1];
            elevation.floatValue = values[2];
        }

    }
}
#endif