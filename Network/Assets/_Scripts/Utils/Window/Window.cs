using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace _Scripts.Utils.Window
{
    [RequireComponent(typeof(Canvas))]
    [RequireComponent(typeof(GraphicRaycaster))]
    public abstract class Window : MonoBehaviour
    {
        protected delegate void OnOpenEvent();
        protected delegate void OnCloseEvent();

        public UnityEvent onRefresh;
        public UnityEvent onEnable;
        public UnityEvent onDisable;

        protected event OnOpenEvent onOpen;
        protected event OnCloseEvent onClose;

        [SerializeField]
        private bool over;
        private Canvas canvas;
        private Canvas Canvas
        {
            get
            {
                if (canvas == null)
                {
                    canvas = GetComponent<Canvas>();
                    if (canvas == null)
                    {
                        Debug.LogError($"{name} with window component has no Canvas component");
                        canvas = gameObject.AddComponent<Canvas>();
                    }
                }
                return canvas;
            }
        }
        private GraphicRaycaster raycaster;
        private GraphicRaycaster Raycaster
        {
            get
            {
                if (raycaster == null)
                {
                    raycaster = GetComponent<GraphicRaycaster>();
                    if (raycaster == null)
                    {
                        Debug.LogError($"{name} with window component has no GraphicRaycaster component");
                        raycaster = gameObject.AddComponent<GraphicRaycaster>();
                    }
                }
                return raycaster;
            }
        }

        public bool Over => over;

        [SerializeField] private bool _canBeClosedByGesture;
        public bool CanBeClosedByGesture => _canBeClosedByGesture;
        protected bool Opened { get; private set; }

        public void Enable()
        {
            Canvas.enabled = true;
            Raycaster.enabled = true;
            onEnable?.Invoke();
        }

        public void Disable()
        {
            Canvas.enabled = false;
            Raycaster.enabled = false;
            onDisable?.Invoke();
        }

        public virtual void OpenWindow()
        {
            if (WindowController.CanBeOpened(this))
            {
                Enable();

                Opened = true;
                onOpen?.Invoke();

                WindowController.AddWindow(this);

                RefreshWindow();
            }
        }

        public virtual void CloseWindow(bool refresh = true)
        {
            Disable();
            Opened = false;
            onClose?.Invoke();
            WindowController.RemoveWindow(this, refresh);
        }

        public virtual void RefreshWindow()
        {
            onRefresh?.Invoke();
        }

        public virtual void Init()
        {
            gameObject.SetActive(false);
        }
    }
}