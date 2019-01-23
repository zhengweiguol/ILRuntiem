using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Hotfix_U3D
{
    public class Main
    {
        public static TestDelegate test;
        public static GameObject obj = new GameObject();
        Vector3 Vector3;
        public static void StartHotFix()
        {
            Debug.Log("开始热更程序");
            test += TestDelegate;
            test.Invoke(123);
            Init();
            Test.TestD(321);
            //obj.AddComponent<Test>();
            obj = GameObject.Find("111");
            Debug.Log(obj);
            Debug.Log(obj.GetComponent<Test>().a);
        }
        public static void Init()
        {
            Test.TestD += WTestDelegate;
        }
        static void WTestDelegate(int i)
        {
            Debug.Log("委托实力给外部"+i);
        }
        static void TestDelegate(int i)
        {
            Debug.Log("内部委托"+ 123);
        }

    }
}
