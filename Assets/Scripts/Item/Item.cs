using UnityEngine;

namespace Item
{
    [System.Serializable]
    public class Item
    {
        [SerializeField] protected ItemData itemData;

        public ItemData ItemData => itemData;
        
        public Item(ItemData data)
        {
            itemData = data;
        }
    }
}
