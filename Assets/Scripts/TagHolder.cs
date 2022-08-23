using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TagHolder : MonoBehaviour
{
    public List<ScriptableTags> tags = new List<ScriptableTags>();

    public bool IsOverlap(List<ScriptableTags> list)
    {
        return tags.Exists(x => list.Exists(y => y==x));
    }
}
