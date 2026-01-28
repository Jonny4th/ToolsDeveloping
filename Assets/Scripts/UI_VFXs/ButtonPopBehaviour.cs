using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ButtonPopBehaviour : MonoBehaviour
{
    public Button Button;

    [SerializeField] private float maxScale;

    [Tooltip("+ and - degree of rotation.")]
    [Range(0f, 90f)]
    [SerializeField] private float angleRange;

    [SerializeField] private AnimationCurve vfxCurve;
    [SerializeField] private float vfxDuration;

    private Coroutine vfxProcess;
    private Transform subjectTransform;

    private Vector3 originalScale;
    private Quaternion originalOrientation;

    private void Start()
    {
        Button.onClick.AddListener(Pop);
        subjectTransform = Button.transform;
        originalScale = subjectTransform.localScale;
        originalOrientation = subjectTransform.localRotation;
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

            var evaluatedScale = Mathf.Lerp(1, maxScale, curveEvaluation);
            var newScale = evaluatedScale * originalScale;
            subjectTransform.localScale = newScale;

            var evaluatedAngle = Mathf.Lerp(0, maxAngle, curveEvaluation);
            var rotation = Quaternion.AngleAxis(evaluatedAngle, subjectTransform.forward);
            var newOrientation = originalOrientation * rotation;
            subjectTransform.localRotation = newOrientation;

            yield return null;
        }

        subjectTransform.localScale = originalScale;
        subjectTransform.localRotation = originalOrientation;
    }
}
