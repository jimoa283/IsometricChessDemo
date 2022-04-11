using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
using UnityEditor;

namespace KasperDev.DialogueEditor
{
    
    public static class Helper
    {
        //Old way of find Dialogue Containers.
        //This work with generic.
        //may work in run time need to check
        //TODO:check if it work in runtime at some point.
        public static List<T> FindAllObjectFromResources<T>()
        {
            List<T> tmp = new List<T>();
            string ResourcesPath = Application.dataPath + "/Resources";
            string[] directories = Directory.GetDirectories(ResourcesPath, "*", SearchOption.AllDirectories);

            foreach (string directorie in directories)
            {
                string directorPath = directorie.Substring(ResourcesPath.Length + 1);
                T[] result = Resources.LoadAll(directorPath, typeof(T)).Cast<T>().ToArray();

                foreach (T item in result)
                {
                    if (!tmp.Contains(item))
                    {
                        tmp.Add(item);
                    }
                }
            }

            return tmp;
        }

        /// <summary>
        /// Find all Dialogue Containers in Assets
        /// </summary>
        /// <returns>List of Dialogue Containers</returns>
        public static List<DialogueContainerSO> FindAllDialogueContainerSO()
        {
            //Find all the DialogueContainersSO in Assets and get it guid ID.
            string[] guids = AssetDatabase.FindAssets("t:DialogueContainerSO");

            //Make a Array as long as found DialogueContainerSO.
            DialogueContainerSO[] items = new DialogueContainerSO[guids.Length];

            for (int i = 0; i < guids.Length; i++)
            {
                string path = AssetDatabase.GUIDToAssetPath(guids[i]);
                items[i] = AssetDatabase.LoadAssetAtPath<DialogueContainerSO>(path);
            }

            return items.ToList();
        }
    }
}


