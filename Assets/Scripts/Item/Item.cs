using UnityEngine;

namespace Item
{
    public class Item : MonoBehaviour
    {
        [SerializeField] protected ItemData itemData;

        private ItemData ItemData => itemData;
        
        public void Init(ItemData data)
        {
            itemData = data;
        }
    }
}
