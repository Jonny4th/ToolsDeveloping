using TMPro;
using UnityEngine;

public class Size : MonoBehaviour
{
    public TMP_Text text;

    private void Update()
    {
        text.text = transform.position.x.ToString();
        text.text += "\n" + transform.localPosition.x;
    }
}
