using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Kogane.Internal
{
    internal sealed class HighlightOpenedPrefabOnProjectWindowSettingProvider : SettingsProvider
    {
        private const string PATH = "Kogane/Highlight Opened Prefab On Project Window";

        private Editor m_editor;

        private HighlightOpenedPrefabOnProjectWindowSettingProvider
        (
            string              path,
            SettingsScope       scopes,
            IEnumerable<string> keywords = null
        ) : base( path, scopes, keywords )
        {
        }

        public override void OnActivate( string searchContext, VisualElement rootElement )
        {
            var instance = HighlightOpenedPrefabOnProjectWindowSetting.instance;

            instance.hideFlags = HideFlags.HideAndDontSave & ~HideFlags.NotEditable;

            Editor.CreateCachedEditor( instance, null, ref m_editor );
        }

        public override void OnGUI( string searchContext )
        {
            using var changeCheckScope = new EditorGUI.ChangeCheckScope();

            var setting = HighlightOpenedPrefabOnProjectWindowSetting.instance;

            m_editor.OnInspectorGUI();

            if ( !changeCheckScope.changed ) return;

            setting.Save();
        }

        [SettingsProvider]
        private static SettingsProvider CreateSettingProvider()
        {
            return new HighlightOpenedPrefabOnProjectWindowSettingProvider
            (
                path: PATH,
                scopes: SettingsScope.Project
            );
        }
    }
}