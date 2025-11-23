using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace ScarToolkit.Button.Editor
{
    [CustomEditor(typeof(MonoBehaviour), true)]
    public class CustomEditorAtributeProcessor : UnityEditor.Editor
    {
       
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
          
            ProcessEditorButtons();
        }

        private void ProcessEditorButtons()
        {
            var targetType = target.GetType();
            var methods = targetType.GetMethods();

            foreach (var method in methods)
            {
                var btnAttr = method.GetCustomAttribute<ButtonAttribute>();

                if (btnAttr != null)
                {
                    var btnName = btnAttr.ButtonName;
                    
                    if (btnName == null)
                    {
                        btnName = method.Name;
                    }
                    if (GUILayout.Button(btnName))
                    {
                        method.Invoke(target, null);
                        EditorUtility.SetDirty(target);

                        if (target is GameObject targetGo)
                        {
                            UnityEditor.SceneManagement.EditorSceneManager.MarkSceneDirty(targetGo.scene);
                        }
                       
                    }
                }

            }
        }
      
    }
}