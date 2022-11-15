using System.Collections.Generic;
using UnityEngine;

namespace _Scripts.Utils.Window
{
    public class WindowController : Singleton<WindowController>
    {
        [SerializeField]
        private Window startWindow;

        private static List<Window> windows = new List<Window>();
        private static Window currentWindow;

        private void Start()
        {
            startWindow?.OpenWindow();
        }

        private void Update()
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                CloseCurrentWindow();
            }
        }

        public static void CloseAll(Window except = null)
        {
            //Debug.LogWarning($"Close all windows except {except.name}");
            for (int i = 0; i < windows.Count - 1; i++)
            {
                //Debug.LogWarning($"{i} {windows[i].name}");
                if (windows[i] == except || windows[i] == Instance.startWindow)
                {
                    //Debug.LogWarning($"Can't close {windows[i].name} window");
                    continue;
                }
                else
                {
                    windows[i].CloseWindow(false);
                    i--;
                }
            }
        }

        public static void AddWindow(Window window)
        {
            if (windows.Count > 0)
            {
                if (!window.Over)
                {
                    currentWindow.Disable();
                    //windows[windows.Count - 1].DisableWindow();
                }
            }
            if (windows.Contains(window))
            {
                windows.Remove(window);
            }
            windows.Add(window);
            currentWindow = window;
        }

        public static void RemoveWindow(Window window, bool refresh)
        {
            windows.Remove(window);
            if (window == currentWindow)
            {
                if (windows.Count > 0)
                {
                    currentWindow = windows[windows.Count - 1];
                    currentWindow.Enable();
                    //windows[windows.Count - 1].EnableWindow();
                    if (refresh)
                    {
                        currentWindow.RefreshWindow();
                        //windows[windows.Count - 1].RefreshWindow();
                    }
                }
            }
        }

        public static bool CanBeOpened(Window window)
        {
            if (windows.Count > 0)
            {
                return currentWindow != window;
                //return windows[windows.Count - 1] != window;
            }
            else
            {
                return true;
            }
        }

        private void CloseCurrentWindow()
        {
            if (windows.Count >= 1 && currentWindow.CanBeClosedByGesture)
            {
                currentWindow.Disable();
                RemoveWindow(currentWindow, false);
                //windows[windows.Count - 1].Disable();
                //RemoveWindow(windows[windows.Count - 1], false);
            }
        }

#if UNITY_EDITOR
        [UnityEditor.CustomEditor(typeof(WindowController))]
        public class WindowsControllerEditor : UnityEditor.Editor
        {
            public override void OnInspectorGUI()
            {
                base.OnInspectorGUI();

                if (Application.isPlaying)
                {
                    for (int i = 0; i < windows.Count; i++)
                    {
                        UnityEditor.EditorGUILayout.LabelField(windows[i].name);
                    }
                }
            }
        }
#endif
    }
}