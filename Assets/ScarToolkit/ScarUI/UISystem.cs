using System.Collections.Generic;
using System.Linq;
using ScarToolkit.Button;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ScarToolkit.ScarUI
{
    public class UISystem : MonoBehaviour
    {
        [SerializeField] private List<UIScreen> screens;
        [SerializeField] private EventSystem eventSystem;
        [SerializeField] private Transform screensContainer;

        public EventSystem EventSystem => eventSystem;

        public void Init()
        {
            if (screens != null)
            {
                foreach (var screen in screens)
                {
                    screen.Init();
                }
            }
            else
            {
                Debug.LogError("No screens in ui system");
            }
        }

        public T GetScreen<T>() where T : UIScreen
        {
            foreach (var screen in screens)
            {
                if (screen is T tScreen)
                {
                    return tScreen;
                }
            }

            return null;
        }

#if UNITY_EDITOR
        [Button()]
        public void GetScreensFromContainer()
        {
            if (screensContainer)
            {
                screens = screensContainer.GetComponentsInChildren<UIScreen>().ToList();
            }
        }
#endif
       
    }
}