﻿using System;
using UnityEditor;
using UnityEngine;
using CSObjectWrapEditor;
using XLua;

public static class BuildFromCLI
{
    /// <summary>
    /// 此方法通过Unity菜单调用。
    /// </summary>
    [MenuItem("XLua/Examples/13_BuildFromCLI")]
    public static void BuildFromUnityMenu()
    {
        var outputDir = Application.dataPath.Substring(0, Application.dataPath.Length - "/Assets".Length) + "/output";
        var packageName = "xLuaGame.exe";
        build(outputDir, packageName);
    }

    /// <summary>
    /// 此方法通过命令行调用。
    /// </summary>
    public static void Build()
    {
        var outputDir = Environment.GetCommandLineArgs()[0];
        var packageName = "xLuaGame.exe";
        build(outputDir, packageName);
    }

    private static void build(string outputDir,string packageName)
    {
        Debug.Log("构建开始：输出目录 " + outputDir);
        DelegateBridge.Gen_Flag = true;
        Generator.ClearAll();
        Generator.GenAll();
        var options = new BuildPlayerOptions
        {
            target = BuildTarget.StandaloneWindows64,
            targetGroup = BuildTargetGroup.Standalone,
            locationPathName = string.Format("{0}/{1}", outputDir, packageName)
        };
        BuildPipeline.BuildPlayer(options);
        Debug.Log("构建完成");
    }
}
