﻿using UnityEditor;
using UnityEngine;

namespace CZToolKit.Core.Editors
{
    public static partial class EditorGUIExtension
    {
        /// <summary> 绘制一个ProgressBar </summary>
        public static float ProgressBar(Rect _rect, float _value, float _minLimit, float _maxLimit, string _text, bool _dragable = true, bool _drawMinMax = false)
        {
            float progress = (_value - _minLimit) / (_maxLimit - _minLimit);

            Rect r = _rect;
            GUI.Box(r, "");
            r.width = _rect.width * progress;
            EditorGUI.DrawRect(r, new Color(0.07f, 0.56f, 0.9f, 1));

            if (_drawMinMax)
            {
                EditorGUI.LabelField(_rect, _minLimit.ToString());
                EditorGUI.LabelField(_rect, _maxLimit.ToString(), EditorStylesExtension.RightLabelStyle);
            }
            EditorGUI.LabelField(_rect, _text, EditorStylesExtension.MiddleLabelStyle);

            if (_dragable)
#if UNITY_2019_1_OR_NEWER
                return GUI.HorizontalSlider(_rect, _value, _minLimit, _maxLimit, EditorStylesExtension.Transparent, EditorStylesExtension.Transparent, EditorStylesExtension.Transparent);
#else
                return GUI.HorizontalSlider(_rect, _value, _minLimit, _maxLimit, EditorStylesExtension.Transparent, EditorStylesExtension.Transparent);
#endif
            return _value;
        }
    }
}
