Shader "Custom/AlwaysVisibleShader"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
    }
    SubShader
    {
        Tags { "Queue"="Overlay" } // Рендер вище за все
        Pass
        {
            Cull Off         // Відображення з обох сторін
            ZWrite Off       // Не записує в Z-буфер
            ZTest Always     // Ігнорує перевірку Z-буфера

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata_t
            {
                float4 vertex : POSITION;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
            };

            v2f vert (appdata_t v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                return o;
            }

            fixed4 frag () : SV_Target
            {
                return fixed4(1, 0, 0, 0.1); // Червоний колір з прозорістю 50%
            }
            ENDCG
        }
    }
}
