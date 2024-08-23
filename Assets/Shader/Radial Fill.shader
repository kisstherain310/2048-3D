Shader "Custom/RadialFillClockwise"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Base (RGB)", 2D) = "white" {}
        _Cutoff ("Fill Amount", Range(0,1)) = 1.0
    }
    SubShader
    {
        Tags { "Queue"="Transparent" }
        LOD 200
        Blend SrcAlpha OneMinusSrcAlpha
        ZWrite Off

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _Cutoff;
            float4 _Color;

            v2f vert (appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float2 center = float2(0.5, 1);
                float2 uv = i.uv - center;
                float angle = atan2(uv.y, uv.x) / (2 * 3.14159265) + 0.5;
                float dist = length(uv);
                float countdown = step(angle, _Cutoff);

                fixed4 col = tex2D(_MainTex, i.uv) * _Color;
                return col * countdown;
            }
            ENDCG
        }
    }
}
