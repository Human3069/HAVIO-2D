using Cysharp.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace VTSModule
{
    [CustomEditor(typeof(BaseTweener), true)]
    [CanEditMultipleObjects]
    public class BaseTweenerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            BaseTweener[] owners = new BaseTweener[targets.Length];
            BaseTweener owner = target as BaseTweener;

            for (int i = 0; i < targets.Length; i++)
            {
                owners[i] = targets[i] as BaseTweener;
            }

            EditorGUILayout.BeginHorizontal();

            if (GUILayout.Button("▶ Play Start") == true)
            {
                foreach (BaseTweener iteratingOwner in owners)
                {
                    iteratingOwner.DoTweenAsync(true).Forget();
                }
            }
            else if (GUILayout.Button("▶ Play End") == true)
            {
                foreach (BaseTweener iteratingOwner in owners)
                {
                    iteratingOwner.DoTweenAsync(false).Forget();
                }
            }
            else if (GUILayout.Button("⏹ Stop") == true)
            {
                foreach (BaseTweener iteratingOwner in owners)
                {
                    iteratingOwner.PauseTween();
                }
            }

            EditorGUILayout.EndHorizontal();
        }
    }
}
