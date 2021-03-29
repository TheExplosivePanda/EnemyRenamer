using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.IO;
using Ionic.Zip;

namespace EnemyRenamer
{
    public class Module : ETGModule
    {
        public static readonly string MOD_NAME = "Personalized Enemy Names mod";
        public static readonly string VERSION = "0.0.0";
        public static readonly string TEXT_COLOR = "#00FFFF";

        public override void Start()
        {
            PrefabDatabase.Instance.SuperReaper.gameObject.AddComponent<ReaperRenamer>();
            Larry.namesDB = LoadTxtFileFromLiterallyAnywhere("NamesDB.txt");
            ETGMod.AIActor.OnPostStart += GiveName;
            Log($"{MOD_NAME} v{VERSION} started successfully and will now commence ruining your day", TEXT_COLOR);
        }
        public void GiveName(AIActor fella)
        {
            fella.gameObject.AddComponent<Larry>();
        }
        public string[] LoadTxtFileFromLiterallyAnywhere(string name)
        {
            string[] strings = null;
            if (File.Exists(this.Metadata.Archive))
            {
                ZipFile ModZIP = ZipFile.Read(this.Metadata.Archive);
                if (ModZIP != null && ModZIP.Entries.Count > 0)
                {
                    foreach (ZipEntry entry in ModZIP.Entries)
                    {
                        if (entry.FileName == name)
                        {

                            using (MemoryStream ms = new MemoryStream())
                            {

                                entry.Extract(ms);
                                StreamReader reader = new StreamReader(ms);
                                ms.Seek(0, SeekOrigin.Begin);
                                List<string> stringList = new List<string>();
                                string str = reader.ReadLine();
                                while (str != null)
                                {
                                    stringList.Add(str);
                                    str = reader.ReadLine();
                                }
                                strings = stringList.ToArray();
                                break;
                            }
                        }
                    }
                }
            }
            else if (File.Exists(this.Metadata.Directory + "/" + name))
            {
                try
                {
                    strings = File.ReadAllLines(this.Metadata.Directory + "/" + name);
                }
                catch (Exception ex)
                {
                    Debug.LogError("Failed loading asset bundle from file.");
                    Debug.LogError(ex.ToString());

                }
            }
            else
            {
                Debug.LogError("Text file NOT FOUND!");
            }
            return strings;
        }

        public static void Log(string text, string color="#FFFFFF")
        {
            ETGModConsole.Log($"<color={color}>{text}</color>");
        }

        public override void Exit() { }
        public override void Init() { }
    }
}
