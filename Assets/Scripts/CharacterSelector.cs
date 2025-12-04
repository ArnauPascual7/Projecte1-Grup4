using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacterSelector : MonoBehaviour
{
    public List<GameObject> characters;

    public void ChangeCharacter(GameObject character)
    {
        characters.ForEach(c => c.SetActive(false));

        character.SetActive(true);
    }

    public void RandomCharacter()
    {
        int rand = Random.Range(0, characters.Count);

        characters.ForEach(c => c.SetActive(false));
        characters[rand].SetActive(true);
    }
}
