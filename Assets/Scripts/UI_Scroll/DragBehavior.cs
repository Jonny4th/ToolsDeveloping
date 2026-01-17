using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MyTool.ScrollUI
{
    public class DragBehavior : MonoBehaviour, IEndDragHandler
    {
        public float ThresholdVelo = 100f;
        public Vector2 CurrentPosition;
        public float posX;
        public float speed;
        public ScrollRect ScrollRect;
        public float halfScrenX;

        private Coroutine moveCoroutine;

        private void Awake()
        {
            ScrollRect.onValueChanged.AddListener(HandleValueChange);
        }

        private void Update()
        {
            halfScrenX = Screen.width / 2;
        }

        public void HandleValueChange(Vector2 pos)
        {
            CurrentPosition = pos;
            posX = ScrollRect.horizontalNormalizedPosition;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (moveCoroutine != null) StopCoroutine(moveCoroutine);
            moveCoroutine = StartCoroutine(GotoValue());
        }

        IEnumerator GotoValue()
        {
            while (Mathf.Abs(ScrollRect.velocity.x) > ThresholdVelo)
            {
                yield return null;
            }

            var x = FindNearestStop();
            Debug.Log(x);

            while (Mathf.Abs(ScrollRect.horizontalNormalizedPosition - x) > 0.01f)
            {
                ScrollRect.horizontalNormalizedPosition = Mathf.Lerp(ScrollRect.horizontalNormalizedPosition, x, speed*Time.deltaTime);
                yield return null;
            }

            ScrollRect.horizontalNormalizedPosition = x;
        }

        public float FindNearestStop()
        {
            return Mathf.Round(posX);
        }
    }
}