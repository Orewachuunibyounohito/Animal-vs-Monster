using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

namespace UiTookit.Inventory
{
    public class InventoryView : StorageView
    {
        [SerializeField]
        private string panelName = "Inventory";

        public override IEnumerator InitializeView(int size = 20)
        {
            Slots = new Slot[size];
            root = uiDocument.rootVisualElement;
            root.Clear();

            root.styleSheets.Add(styleSheet);

            // Can make it an extension
            root.CreateChild("container");

            yield return null;
        }
    }

    public static class VisualElementExtension
    {
        public static VisualElement CreateChild(this VisualElement parent, params string[] classes)
        {
            var child = new VisualElement();
            child.AddClass(classes).AddTo(parent);
            return child;
        }

        public static T CreateChild<T>(this VisualElement parent, params string[] classes) where T : VisualElement, new()
        {
            var child = new T();
            child.AddClass(classes).AddTo(parent);
            return child;
        }

        public static T AddClass<T>(this T visualElement, string[] classes) where T : VisualElement
        {
            foreach (var childClass in classes){
                if (string.IsNullOrEmpty(childClass)) { continue; }
                visualElement.AddToClassList(childClass);
            }
            return visualElement;
        }
        
        public static T AddTo<T>(this T child, VisualElement parent) where T : VisualElement
        {
            parent.Add(child);
            return child;
        }

        public static T WithManipulator<T>(this T visualElement, IManipulator manipulator) where T : VisualElement
        {
            visualElement.AddManipulator(manipulator);
            return visualElement;
        }
    }
}
