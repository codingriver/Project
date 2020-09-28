using UnityEngine.Profiling;

public static class ProfilerHelper
{

    public static bool UseProfiler = true;
    public static bool UseStringFormatProfiler = true;

    public static void BeginSample(string name)
    {
        if (!UseProfiler)
            return;

        Profiler.BeginSample(name);
    }
    public static void BeginSample(string name,params object[] param)
    {
        if (!UseProfiler)
            return;
        if (!UseStringFormatProfiler)
            return;

        name = string.Format(name, param);
        Profiler.BeginSample(name);
    }



    public static void EndSample()
    {
        if (!UseProfiler)
            return;
        Profiler.EndSample();
    }

}
