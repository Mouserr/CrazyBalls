using System;
using UnityEngine;

namespace Assets.Scripts.Core.Helpers
{
    public static class ColorHelper
    {
        public static Color SetAlpha(float value, Color color)
        {
            color.a = value;
            return color;
        }

	    public static Color AlphaBlend(Color foreground, Color background)
	    {
		    Color result;
		    float foreAlpha = foreground.a;

			result = foreground * foreAlpha + background * (1 - foreAlpha);
		    result.a = foreAlpha;

		    return result;
	    }

        public static Color ColorByCode(string colorCode, byte alpha = 255)
        {
            return new Color32(
                Convert.ToByte(colorCode.Substring(0, 2), 16),
                Convert.ToByte(colorCode.Substring(2, 2), 16),
                Convert.ToByte(colorCode.Substring(4, 2), 16),
                alpha
                );
        }
    }
}