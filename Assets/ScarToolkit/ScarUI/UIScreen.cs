using UnityEngine;

namespace ScarToolkit.ScarUI
{
    public class UIScreen : UIView
    {
        [SerializeField] private Canvas canvas;

        public Canvas Canvas => canvas;
    }
}