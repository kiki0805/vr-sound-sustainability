using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredient : MonoBehaviour {
    public IngredientType ingredientType;
    public CookingMethod cookingMethod;
    public CookingMethod cookedBy = CookingMethod.RawOrReadyToEat;

    public string GetTipString() {
        return $"{ingredientType}\nCooking method: {cookingMethod}";
    }
}
