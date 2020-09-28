using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[ExecuteInEditMode]
public class MyPlayer : MonoBehaviour
{
    /// <summary>
    /// 枚举
    /// 多选的时候枚举必须是2的次方值，有意义的最小值为1
    /// </summary>
    public enum CodingType
    {
        Nothing=0,
        One=1,
        Two=2,
        Three=4,
        Four=8,
        Five=16,
    }

    // Start is called before the first frame update
    public int id;

    public string playerName= "https://codingriver.github.io ";
    public string backStory= "https://codingriver.github.io";
    public float health;
    public float damage;

    public float weaponDamage1, weaponDamage2;

    public string shoeName;
    public int shoeSize;
    public string shoeType;
    public GameObject obj;
    public Transform trans;
    public Material mat;
    public int popup;
    public int mixedPopup;//可以多选
    public CodingType codingType;
    public CodingType mixedCodingType;//可以多选
    public int gridId;
    public string password= "codingriver";
    public bool isToggle;
    public GameObject go;
    public float knob=5.6f;
    public bool toggleGroupOpen;
    public float fadeGroupValue=1;
    void Start()
    {
        health = 50;
    }

    /// <summary>
    /// 将多选的值转换为多选数组的下标(多选后的值的计算和LayerMask.GetMask类似)
    /// 如果是枚举  则每个值+1后是对应的枚举
    /// 
    /// </summary>
    /// <param name="mixedValue">mixed的值</param>
    /// <param name="arrLen">数组数量（如果是枚举则是枚举的数量，排除枚举为0的值）</param>
    /// <returns></returns>
    public static List<int> ResolveMixed(int mixedValue, int arrLen)
    {
        List<int> ls = new List<int>();
        //全选时值为-1
        if (mixedValue == -1)
        {
            for (int i = 0; i < arrLen; i++)
                ls.Add(i);

            return ls;
        }

        for (int i = 0; i < 32; i++)
        {
            if ((mixedValue & 1) == 1) ls.Add(i);

            mixedValue = mixedValue >> 1;
        }
        return ls;
    }

    private void Update()
    {
        
    }
}
