using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum IngredientType {
    Egg,
    FriedEgg,
    Potato,
    Zucchini,
    Butter,
    Lemon,
    Bun,
    Cheese,
    Hotdog,
    Ketchup,
    Mustard,
    Bacon,
    Sausage,
    Chicken,
    Shrimp,
    Crab,
    Mushroom,
    Lettuce,
    Bread,
    Tomato,
}

public enum CookingMethod {
    RawOrReadyToEat,
    ByOven,
    ByMicrowave,
    ByToaster,
    ByStove,
    Any,
}

public class Recipe : MonoBehaviour
{
    public List<Ingredient> ingredients;
    public string name;
    public GameObject finishedPrefab;
}
