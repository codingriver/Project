using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Codingriver;

public class DumperTest : MonoBehaviour
{
    public class InnerTest
    {
        public class InnerA
        {
            string innerStr = "my name is codingriver";
            int key = 99;
        }
        int a = 1;
        string b = "hello";
        int[] arr = new int[] { 5, 76, 12 };
        InnerA innerA = new InnerA();
    }
    public class TestData
    {
        public string[] Sitekeys = new string[] { "https://codingriver.github.io", "codingriver" };
        string name = "codingriver";
        int m_Main = 1024;

        protected InnerTest obj = new InnerTest();

        public int Main
        {
            get
            {
                return m_Main;
            }
            set
            {
                m_Main = value;
            }
        }
    }




    // Start is called before the first frame update
    void Start()
    {
        TestData data = new TestData();
        Debug.Log(Dumper.DumpAsString(data));
    }



}
