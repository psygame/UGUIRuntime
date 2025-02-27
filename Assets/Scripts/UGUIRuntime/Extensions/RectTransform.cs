﻿using UnityEngine;
using UnityEngine.UI;

namespace UGUIRuntime
{
    public static partial class UGUIRuntimeExtensions
    {
        private static RectTransform SetAnchoredPosition(this RectTransform rectTransform, Vector2 position)
        {
            rectTransform.anchoredPosition = position;
            return rectTransform;
        }

        private static RectTransform SetSizeDelta(this RectTransform rectTransform, Vector2 sizeDelta)
        {
            rectTransform.sizeDelta = sizeDelta;
            return rectTransform;
        }

        private static RectTransform SetAnchorMinMax(this RectTransform rectTransform, Vector2 min, Vector2 max)
        {
            rectTransform.anchorMin = min;
            rectTransform.anchorMax = max;
            return rectTransform;
        }

        private static RectTransform SetMargin(this RectTransform rectTransform, float all = 0f)
        {
            rectTransform.SetMargin(all, all, all, all);
            return rectTransform;
        }

        private static RectTransform SetMargin(this RectTransform rectTransform, float horizontal, float vertical)
        {
            rectTransform.SetMargin(vertical, horizontal, vertical, horizontal);
            return rectTransform;
        }

        private static RectTransform SetMargin(this RectTransform rectTransform, float top, float right, float down, float left)
        {
            rectTransform.anchorMin = Vector2.zero;
            rectTransform.anchorMax = Vector2.one;
            rectTransform.sizeDelta = new Vector2(-left - right, -top - down);
            rectTransform.anchoredPosition = new Vector2(left, top);
            return rectTransform;
        }

        private static RectTransform SetPivot(this RectTransform rectTransform, Vector2 pivot)
        {
            rectTransform.pivot = pivot;
            return rectTransform;
        }

        private static RectTransform SetPivotCenter(this RectTransform rectTransform)
        {
            rectTransform.SetPivot(Vector2.one * 0.5f);
            return rectTransform;
        }

        private static RectTransform SetCenter(this RectTransform rectTransform)
        {
            rectTransform.SetPivot(Vector2.one * 0.5f)
                .SetAnchorMinMax(Vector2.one * 0.5f, Vector2.one * 0.5f);
            return rectTransform;
        }

        internal static void Reset(this RectTransform rectTransform)
        {
            rectTransform.sizeDelta = Vector2.zero;
            rectTransform.anchoredPosition3D = Vector3.zero;
            rectTransform.localScale = Vector3.one;
            rectTransform.localRotation = Quaternion.identity;
            rectTransform.pivot = Vector2.up;
            rectTransform.anchorMin = Vector2.up;
            rectTransform.anchorMax = Vector2.up;
        }

        private static RectTransform GetOrAddNode(this RectTransform rectTransform, string name)
        {
            var node = rectTransform.Find(name);
            if (!node)
            {
                node = rectTransform.AddNode(name);
            }
            return node.GetRectTransform();
        }

        private static T GetOrAddComponent<T>(this RectTransform rectTransform) where T : Component
        {
            var comp = rectTransform.GetComponent<T>();
            if (!comp)
            {
                comp = rectTransform.gameObject.AddComponent<T>();
            }
            return comp;
        }

        private static RectTransform GetRectTransform(this Component component)
        {
            return component.GetComponent<RectTransform>();
        }

        #region Set Items
        public static RectTransform SetIndex(this RectTransform rectTransform, int index)
        {
            rectTransform.SetSiblingIndex(index);
            return rectTransform;
        }

        public static RectTransform SetPosition(this RectTransform rectTransform, float x, float y)
        {
            return rectTransform.SetPosition(new Vector2(x, y));
        }

        public static RectTransform SetPosition(this RectTransform rectTransform, Vector2 pos)
        {
            rectTransform.SetAnchoredPosition(new Vector2(pos.x, -pos.y));
            return rectTransform;
        }

        public static RectTransform SetSize(this RectTransform rectTransform, float w, float h)
        {
            rectTransform.SetSize(new Vector2(w, h));
            return rectTransform;
        }

        public static RectTransform SetSize(this RectTransform rectTransform, Vector2 size)
        {
            rectTransform.SetSizeDelta(size);
            return rectTransform;
        }
        #endregion

        #region Add Items
        public static RectTransform AddNode(this RectTransform rectTransform, string name = null)
        {
            var go = new GameObject(name ?? "node");
            go.layer = UGUI.UI_LAYER;
            var node = go.AddComponent<RectTransform>();
            node.SetParent(rectTransform);
            node.Reset();
            return node;
        }

        public static Image AddImage(this RectTransform rectTransform, string name = null)
        {
            var node = rectTransform.AddNode(name ?? "image");
            var image = node.gameObject.AddComponent<Image>();
            image.raycastTarget = false;
            return image;
        }

