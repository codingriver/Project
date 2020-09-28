using System;
using UnityEngine;
using Codingriver;
using System.Threading;
using System.Runtime;
using System.Collections;
using System.Threading.Tasks;

class TimerManagerTest : MonoBehaviour
{


    void Test()
    {

    }
    TaskCompletionSource<bool> tcs = new TaskCompletionSource<bool>();
    int a = 999;
    async void Start()
    {
        
        //GC.Collect();
        //GCSettings.LargeObjectHeapCompactionMode = GCLargeObjectHeapCompactionMode.Default;
        //GC.SuppressFinalize
        //while (true)
        //{
        //    ProfilerHelper.BeginSample("udpate2");
        //    if (tcs == null )
        //    {
        //        tcs = new TaskCompletionSource<bool>();
        //        count = 20;
        //    }
        //    ProfilerHelper.EndSample();
            
        //    ProfilerHelper.BeginSample("start");
        //    await tcs.Task;
        //    tcs = null;
        //    ProfilerHelper.EndSample();
        //    ProfilerHelper.BeginSample("hello");
        //    Debug.Log("hello");
        //    ProfilerHelper.EndSample();
        //    //yield return new WaitForSeconds(0.01f);
        //    //TimerManager.Instance.Wait(10, null);
        //}
        



        //long id;
        //Debug.Log("hello:" + TimeHelper.CurTime);
        //await TimerManager.Instance.Wait(1000);
        //Debug.Log("hello:" + TimeHelper.CurTime);
        //await TimerManager.Instance.Wait(1000);
        //Debug.Log("hello:" + TimeHelper.CurTime);
        //await TimerManager.Instance.Wait(1000);
        //Debug.Log("hello:" + TimeHelper.CurTime);
        //await TimerManager.Instance.Wait(4000);
        //Debug.Log("hello:" + TimeHelper.CurTime);
        //await TimerManager.Instance.Wait(2000);
        //Debug.Log("hello:" + TimeHelper.CurTime);
        //await TimerManager.Instance.Wait(5000);
        //Debug.Log("hello:" + TimeHelper.CurTime);
        ////TimerManager.Instance.Wait(5000,out id);
        ////Debug.Log("hello:" + TimeHelper.CurTime+"--->"+id);
        //TimerManager.Instance.Wait(3000);

    }

    int count = 20;
    private void Update()
    {
        ProfilerHelper.BeginSample("udpate1");
        tcs = new TaskCompletionSource<bool>();
        ProfilerHelper.EndSample();


    }
}

