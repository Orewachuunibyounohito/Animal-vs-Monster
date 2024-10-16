using UnityEngine;

namespace MyScripts{
    public static class ValueMap
    {
        public static float Map( float oldValue, float originMin, float originMax, float newMin, float newMax, bool clamp ){
            float newValue;
            newValue = ( oldValue-originMin )/( originMax-originMin )*( newMax-newMin )+newMin;
            if( clamp ){
                newValue = Mathf.Clamp( newValue, newMin, newMax );
            }
            return newValue;
        }
    }
}
