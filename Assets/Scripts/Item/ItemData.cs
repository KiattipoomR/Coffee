using UnityEngine;

namespace Item
{
    [CreateAssetMenu(fileName = "ItemData", menuName = "Items/General Item", order = 0)]
    public class ItemData : ScriptableObject
    {
        [SerializeField] protected string id;
        [SerializeField] protected string itemName;
        [SerializeField] protected string description;
        [SerializeField] protected int maxStack;
        [SerializeField] protected float weight;
        [SerializeField] protected GameObject model;

        public string ID => id;
        public string ItemName => itemName;
        public string Description => description;
        public int MaxStack => maxStack;
        public float Weight => weight;
        public GameObject Model => model;
    }
}
