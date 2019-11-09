using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CreatorUtility;

public class Canvas_Script : MonoBehaviour
{
    // Start is called before the first frame update

    int
        stepCounter,
        jumpCounter,
        rotCounter;
    Text 
        stepCountText,
        rotCountText,
        jumpCountText;

    UtilityOne ugo = new UtilityOne();

    void Start()
    {
        stepCountText = ugo.FindTransform("StepCounterText", transform).GetComponent<Text>();
        rotCountText = ugo.FindTransform("RotCounterText", transform).GetComponent<Text>();
        jumpCountText = ugo.FindTransform("JumpCounterText", transform).GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Обновляет значения в скрипте для счетчиков шагов, вращения и прыжков
    /// </summary>
    /// <param name="values">x-шаги, y-вращение, z-прыжки</param>
    public void updateValues(Vector3Int values)
    {
        stepCounter -= values.x;
        rotCounter -= values.y;
        jumpCounter -= values.z;

        stepCountText.text = stepCounter.ToString();
        rotCountText.text = rotCounter.ToString();
        jumpCountText.text = jumpCounter.ToString();






    }
}
