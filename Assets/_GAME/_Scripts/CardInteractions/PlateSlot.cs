using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateSlot : CardSlot
{
    private List<FAttributeSetup> ingredientsUsed = new List<FAttributeSetup>();
    
    public void AddIngredient(FAttributeSetup ingredient)
    {
        for (int i = 0; i < ingredientsUsed.Count; i++)
        {
            if (ingredientsUsed[i].Attribute == ingredient.Attribute)
            {
                var newAttribute = ingredientsUsed[i];

                newAttribute.Value += ingredient.Value;

                ingredientsUsed[i] = newAttribute;
                return;
            }
        }
        ingredientsUsed.Add(ingredient);
    }

    public void ClearPlate()
    {
        Debug.Log("=== Clearing plate");
        while (Cards.Count > 0)
        {
            Debug.Log("a");
            Destroy(Cards[0].gameObject);
            Cards.RemoveAt(0);
        }
        
        ingredientsUsed.Clear();
    }
    
    protected override void OnCardAdded(CardActor card)
    {
        for (int i = 0; i < card.Attributes.Count; i++)
        {
            AddIngredient(card.Attributes[i]);
        }

        IsDishCorrect();
    }

    public bool IsDishCorrect()
    {
        return MatchManager.instance.currentStage.GetCurrentRound().roundRecipe.CheckRecipeComplete(ingredientsUsed);
    }
}
