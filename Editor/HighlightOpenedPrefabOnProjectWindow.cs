using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Kogane.Internal
{
    [InitializeOnLoad]
    internal static class HighlightOpenedPrefabOnProjectWindow
    {
        static HighlightOpenedPrefabOnProjectWindow()
        {
            EditorApplication.projectWindowItemOnGUI += OnGUI;
        }

        private static void OnGUI( string guid, Rect selectionRect )
        {
            var setting = HighlightOpenedPrefabOnProjectWindowSetting.instance;

            if ( !setting.IsEnable ) return;

            var currentPrefabStage = PrefabStageUtility.GetCurrentPrefabStage();

            if ( currentPrefabStage == null ) return;

            var assetPath = AssetDatabase.GUIDToAssetPath( guid );

            if ( assetPath != currentPrefabStage.assetPath ) return;

            var position = new Rect( selectionRect )
            {
                x     = 0,
                width = selectionRect.x + selectionRect.width,
            };

            var color = GUI.color;

            try
            {
                GUI.color = setting.Color;
                GUI.DrawTexture( position, EditorGUIUtility.whiteTexture );
            }
            finally
            {
                GUI.color = color;
            }
        }
    }
}