using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListHolder<T> : MonoBehaviour
{
    public List<T> items = new List<T>();

    public static bool IsOverlap(List<T> list1, List<T> list2)
    {
        return list1.Exists(x => list2.Exists(y => y.Equals(x)));
    }
    public bool IsOverlap(List<T> list)
    {
        return items.Exists(x => list.Exists(y => y.Equals(x)));
    }
}
