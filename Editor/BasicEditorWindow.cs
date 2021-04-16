﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace CZToolKit.Core.Editors
{
    public abstract class BasicEditorWindow<T> : EditorWindow where T : BasicEditorWindow<T>, new()
    {

        static GUIStyle csIconStyle;
        static GUIContent csIcon;

        public static GUIStyle CSIconStyle
        {
            get
            {
                if (csIconStyle == null)
                {
                    csIconStyle = new GUIStyle(GUI.skin.button);
                    csIconStyle.padding.left = 0;
                    csIconStyle.padding.right = 0;
                    csIconStyle.padding.top = 0;
                    csIconStyle.padding.bottom = 0;
                }
                return csIconStyle;
            }
        }
        public static GUIContent CSIcon
        {
            get
            {
                if (csIcon == null)
                    csIcon = new GUIContent(EditorGUIUtility.FindTexture("cs Script Icon"), "打开窗口脚本");
                return csIcon;
            }
        }

        MonoScript monoScript;
        MonoScript MonoScript
        {
            get
            {
                if (monoScript == null)
                    monoScript = MonoScript.FromScriptableObject(this);
                return monoScript;
            }
        }

        protected virtual void ShowButton(Rect rect)
        {
            rect.x -= 8;
            rect.width = 20;
            if (GUI.Button(rect, CSIcon, CSIconStyle))
                AssetDatabase.OpenAsset(MonoScript);
        }

        Stack<EditorCoroutine> coroutineStack = new Stack<EditorCoroutine>();

        protected virtual void Update()
        {
            int count = coroutineStack.Count;
            while (count-- > 0)
            {
                EditorCoroutine coroutine = coroutineStack.Pop();
                if (!coroutine.IsRunning) continue;
                ICondition condition = coroutine.Current as ICondition;
                if (condition == null || condition.Result(coroutine))
                {
                    if (!coroutine.MoveNext())
                        continue;
                }
                coroutineStack.Push(coroutine);
            }
        }

        public EditorCoroutine StartCoroutine(IEnumerator _coroutine)
        {
            EditorCoroutine coroutine = new EditorCoroutine(_coroutine);
            coroutineStack.Push(coroutine);
            return coroutine;
        }
    }
}