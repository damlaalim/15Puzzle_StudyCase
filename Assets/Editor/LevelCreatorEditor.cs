using _15Puzzle.Scripts.Level;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(RandomLevelCreator))]
    public class LevelCreatorEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            var levelCreator = (RandomLevelCreator)target;

            if (GUILayout.Button("Generate Grid"))
            {
                levelCreator.GenerateGrid();
            }
        }
    }
}