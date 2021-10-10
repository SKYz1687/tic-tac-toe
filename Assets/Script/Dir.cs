using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dir : MonoBehaviour
{
    Dictionary<int, string> d;
    int[] T = {1, 5, 8, 9, 7, 9, 14};
    int[] X = {1, 5, 8, 9, 7, 9, 14 };

    int H = 0;
    int Te = 0;
    private void Start()
    {
        d = new Dictionary<int, string>();

        d.Add(1000, "Hello");
        d.Add(1001, "Hi");
        d.Add(1002, "Hp");

        foreach(var item in d)
        {
            Debug.Log(item);
        }
        H = GetHashCode(T);
        Te = GetHashCode(X);
        Debug.Log(H);
        Debug.Log(Te);
    }
    int GetHashCode(int[] values)
    {
        int result = 0;
        int shift = 0;
        for (int i = 0; i < values.Length; i++)
        {
            shift = (shift + 11) % 21;
            result ^= (values[i] + 1024) << shift;
        }
        return result;
    }
}
