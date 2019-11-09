using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CreatorUtility;

public class Canvas_Script : MonoBehaviour
{
    // Start is called before the first frame update

    int
        stepCounter=0,
        jumpCounter=0,
        rotCounter=0;
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
    public void updateValues(int step, int rot,int jmp)
    {
        stepCounter -= step;
        rotCounter -= rot;
        jumpCounter -= jmp;

        //Debug.LogFormat("Values: {0}, stepCounter: {1}, rotCounter {2}, jumpCounter {3}", values, stepCounter, rotCounter, jumpCounter);
        //Debug.LogFormat("Values: {0}, values.x: {1}, values.y {2}, jumpCounter {3}", values, stepCounter, rotCounter, jumpCounter);

        stepCountText.text = stepCounter.ToString();
        rotCountText.text = rotCounter.ToString();
        jumpCountText.text = jumpCounter.ToString();






    }
}
