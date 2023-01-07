using System;

namespace Assets.Game.Scripts.Items
{
    [Serializable]
    public struct ItemMergeRecipe
    {
        public Item item1;
        public Item item2;
        public Item result;
    }
}