using System;

namespace ScarToolkit.Button
{
    [AttributeUsage(AttributeTargets.Method)]
    public class ButtonAttribute : Attribute
    {
        private string _buttonName;
        public string ButtonName => _buttonName;
    
        public ButtonAttribute(string buttonName = null)
        {
            _buttonName = buttonName;
        }
    }
}
