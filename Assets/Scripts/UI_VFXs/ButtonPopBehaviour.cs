using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ButtonPopBehaviour : MonoBehaviour
{
    public Button Button;

    [SerializeField] private float scaleFactor;

    [Tooltip("+ and - degree of rotation.")]
    [Range(0f, 90f)]
    [SerializeField] private float angleRange;

    [SerializeField] private AnimationCurve vfxCurve;
    [SerializeField] private float vfxDuration;

    private Coroutine vfxProcess;

    private Vector3 originalScale;
    private Quaternion originalOrientation;

    private void Start()
    {
        Button.onClick.AddListener(Pop);
        originalScale = transform.localScale;
        originalOrientation = transform.localRotation;
    }

    private void Pop()
    {
        if(vfxProcess != null)
        {
            StopCoroutine(vfxProcess);
        }

        vfxProcess = StartCoroutine(PopVfxProcess());
    }

    IEnumerator PopVfxProcess()
    {
        var endTime = Time.time + vfxDuration;
        var startTime = Time.time;
        var maxAngle = Random.Range(-angleRange, angleRange);

        while(Time.time < endTime)
        {
            var normalizedTime = (Time.time - startTime) / vfxDuration;
            var curveEvaluation = vfxCurve.Evaluate(normalizedTime);

            var maxScale = scaleFactor * originalScale;
            var newScale = Vector3.Lerp(originalScale, maxScale, curveEvaluation);
            transform.localScale = newScale;

            var orientationAtMaximum = Quaternion.AngleAxis(maxAngle, transform.forward);
            var newOrientation = Quaternion.Lerp(originalOrientation, orientationAtMaximum, curveEvaluation);
            transform.localRotation = newOrientation;

            yield return null;
        }

        transform.localScale = originalScale;
        transform.localRotation = originalOrientation;
    }
}
