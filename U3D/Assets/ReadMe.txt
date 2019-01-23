如果调用 ILRuntime/Generate CLR Binding Code by Analysis 功能
系统回自动覆盖UnityEngine_Debug_Binding文件，此时自己添加的堆栈信息会被覆盖
所以需要自动添加

        var stacktrace = __domain.DebugService.GetStackTrance(__intp);

        UnityEngine.Debug.Log(message + "\n" + stacktrace);