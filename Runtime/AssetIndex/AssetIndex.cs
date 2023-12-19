using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityUtility;
using static UnityEditor.Progress;

namespace AssetIndex
{
    public abstract class AssetIndex : ScriptableObject
    {
        protected abstract void Refresh();

#if UNITY_EDITOR

        [UnityEditor.MenuItem("Assets/Refresh Asset Indexes")]
        static void RefreshAll()
        {
            var indexList = Resources.LoadAll<AssetIndex>(string.Empty);

            foreach(var index in indexList)
                index.Refresh();
        }

#endif
    }

    public abstract class AssetIndex<ASSET, INDEX> : AssetIndex 
        where ASSET : UnityEngine.Object
        where INDEX : AssetIndex<ASSET,INDEX>
    {
        [SerializeField] string[] Paths;
        [SerializeField][ReadOnly] Item[] Items;

        [Serializable]
        class Item
        {
            public string Name;
            public ASSET Asset;
        }

#if UNITY_EDITOR

        protected override void Refresh()
        {
            var guids = UnityEditor.AssetDatabase.FindAssets("t:" + (typeof (ASSET)).Name, Paths);
            Items = new Item[guids.Length];

            for (var i = 0; i < guids.Length; i++)
            {
                var path = UnityEditor.AssetDatabase.GUIDToAssetPath(guids[i]);
                var asset = UnityEditor.AssetDatabase.LoadAssetAtPath<ASSET>(path);

                Items[i] = new Item()
                {
                    Name = asset.name,
                    Asset = asset
                };
            }

            UnityEditor.EditorUtility.SetDirty(this);
        }

#endif

        // STATIC

        static Dictionary<string, ASSET> Index;

        public static bool TryGet(string name, out ASSET result)
        {
            if (name == null)
            {
                result = null;
                return false;
            }

            if (Index == null)
            {
                Index = new Dictionary<string, ASSET>();
                var indexAssets = Resources.LoadAll<INDEX>(string.Empty);

                foreach (var indexAsset in indexAssets)
                {
                    foreach (var item in indexAsset.Items)
                    {
                        if (!Index.ContainsKey(item.Name))
                            Index.Add(item.Name, item.Asset);
                    }
                }
            }

            return Index.TryGetValue(name, out result);
        }
    }
}
