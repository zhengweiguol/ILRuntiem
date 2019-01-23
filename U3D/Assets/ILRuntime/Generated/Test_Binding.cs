using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;

using ILRuntime.CLR.TypeSystem;
using ILRuntime.CLR.Method;
using ILRuntime.Runtime.Enviorment;
using ILRuntime.Runtime.Intepreter;
using ILRuntime.Runtime.Stack;
using ILRuntime.Reflection;
using ILRuntime.CLR.Utils;

namespace ILRuntime.Runtime.Generated
{
    unsafe class Test_Binding
    {
        public static void Register(ILRuntime.Runtime.Enviorment.AppDomain app)
        {
            BindingFlags flag = BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly;
            FieldInfo field;
            Type[] args;
            Type type = typeof(global::Test);

            field = type.GetField("TestD", flag);
            app.RegisterCLRFieldGetter(field, get_TestD_0);
            app.RegisterCLRFieldSetter(field, set_TestD_0);
            field = type.GetField("a", flag);
            app.RegisterCLRFieldGetter(field, get_a_1);
            app.RegisterCLRFieldSetter(field, set_a_1);


        }



        static object get_TestD_0(ref object o)
        {
            return global::Test.TestD;
        }
        static void set_TestD_0(ref object o, object v)
        {
            global::Test.TestD = (global::TestDelegate)v;
        }
        static object get_a_1(ref object o)
        {
            return ((global::Test)o).a;
        }
        static void set_a_1(ref object o, object v)
        {
            ((global::Test)o).a = (System.Int32)v;
        }


    }
}
