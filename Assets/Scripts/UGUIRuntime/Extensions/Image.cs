﻿using UnityEngine;
using UnityEngine.UI;
namespace UGUIRuntime
{
    public static partial class UGUIRuntimeExtensions
    {
        private static void SetSpriteUrl(this Image image, string url,
          int border, bool setNativeSize, bool showLoading)
        {
            image.enabled = false;
            var _maskId = 0;
            if (showLoading)
            {
                _maskId = LoadingMask.Show(image.rectTransform.position, image.rectTransform.rect.size);
            }
            SpriteLoader.LoadFromUrl(url, border, (sprite) =>
            {
                image.sprite = sprite;
                image.enabled = true;
                if (setNativeSize)
                {
                    image.type = Image.Type.Simple;
                    image.SetNativeSize();
                }
                else if (!Mathf.Approximately(border, 0))
                {
                    image.type = Image.Type.Sliced;
                }
                if (showLoading)
                {
                    LoadingMask.Hide(_maskId);
                }
            });
        }

        public static Image SetSprite(this Image image, string name,
            int border = 0, bool setNativeSize = false, bool showLoading = false)
        {
            var host = UGUI.host ?? "";
            var url = name.StartsWith("http") ? name : host + name + ".png";
            image.SetSpriteUrl(url, border, setNativeSize, showLoading);
            return image;
        }

        public static Image SetColor(this Image image, Color32 color)
        {
            image.color = color;
            return image;
        }

        public static Image SetPosition(this Image comp, float x, float y)
        {
            comp.GetRectTransform().SetPosition(x, y);
            return comp;
        }

        public static Image SetSize(this Image comp, float w, float h)
        {
            comp.GetRectTransform().SetSize(w, h);
            return comp;
        }
    }
}
