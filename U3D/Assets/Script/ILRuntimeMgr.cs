using ILRuntime.Runtime.Generated;
using ILRuntime.Runtime.Intepreter;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ILRuntimeMgr : MonoBehaviour {

    ILRuntime.Runtime.Enviorment.AppDomain appdomain;
    // Use this for initialization
    void Start () {
        StartCoroutine(LoadILRuntime());
    }

    IEnumerator LoadILRuntime()
    {
        appdomain = new ILRuntime.Runtime.Enviorment.AppDomain();
#if UNITY_ANDROID
    WWW www = new WWW(Application.streamingAssetsPath + "/Hotfix.dll");
#else
        WWW www = new WWW("file:///" + Application.streamingAssetsPath + "/Hotfix_U3D.dll");
#endif
        while (!www.isDone)
            yield return null;
        if (!string.IsNullOrEmpty(www.error))
            Debug.Log(www.error);
        byte[] dll = www.bytes;
        www.Dispose();
#if UNITY_ANDROID
    www = new WWW(Application.streamingAssetsPath + "/Hotfix.pdb");
#else
        www = new WWW("file:///" + Application.streamingAssetsPath + "/Hotfix_U3D.pdb");
#endif
        while (!www.isDone)
            yield return null;
        if (!string.IsNullOrEmpty(www.error))
            Debug.Log(www.error);
        byte[] pdb = www.bytes;
        using (System.IO.MemoryStream fs = new MemoryStream(dll))
        {
            using (System.IO.MemoryStream p = new MemoryStream(pdb))
            {
                appdomain.LoadAssembly(fs, p, new Mono.Cecil.Pdb.PdbReaderProvider());
            }
        }
        init();
        OnILRuntimeInitialized();
    }

    void init()
    {
        //值类型绑定
        appdomain.RegisterValueTypeBinder(typeof(Vector3), new Vector3Binder());
        appdomain.RegisterValueTypeBinder(typeof(Quaternion), new QuaternionBinder());
        appdomain.RegisterValueTypeBinder(typeof(Vector2), new Vector2Binder());
        //委托绑定
        appdomain.DelegateManager.RegisterMethodDelegate<System.Int32>();
        appdomain.DelegateManager.RegisterDelegateConvertor<TestDelegate>((act) =>
        {
            return new TestDelegate((intarg) =>
            {
                ((Action<System.Int32>)act)(intarg);
            });
        });
        //重定向
        CLRBindings.Initialize(appdomain);
        LitJson.JsonMapper.RegisterILRuntimeCLRRedirection(appdomain);

    }
    void OnILRuntimeInitialized()
    {
        appdomain.Invoke("Hotfix_U3D.Main", "StartHotFix", null, null);
    } 
    
    public static void MethodException(ILIntepreter intepreter, Exception ex, ILRuntime.Runtime.Enviorment.AppDomain appdomain)
    {
        if (!(ex is ILRuntimeException))
        {
            if (appdomain != null && appdomain.DebugService != null)
            {
                Debug.LogError("[ILR Error]:" + ex.Message + "\t\t" + appdomain.DebugService.GetStackTrance(intepreter) + "\n");
            }
        }
    }
}
