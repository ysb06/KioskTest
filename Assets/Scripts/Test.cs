using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Test : MonoBehaviour
{
    private void Start()
    {
        string[] testCase = { "서울시", "수원시", "인천시", "화성시", "부천시", "분당", "송도", "기타" };
        printArray(testCase);
        printArray(GenerateRandomAnswerSet(testCase));
    }

    private string[] GenerateRandomAnswerSet(string[] target)
    {
        string[] result = new string[target.Length];

        foreach (string str in target)
        {
            int pos = -1;
            do
            {
                pos = UnityEngine.Random.Range(0, result.Length);

            } while (result[pos] != null);
            result[pos] = str;
        }
        Array.Sort(result, (string s1, string s2) =>
        {
            //print((s1 == " ") + ", " + (s2 == " "));

            if (s1 == " " && s2 == " ") return 0;
            else if (s1 != " " && s2 != " ") return 0;
            else if (s1 == " ") return -1;
            else return 1;
        });
        
        return result;
    }

    private void printArray(string[] target)
    {
        string result = string.Empty;
        foreach (string s in target)
        {
            result += s + ", ";
        }
        Debug.Log(result);
    }
}
