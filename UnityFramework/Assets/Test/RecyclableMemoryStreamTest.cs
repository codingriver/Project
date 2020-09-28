using System;
using UnityEngine;
using Codingriver;


class RecyclableMemoryStreamTest
{

    void Start()
    {
        RecyclableMemoryStreamManager manager = new RecyclableMemoryStreamManager();
        using (var stream= manager.GetStream())
        {
            
        }

        
    }

}

