// ------------------------
// Onur Ereren - May 2023
// ------------------------

// Editor script that simplifies InputReceiver in the inspector
// Displays relevant fields and hides others

using UnityEditor;

namespace WixotCase.PlayerInput
{
    [CustomEditor(typeof(InputReceiver))]
    public class InputReceiverEditor : Editor
    {
        private SerializedProperty _swipeDetectionModeProperty;
        private SerializedProperty _swipeDetectionPercentageProperty;
        private SerializedProperty _exactPhysicalDistanceProperty;
        private SerializedProperty _swipeDetectionDeltaProperty;
        private SerializedProperty _swipeDetectionTimeProperty;
        private SerializedProperty _tapDetectionTimeProperty;

        private void OnEnable()
        {
            _swipeDetectionModeProperty = serializedObject.FindProperty("_swipeDetectionMode");
            _swipeDetectionPercentageProperty = serializedObject.FindProperty("_swipeDetectionPercentage");
            _exactPhysicalDistanceProperty = serializedObject.FindProperty("_exactPhysicalDistance");
            _swipeDetectionDeltaProperty = serializedObject.FindProperty("_swipeDetectionDelta");
            _swipeDetectionTimeProperty = serializedObject.FindProperty("_swipeDetectionTime");
            _tapDetectionTimeProperty = serializedObject.FindProperty("_tapDetectionTime");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.LabelField("DETECTION TIMES", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(_swipeDetectionTimeProperty);
            EditorGUILayout.PropertyField(_tapDetectionTimeProperty);

            EditorGUILayout.Space(10);

            EditorGUILayout.LabelField("DETECTION MODE", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(_swipeDetectionModeProperty);

            InputReceiver.SwipeDetectionMode swipeDetectionMode = (InputReceiver.SwipeDetectionMode)_swipeDetectionModeProperty.enumValueIndex;

            switch (swipeDetectionMode)
            {
                case InputReceiver.SwipeDetectionMode.PhysicalDistance:
                    EditorGUILayout.PropertyField(_exactPhysicalDistanceProperty);
                    break;
                case InputReceiver.SwipeDetectionMode.ScreenRatio:
                    EditorGUILayout.PropertyField(_swipeDetectionPercentageProperty);
                    break;
                case InputReceiver.SwipeDetectionMode.ExactPixels:
                    EditorGUILayout.PropertyField(_swipeDetectionDeltaProperty);
                    break;
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}