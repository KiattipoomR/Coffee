using UnityEngine;

namespace Item
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "ItemData", menuName = "Items/General Item", order = 0)]
    public class ItemData : ScriptableObject
    {
        [SerializeField] private string id;
        [SerializeField] private string itemName;
        [SerializeField] private string description;

        public string ID => id;
        public string ItemName => itemName;
        public string Description => description;
    }
}
