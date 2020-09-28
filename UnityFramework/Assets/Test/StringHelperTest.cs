using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Codingriver;

public class StringHelperTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        string str = "哈哈哈哈你们好吗！ codingriver";
        Debug.Log(str.ToUnicode());
        Debug.Log(str.ToUnicode().FromUnicode());

        Debug.Log(str.ToBytes().ToHex());
        Debug.Log(str.ToUtf8().ToHex());
        Debug.Log(str.ToBytes().ToStr());

    }

}
