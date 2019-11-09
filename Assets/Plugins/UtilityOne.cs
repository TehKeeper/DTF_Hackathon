using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;



//***********************************//
//******** THE TRUE AND ONLY ********//
//***********************************//
//************* VER 1.1 *************//
//***********************************//
namespace CreatorUtility
{
    public class UtilityOne
    {
        Sprite[] hpSprites;

        public Transform FindTransform(string name, Transform parent)
        {
            if (parent.name.Equals(name)) return parent;
            foreach (Transform child in parent)
            {
                Transform result = FindTransform(name, child);
                if (result != null)
                    return result;
            }
            return null;
        }

        public void loadControls(Dictionary<string, KeyCode> keyBindings, Dictionary<string, KeyCode> keyBindingsSec)
        {

            if (!PlayerPrefs.HasKey("KeyBindingsPrimary"))
                return;

            keyBindings.Clear();
            keyBindingsSec.Clear();

            string keysLine = PlayerPrefs.GetString("KeyBindingsPrimary");
            string keysLineSec = PlayerPrefs.GetString("KeyBindingsSecondary");
            string[] lines = keysLine.Split('\n'), linesSec = keysLineSec.Split('\n');
            string[] key_value, key_value_sec;

            //string msgPrim = "Controls\n";
            //string msgSec = "Secondary Controls\n";

            for (int i = 0; i < lines.GetLength(0); i++)
            {
                key_value = lines[i].Split('_');
                key_value_sec = linesSec[i].Split('_');
                keyBindings.Add(key_value[0], (KeyCode)System.Enum.Parse(typeof(KeyCode), key_value[1]));
                keyBindingsSec.Add(key_value_sec[0], (KeyCode)System.Enum.Parse(typeof(KeyCode), key_value_sec[1]));
                //msgPrim += key_value[0] + ": " + key_value[1] + " | " + key_value_sec[0] + ": " + key_value_sec[1] + "\n";
            }

            //print(msgPrim);
        }

        public string coloredText(string text, string color)
        {
            return "<color=#" + color + ">" + text + "</color>";
        }

        

        public void dashLiner(Vector3 start, Vector3 end)
        {
            Vector3 delta = (end - start) / 100;
            Color clr = Color.white;
            for (int i = 0; i < 100; i++)
            {
                if (i % 2 == 0)
                {
                    Debug.DrawLine(start + i * delta, end - (99 - i) * delta, Color.white);
                }
                else
                {
                    Debug.DrawLine(start + i * delta, end - (99 - i) * delta, Color.grey);
                }

                
            }
        }

        public void dashLiner(Vector3 start, Vector3 end, Color startColor)
        {
            Vector3 delta = (end - start) / 100;
            Color clr = startColor;
            Color clr2 = colorChanger(clr);

            for (int i = 0; i < 100; i++)
            {
                if (i % 2 == 0)
                {
                    Debug.DrawLine(start + i * delta, end - (99 - i) * delta, clr);
                }
                else
                {
                    Debug.DrawLine(start + i * delta, end - (99 - i) * delta, clr2);
                }
            }
        }

        Color colorChanger(Color clr)
        {
            /*
            float minVal = Math.Max(clr.r, clr.g);
            minVal = Math.Max(minVal, clr.b);
            */

            float mp = 0.75f;

            if (clr.grayscale < 0.5)
                mp = 1.25f;


            return new Color(Mathf.Clamp01(clr.r * mp), Mathf.Clamp01(clr.g * mp), Mathf.Clamp01(clr.g * mp));

        }

        public int maxArrayValue(float[] array)
        {
            if (array.GetLength(0) == 0)
                return 0;

            int ret = 0;
            float mv = array[0];
            for (int i = 1; i < array.GetLength(0); i++)
            {
                if (mv < array[i])
                {
                    ret = i;
                    mv = array[i];
                }

            }

            return ret;

        }


