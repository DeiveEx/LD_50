using _GAME._Scripts.LevelManagement;
using UnityEditor;
using UnityEngine;

namespace _GAME.Editor
{
    [CustomPropertyDrawer(typeof(GameScene))]
    public class GameScenePropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property,
            GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            var scenePath = property.FindPropertyRelative("path").stringValue;
            var oldScene = AssetDatabase.LoadAssetAtPath<SceneAsset>(scenePath);

            EditorGUI.BeginChangeCheck();
            var newScene = EditorGUI.ObjectField(position, label, oldScene, typeof(SceneAsset), false) as SceneAsset;
            var changed = EditorGUI.EndChangeCheck();

            if (changed)
            {
                property.FindPropertyRelative("path").stringValue = AssetDatabase.GetAssetPath(newScene);
                property.FindPropertyRelative("name").stringValue = newScene == null ? null : newScene.name;
            }

            EditorGUI.EndProperty();
        }
    }
}