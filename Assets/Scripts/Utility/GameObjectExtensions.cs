using UnityEngine;

namespace Utility
{
    public static class GameObjectExtensions
    {
        public static bool CheckLayerMask(this GameObject gameObject, LayerMask layerMask)
        {
            return (layerMask.value & 1 << gameObject.layer) != 0;
        }
    }
}