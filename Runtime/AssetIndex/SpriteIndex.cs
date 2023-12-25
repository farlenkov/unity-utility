#if UNITY_2017_1_OR_NEWER

using UnityEngine;

namespace AssetIndex
{
    [CreateAssetMenu]
    public class SpriteIndex : AssetIndex<Sprite, SpriteIndex>
    {

#if UNITY_EDITOR

        [ContextMenu("Refresh")]
        protected override void Refresh()
        {
            base.Refresh();
        }

#endif

    }
}

#endif