        public static Button AddButton(this RectTransform rectTransform, string name = null)
        {
            var node = rectTransform.AddNode(name ?? "button");
            var image = node.gameObject.AddComponent<Image>();
            var button = node.gameObject.AddComponent<Button>();
            button.image = image;
            return button;
        }

        public static Text AddText(this RectTransform rectTransform, string name = null)
        {
            var node = rectTransform.AddNode(name ?? "text");
            var text = node.gameObject.AddComponent<Text>();
            text.alignment = TextAnchor.MiddleLeft;
            text.fontSize = 24;
            text.raycastTarget = false;
            return text;
        }

        public static Toggle AddToggle(this RectTransform rectTransform, string name = null)
        {
            var node = rectTransform.AddNode(name ?? "toggle");
            node.AddNode("Background").SetMargin().AddNode("Checkmark").SetCenter();
            var toggle = node.gameObject.AddComponent<Toggle>();
            return toggle;
        }

        public static Switch AddSwitch(this RectTransform rectTransform, string name = null)
        {
            var toggle = rectTransform.AddToggle(name ?? "switch");
            toggle.GetRectTransform().GetOrAddNode("Background").AddNode("Knob").SetCenter();
            var _switch = toggle.gameObject.AddComponent<Switch>();
            _switch.toggle = toggle;
            return _switch;
        }

        public static Slider AddSlider(this RectTransform rectTransform, string name = null)
        {
            var node = rectTransform.AddNode(name ?? "slider");
            node.AddNode("Background").SetMargin().SetPivotCenter();
            node.AddNode("Fill Area").SetMargin().SetPivotCenter().AddNode("Fill")
                .SetAnchorMinMax(Vector2.zero, Vector2.one);
            node.AddNode("Handle Slide Area").SetMargin().SetPivotCenter()
                .AddNode("Position").SetCenter()
                .AddNode("Handle").SetCenter();
            var slider = node.gameObject.AddComponent<Slider>();
            return slider;
        }

        public static Dropdown AddDropdown(this RectTransform rectTransform, string name = null)
        {
            var node = rectTransform.AddNode(name ?? "dropdown");
            var dropdown = node.GetOrAddComponent<Dropdown>();
            dropdown.image = node.GetOrAddComponent<Image>();

            var label = node.AddNode("Label").SetMargin(10, 0).GetOrAddComponent<Text>()
                .SetFont().SetColor(Color.black);
            label.alignment = TextAnchor.MiddleLeft;

            var template = node.AddNode("Template");
            template.SetAnchorMinMax(Vector2.zero, Vector2.right);
            template.SetPivot(new Vector2(0.5f, 1f));
            template.gameObject.SetActive(false);
            template.SetSizeDelta(new Vector2(0, 150));
            template.SetAnchoredPosition(new Vector2(0, 2));
            template.GetOrAddComponent<Image>();
            var scrollRect = template.GetOrAddComponent<ScrollRect>();
            var canvas = template.GetOrAddComponent<Canvas>();
            canvas.overrideSorting = true;
            canvas.sortingOrder = 30000;
            template.GetOrAddComponent<GraphicRaycaster>();
            template.GetOrAddComponent<CanvasGroup>();

            var viewport = template.AddNode("Viewport");
            viewport.SetMargin();
            viewport.GetOrAddComponent<Image>().SetColor(new Color32(0, 0, 0, 0));
            viewport.GetOrAddComponent<RectMask2D>();

            var content = viewport.AddNode("Content")
                .SetAnchorMinMax(Vector2.up, Vector2.one)
                .SetPivot(new Vector2(0.5f, 1))
                .SetSizeDelta(new Vector2(0, 38));

            var item = content.AddNode("Item");
            item.SetAnchorMinMax(new Vector2(0, 0.5f), new Vector2(1, 0.5f));
            item.SetPivotCenter();
            item.SetSizeDelta(new Vector2(0, 30));
            var itemToggle = item.GetOrAddComponent<Toggle>();
            itemToggle.targetGraphic = item.AddNode("Item Background").SetMargin().GetOrAddComponent<Image>();
            itemToggle.graphic = item.AddNode("Item Checkmark").SetMargin().GetOrAddComponent<Image>().SetColor(Color.gray);
            var itemLabel = item.AddNode("Item Label").SetMargin(10, 0).GetOrAddComponent<Text>()
                .SetFont().SetColor(Color.black); ;
            itemLabel.alignment = TextAnchor.MiddleLeft;

            scrollRect.viewport = viewport;
            scrollRect.content = content;
            scrollRect.horizontal = false;
            scrollRect.movementType = ScrollRect.MovementType.Clamped;
            scrollRect.scrollSensitivity = 20;

            dropdown.template = template;
            dropdown.captionText = label;
            dropdown.itemText = itemLabel;

            return dropdown;
        }
        #endregion

    }
}
