using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.IO;
using Ionic.Zip;

namespace EnemyRenamer
{
    public class NamerModuleClassic : ETGModule
    {
        public static readonly string MOD_NAME = "Personalized Enemy Names mod";
        public static readonly string VERSION = "0.0.0";
        public static readonly string TEXT_COLOR = "#00FFFF";

        public override void Start()
        {
            PrefabDatabase.Instance.SuperReaper.gameObject.AddComponent<ReaperRenamer>();
            ETGModConsole.Commands.AddGroup("namer:namesize", new Action<string[]>(this.ChangeNameSize));
            ETGModConsole.Commands.AddGroup("namer:addname", new Action<string[]>(this.AddNameManual));
            ETGModConsole.Commands.AddGroup("namer:opacityamount", new Action<string[]>(this.ChangeOpacityAmount));
            Larry.namesDB = LoadTxtFileFromLiterallyAnywhere("NamesDB.txt");
            ETGMod.AIActor.OnPostStart += GiveName;
            Log($"{MOD_NAME} v{VERSION} started successfully and will now commence ruining your day", TEXT_COLOR);
        }

        public static event Action onNameSizeChanged;
        public void ChangeNameSize(string[] args)
        {
            if (args != null && args.Length == 1)
            {
                float size = 1;
                bool success = float.TryParse(args[0], out size);
                if (success)
                {

                    if (size >= 0)
                    {
                        Larry.nameSize = size;
                        ETGModConsole.Log("names from now on will be size = " + Larry.nameSize);
                        onNameSizeChanged();
                    }
                    else
                    {
                        ETGModConsole.Log("size must be a positive number");
                    }
                }
                else
                {
                    ETGModConsole.Log("incorrect format, make sure to input a number");
                }
            }
            else
            {
                ETGModConsole.Log("incorrect amount of arguments. one argument required");
            }
        }
        public static event Action onOpacityAmountChanged;
        public void ChangeOpacityAmount(string[] args)
        {
            if (args != null && args.Length == 1)
            {
                float opacity = 0.9f;
                bool success = float.TryParse(args[0], out opacity);
                if (success)
                {

                    if (opacity >= 0 && opacity <= 1)
                    {
                        Larry.opacityAmount = opacity;
                        ETGModConsole.Log("names opacity amount will now be " + Larry.opacityAmount);
                        onOpacityAmountChanged();
                    }
                    else
                    {
                        ETGModConsole.Log("opacity must be between 0 and 1!");
                    }
                }
                else
                {
                    ETGModConsole.Log("incorrect format, make sure to input a number");
                }
            }
            else
            {
                ETGModConsole.Log("incorrect amount of arguments. one argument required");
            }
        }

        public void AddNameManual(string[] args)
        {
            if (args != null && args.Length > 0)
            {
                string name = "";
                for (int i = 0; i < args.Length; i++)
                {
                    name = name + args[i];
                }
                if (!Larry.namesDB.Contains(name))
                {
                    Larry.namesDB.Add(name);
                    ETGModConsole.Log("the name: " + name + " was added to the name database");
                }
                else
                {
                    ETGModConsole.Log("the name: " + name + " alraedy exists in the database!");
                }
            }
            else
            {
                ETGModConsole.Log("incorrect amount of arguments. at least one argument required");
            }

        }

        public void GiveName(AIActor fella)
        {
            fella.gameObject.AddComponent<Larry>();
        }
        public List<string> LoadTxtFileFromLiterallyAnywhere(string name)
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
            return strings.ToList<string>();
        }

        public static void Log(string text, string color="#FFFFFF")
        {
            ETGModConsole.Log($"<color={color}>{text}</color>");
        }

        public override void Exit() { }
        public override void Init() { }
    }
}
