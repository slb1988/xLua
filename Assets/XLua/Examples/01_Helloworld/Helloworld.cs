/*
 * Tencent is pleased to support the open source community by making xLua available.
 * Copyright (C) 2016 THL A29 Limited, a Tencent company. All rights reserved.
 * Licensed under the MIT License (the "License"); you may not use this file except in compliance with the License. You may obtain a copy of the License at
 * http://opensource.org/licenses/MIT
 * Unless required by applicable law or agreed to in writing, software distributed under the License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. See the License for the specific language governing permissions and limitations under the License.
*/

using System;
using System.IO;
using UnityEngine;
using XLua;

namespace XLuaTest
{
    public class Helloworld : MonoBehaviour
    {
        public TextAsset ta;

        public byte[] RSACheckContent(byte[] data)
        {
            byte[] filecontent = new byte[data.Length - 128];
            Array.Copy(data, 128, filecontent, 0, filecontent.Length);
            return filecontent;
        }
        // Use this for initialization
        void Start()
        {
            LuaEnv luaenv = new LuaEnv();

            Debug.Log("lua start");
            var luadatas = RSACheckContent(ta.bytes);
            byte[] luascript_magickeys = new byte[] { 47, 79, 101, 157 };
            for (int i = 0; i < luadatas.Length; i++)
            {
                luadatas[i] ^= luascript_magickeys[i % 4];
            }

            Debug.Log("lua dostring");
            luaenv.DoString(luadatas, "battle_lua");
            
            Debug.Log("lua end");
            //luaenv.DoString("CS.UnityEngine.Debug.Log('hello world')");
            luaenv.Dispose();
            luaRight = true;
        }


        bool luaRight = false;
        private void OnGUI()
        {
            if (luaRight)
            {
                GUILayout.Button("hahaha");
            }
        }
    }
}