        /// <summary>
        /// Checks if current scene is not Logo, Hangar or Loading Screen
        /// </summary>
        /// <param name="currentSceneName">Name of current scene</param>
        /// <returns></returns>
        public bool sceneChecker(string currentSceneName)
        {
            if (currentSceneName != "Assembley_Hangar" && currentSceneName != "MechApoc_Logo" && currentSceneName != "Loading_Screen")
                return true;
            else
                return false;
        }



        float randa_Hole(float range, float hole)
        {
            float a = UnityEngine.Random.Range(-range, range);
            if (Mathf.Abs(a) > hole)
                return a;
            else return (randa_Hole(range, hole));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a">Current Angle</param>
        /// <param name="b">Target Angle</param>
        /// <param name="speed"></param>
        /// <returns></returns>
        float clampedSpeed(float a, float b, float speed)
        {

            float ret = 0;
            //int debugCode = -1;


            if (a == b||(a==0&&b==360)||(a==360&&b==0))
                return 0;

            if (b < 0)
                b += 360;

            if ((a <= 180 && b <= 180) || (a > 180 && b > 180))
            {
                //debugCode = 1;
                ret= Mathf.Clamp(Mathf.Abs(a - b), 0, speed);
            }
            else if (a <= 180 && b > 180)
            {
                //debugCode = 2;

                ret = Mathf.Clamp(Mathf.Abs(b - (a+360)), 0, speed);
            }
            else if(a > 180 && b <= 180)
            {
                //debugCode = 3;

                ret = Mathf.Clamp(Mathf.Abs((b+360)-a), 0, speed);
            }

            //Debug.Log("Clamped speed=" + ret + ", angleCurrent=" + a.ToString() + ", angleTarget=" + b.ToString() + ", branch No: " + debugCode);

            return ret;
           


        }

        /// <summary>
        /// Calculate delta for angular movement between [0;360]
        /// </summary>
        /// <param name="values">X - current Angle, Y - target Angle, Z - speed</param>
        /// <returns></returns>
        float clampedSpeed(Vector3 values)
        {
            return clampedSpeed(values.x, values.y, values.z);
        }
        

        /// <summary>
        /// Smooth angular rotation with msximum speed
        /// </summary>
        /// <returns></returns>
        public float angularRotation(float angleCurrent, float angleTarget, float speed)
        {
           
            float ret = angleCurrent;
            if (angleTarget < 0)
                angleTarget += 360;

            Vector3 vls = new Vector3(angleCurrent, angleTarget, speed);
            speed = clampedSpeed(vls);

            

            if ((angleCurrent <= 180 && angleTarget <= 180) || (angleCurrent > 180 && angleTarget > 180))
            {
                if (angleCurrent < angleTarget)
                    ret += speed;
                else if(angleCurrent > angleTarget)
                    ret -= speed;
            }
            else if (angleCurrent <= 180 && angleTarget > 180)
            {

                ret -= speed;
            }
            else if(angleCurrent > 180 && angleTarget <= 180)
            {
                ret += speed;
            }


            //Debug.Log(coloredText("   Angular Rotation Debug:\n", "20f000")+"Angle Current="+angleCurrent+", AngleTarget="+angleTarget+", speed="+speed+", angleRet="+ret);

            return ret;
        }

        public float freeAngularRotation(float angleCurrent, float angleTarget, float speed)
        {
            float ret = angleCurrent;
            float angleTemp = angleCurrent;
            if (angleCurrent == angleTarget)
                return ret;

            speed = clampedSpeed(angleCurrent, angleTarget, speed);

            if (angleCurrent < 180)
            {


                if (angleCurrent < (angleTarget) && angleCurrent+180 >= angleTarget)
                {
                    //print("Angle Current=" + angleCurrent+", speed="+speed);
                    ret += speed;


                }
                else
                {
                    ret -= speed;
                }


            }
            else
            {

                if (angleCurrent > angleTarget && angleCurrent-180 <= angleTarget)
                {
                    ret -= speed;
                }
                else
                {
                    ret += speed;
                }
            }

            //Debug.Log(coloredText("Free rotation speed=" + speed + ", current Angle=" + angleCurrent + ", targetAngle=" + angleTarget+", ret=" +ret,"0020c0"));





            return ret;
        }

        public float freeAngularRotation(float angleCurrent, float angleTarget, float speed, ref float speedDelta)
        {
            float ret = angleCurrent;
            float angleTemp = angleCurrent;
            if (angleCurrent == angleTarget)
            {
                speedDelta = 0;
                return ret;
            }

            speed = clampedSpeed(angleCurrent, angleTarget, speed);
            //Debug.Log("angleCurrent=" + angleCurrent + ", angleTarget=" + angleTarget + ", speed=" + speed);
            speedDelta = Mathf.Abs(speed);

            if (angleCurrent < 180)
            {


                if (angleCurrent < (angleTarget) && angleCurrent + 180 >= angleTarget)
                {
                    //print("Angle Current=" + angleCurrent+", speed="+speed);
                    ret += speed;


                }
                else
                {
                    ret -= speed;
                    //Debug.Log("Angle Current=" + angleCurrent + ", speed=" + speed+", ret="+ret);
                }


            }
            else
            {

                if (angleCurrent > angleTarget && angleCurrent - 180 <= angleTarget)
                {
                    ret -= speed;
                }
                else
                {
                    ret += speed;
                }
            }

            //Debug.Log(coloredText("Free rotation speed=" + speed + ", current Angle=" + angleCurrent + ", targetAngle=" + angleTarget+", ret=" +ret,"0020c0"));





            return ret;
        }

        public void addUniqueToList<T>(T obj, ref List<T> originalList)
        {
            if (obj == null)
                return;

            if (!originalList.Contains(obj))
            {
                originalList.Add(obj);
                //Debug.Log("Object " + obj + " Added to List");
            }
        }

        public string splitThouzands(int number)
        {
            string ret = "";
            if (number / 1000 < 0)
                return number.ToString();

            List<int> part = new List<int>();

            //int iter = 1;

            do
            {
                int a1 = number % 1000;
                part.Add(a1);
                number -= a1;
                number =(int)(number / 1000);
                //iter++;
            }
            while (number > 0);


            string debugMsg = "";

            ret += "" + part[part.Count - 1];

            for (int i = part.Count-2; i >=0; i--)
            {
                ret += " " + part[i];
            }

            return ret;
        }

        /*
        public string splitThouzands(string number)
        {
            string divider = ".";

            char[] nmb = number.ToArray<char>();
            
            

            string ret = "", pt="";
            List<string> retParts = new List<string>();

            int counter = 0;

            for (int i = nmb.GetLength(0)-1; i >=0; i--)
            {
                
                pt += nmb[i];
                counter++;
                if (counter > 2||i==0)
                {
                    //Debug.Log("Pt=" + pt);
                    retParts.Add(pt);
                    pt = "";
                    counter = 0;
                }
            }

            for (int i = retParts.Count - 1; i >= 0; i--)
            {
                ret += retParts[i] + (i != 0 ? divider : "");
                //Debug.Log("retParts[i]" + retParts[i]);
            }

            //Debug.Log("TD ret=" + ret);

            return ret;
        }
        */

        public string splitThouzands(string number)
        {
            char[] num = number.ToArray<char>();
            int endDigits = num.GetLength(0) % 3;
            string ret = "", divider = " ";

            for (int i = 0; i < num.GetLength(0); i++)
            {
                if (endDigits > 0)
                {
                    ret += num[i];
                    endDigits--;
                }
                else
                {
                    if (i != num.GetLength(0) - 1)
                    {
                        ret += divider;
                        endDigits = 3;
                        i--;
                    }
                }
            }

            return ret;
        }

        public void arrayToPlayerPrefs(string[] array, string divider, string prefsName)
        {
            string writeString = "";

            for (int i = 0; i < array.GetLength(0); i++)
            {
                writeString += array[i];
                writeString += i != array.GetLength(0) - 1 ? divider : "";
            }

            PlayerPrefs.SetString(prefsName, writeString);


        }

        public void arrayToPlayerPrefs(int[] array, string divider, string prefsName)
        {
            string writeString = "";

            for (int i = 0; i < array.GetLength(0); i++)
            {
                writeString +=""+ array[i];
                writeString += "" + (i != array.GetLength(0) - 1 ? divider : "");
            }

            PlayerPrefs.SetString(prefsName, writeString);


        }

        public string arrayToString<T>(T[] array, string divider)
        {
            string writeString = "";

            for (int i = 0; i < array.GetLength(0); i++)
            {
                writeString += array[i];
                writeString += i != array.GetLength(0) - 1 ? divider : "";
            }

            return writeString;

            //PlayerPrefs.SetString(prefsName, writeString);


        }

        public Vector2 angleToScalar(float angle)
        {
            return new Vector2(Mathf.Cos(Mathf.Deg2Rad * angle), Mathf.Sin(Mathf.Deg2Rad * angle));
        }

        public Vector3 angleToScalar3(float angle)
        {
            return new Vector3(Mathf.Cos(Mathf.Deg2Rad * angle),0, Mathf.Sin(Mathf.Deg2Rad * angle));
        }

        public Sprite retHardSprite(string hpName)
        {

            Sprite[] hpSprites =  Resources.LoadAll<Sprite>("Sprites/GUI/Images/Hardpoints_Icons_Sheet_2");

            Sprite hpSprite = hpSprites[0];
            switch (hpName)
            {
                case "Body":
                    {
                        hpSprite = hpSprites[1];
                    }
                    break;

                case "Shoulder":
                    {
                        hpSprite = hpSprites[2];
                    }
                    break;

                case "Arm":
                    {
                        hpSprite = hpSprites[3];
                    }
                    break;
                case "Ballistic":
                    {
                        hpSprite = hpSprites[4];
                    }
                    break;
                case "Laser":
                    {
                        hpSprite = hpSprites[5];
                    }
                    break;

                case "Rocket":
                    {
                        hpSprite = hpSprites[6];
                    }
                    break;

                case "Minelayer":
                    {
                        hpSprite = hpSprites[10];
                    }
                    break;

                case "DroneSpawner":
                    {
                        hpSprite = hpSprites[15];
                    }
                    break;




            }

            return hpSprite;
        }

        public string hpNameChanging(string original)
        {

            string ret = original;
            switch (original)
            {
                case "Ballistic":
                    ret = "Firearms";
                    break;
                case "Rocket":
                    ret = "Rocket Launcher";
                    break;
                case "Laser":
                    ret = "Beam";
                    break;

                case "DroneSpawner":
                    ret = "Drone Spawner";
                    break;
            }


            return ret;
        }

        public int[] returnRandomIds(int idsToReturn, int maxValue)
        {
            int[] ret = new int[idsToReturn];
            int retI = 0,missed= maxValue - idsToReturn+1;
            for (int i = 0; i < maxValue; i++)
            {
                bool rndVal = UnityEngine.Random.Range(0,Mathf.Clamp(missed,1,maxValue)) <1;
                if (rndVal)
                {
                    ret[retI] = i;
                    retI++;
                    if (retI == idsToReturn)
                        break;
                }
                else
                    missed--;
            }

            
            string debugMsg = "Output random IDs: [";
            for (int i = 0; i < idsToReturn; i++)
            {
                debugMsg += ret[i] + ((i == idsToReturn - 1) ? "]" : ",");
            }
            //Debug.Log(debugMsg);
            

            return ret; 

        }
    }



}



