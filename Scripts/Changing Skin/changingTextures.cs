using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class changingTextures : MonoBehaviour
{
    public Material maleLight; //Reference to pale skinned male material
    public Material maleTanned; //Reference to tanned skinned male material
    public Material maleDark; //Reference to dark skinned male material
    public Material armyMaleLight; //Reference to army pale skinned male material
    public Material armyMaleTanned; //Reference to army tanned skinned male material
    public Material armyMaleDark; //Reference to army dark skinned male material
    public Material femaleLight; //Reference to pale skinned female material
    public Material femaleTanned; //Reference to tanned skinned female material
    public Material femaleDark; //Reference to dark skinned female material
    public Material ArmyFemaleLight; //Reference to army pale skinned female material
    public Material ArmyFemaleTanned; //Reference to army tanned skinned female material
    public Material ArmyFemaleDark; //Reference to army dark skinned female material

    public Transform dropdownMenu; //Reference to the transform of the dropdownmenu in the options, display menu

    Renderer renderer1; //Reference to arms renderer
   
    void Start()
    {
        //Setting up reference
        renderer1 = GetComponent<Renderer>();
    }

    void Update()
    {
        if (dropdownMenu.GetComponent<Dropdown>().value == 0) //If the first option on the drop down menu is selected then...
        {
            renderer1.material = maleLight; //Change the players skin to pale skinned male
        }
        if (dropdownMenu.GetComponent<Dropdown>().value == 1) //If the second option on the drop down menu is selected then...
        {
            renderer1.material = maleTanned; //Change the players skin to tanned skinned male
        }
        if (dropdownMenu.GetComponent<Dropdown>().value == 2) //If the third option on the drop down menu is selected then...
        {
            renderer1.material = maleDark; //Change the players skin to dark skinned male
        }
        if (dropdownMenu.GetComponent<Dropdown>().value == 3) //If the forth option on the drop down menu is selected then...
        {
            renderer1.material = armyMaleLight; //Change the players material to army pale skinned male
        }
        if (dropdownMenu.GetComponent<Dropdown>().value == 4) //If the fifth option on the drop down menu is selected then...
        {
            renderer1.material = armyMaleTanned; //Change the players material to army tanned skinned male
        }
        if (dropdownMenu.GetComponent<Dropdown>().value == 5) //If the sixth option on the drop down menu is selected then...
        {
            renderer1.material = armyMaleDark; //Change the players material to army dark skinned male
        }
        if (dropdownMenu.GetComponent<Dropdown>().value == 6) //If the seventh option on the drop down menu is selected then...
        {
            renderer1.material = femaleLight; //Change the players skin to pale skinned female
        }
        if (dropdownMenu.GetComponent<Dropdown>().value == 7) //If the eigth option on the drop down menu is selected then...
        {
            renderer1.material = femaleTanned; //Change the players skin to tanned skinned female
        }
        if (dropdownMenu.GetComponent<Dropdown>().value == 8) //If the ninth option on the drop down menu is selected then...
        {
            renderer1.material = femaleDark; //Change the players skin to dark skinned female
        }
        if (dropdownMenu.GetComponent<Dropdown>().value == 9) //If the tenth option on the drop down menu is selected then...
        {
            renderer1.material = ArmyFemaleLight; //Change the players material to army pale skinned female
        }
        if (dropdownMenu.GetComponent<Dropdown>().value == 10) //If the eleventh option on the drop down menu is selected then...
        {
            renderer1.material = ArmyFemaleTanned; //Change the players material to army tanned skinned female
        }
        if (dropdownMenu.GetComponent<Dropdown>().value == 11) //If the twelevth option on the drop down menu is selected then...
        {
            renderer1.material = ArmyFemaleDark; //Change the players material to army dark skinned female
        }
    }    
}
