using System.Collections.Generic;
using System.Text;
using Scripts.Items;
using UnityEngine;

namespace Scripts.Rules
{
    [CreateAssetMenu(menuName = "Rule/Must Not Touch")]
    public class MustNotTouchRule : BaseRule
    {
        [SerializeField] private List<ItemType> itemTypes;
        
        public override bool Evaluate()
        {
            foreach (var itemType in itemTypes)
            {
                var items = Room.GetItemsInRoom(itemType);

                foreach (var item in items)
                {
                    var surroundingPositions = item.GetSurroundingPositions(Room);

                    foreach (var surroundingPosition in surroundingPositions)
                    {
                        if (!surroundingPosition.IsTaken)
                            continue;

                        if (itemTypes.Contains(surroundingPosition.Item.Type))
                            return false;
                    }
                }
            }

            return true;
        }
        
        public override string GetRuleAddedDescription()
        {
            var sb = new StringBuilder();

            sb.Append("All ");

            for (var index = 0; index < itemTypes.Count; index++)
            {
                var itemType = itemTypes[index];

                if (itemTypes.Count == 1)
                {
                    sb.Append(itemType);
                }
                else if (index != itemTypes.Count - 1)
                {
                    sb.Append(itemType + "s, ");
                }
                else
                {
                    sb.Append("and " + itemType + "s");
                }
                
            }

            sb.Append(" must not be next to each other.");
            
            return sb.ToString();
        }
        
        public override string GetRuleRemovedDescription()
        {
            var sb = new StringBuilder();

            sb.Append("All ");

            for (var index = 0; index < itemTypes.Count; index++)
            {
                var itemType = itemTypes[index];

                if (index != itemTypes.Count - 1)
                {
                    sb.Append(itemType + "s, ");
                }
                else
                {
                    sb.Append("and " + itemType + "s");
                }
                
            }

            sb.Append(" can now touch each other again (but they don't have to).");
            
            return sb.ToString();
        }
    }
}