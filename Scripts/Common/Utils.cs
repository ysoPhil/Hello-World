namespace CardGameUIEffect
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;


    /// The Utils class
    public class Utils
    {
     
        /// Converts the angle to arc.

        /// <param name="angle">Angle
        /// <returns>Arc
        public static float ConvertAngleToArc(float angle)
        {
            return angle * Mathf.PI / 180;
        }


        /// Gets the angle by vector.
        /// <param name="len_x">Length x
        /// <param name="len_y">Length y
        /// <returns>Angle
        public static float GetAngleByVector(float len_x, float len_y)
        {
            if (len_y == 0)
            {
                if (len_x < 0)
                {
                    return 270;
                }
                else if (len_x > 0)
                {
                    return 90;
                }
                return 0;
            }
            if (len_x == 0)
            {
                if (len_y >= 0)
                {
                    return 0;
                }
                else if (len_y < 0)
                {
                    return 180;
                }
            }

            float angle = 0;
            if (len_y > 0 && len_x > 0)
            {
                angle = 270 + Mathf.Atan2(Mathf.Abs(len_y), Mathf.Abs(len_x)) * 180 / Mathf.PI;
            }
            else if (len_y > 0 && len_x < 0)
            {
                angle = 90 - Mathf.Atan2(Mathf.Abs(len_y), Mathf.Abs(len_x)) * 180 / Mathf.PI;
            }
            else if (len_y < 0 && len_x > 0)
            {
                angle = 270 - Mathf.Atan2(Mathf.Abs(len_y), Mathf.Abs(len_x)) * 180 / Mathf.PI;
            }
            else if (len_y < 0 && len_x < 0)
            {
                angle = Mathf.Atan2(Mathf.Abs(len_y), Mathf.Abs(len_x)) * 180 / Mathf.PI + 90;
            }
            return angle;
        }
    }
}