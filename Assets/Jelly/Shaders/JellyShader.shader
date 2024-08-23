Shader "Custom/LayeredJellyShader"
{
    Properties
    {
        _Color ("Main Color", Color) = (1,1,1,1)
        _Glossiness ("Smoothness", Range(0.0,1.0)) = 0.5
        _Metallic ("Metallic", Range(0.0,1.0)) = 1
        _MainTex ("Texture", 2D) = "white" {}
        _NormalMap ("Normal Map", 2D) = "bump" {}
        _RimColor ("Rim Color", Color) = (1,1,1,1)
        _RimPower ("Rim Power", Range(0.5,20.0)) = 5.5
        _Layer1Transparency ("Layer 1 Transparency", Range(0.0,1.0)) = 0.612
        _Layer2Transparency ("Layer 2 Transparency", Range(0.0,1.0)) = 0.412
        _Layer3Transparency ("Layer 3 Transparency", Range(0.0,1.0)) = 0.553
        _Layer4Transparency ("Layer 4 Transparency", Range(0.0,1.0)) = 0.3
        _Layer5Transparency ("Layer 5 Transparency", Range(0.0,1.0)) = 0.625
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }
        LOD 200

        CGPROGRAM
        #pragma surface surf Standard alpha:fade

        struct Input
        {
            float2 uv_MainTex;
            float2 uv_NormalMap;
            float3 viewDir;
        };

        sampler2D _MainTex;
        sampler2D _NormalMap;
        half _Glossiness;
        half _Metallic;
        fixed4 _Color;
        fixed4 _RimColor;
        half _RimPower;
        half _Layer1Transparency;
        half _Layer2Transparency;
        half _Layer3Transparency;
        half _Layer4Transparency;
        half _Layer5Transparency;

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;

            // Sample the normal map
            half3 normal = UnpackNormal(tex2D(_NormalMap, IN.uv_NormalMap)).xyz;

            // Calculate transparency based on depth (simplified)
            float depthFactor = IN.uv_MainTex.y; // Use y-coordinate as depth example
            float transparency = lerp(
                _Layer1Transparency,
                _Layer5Transparency,
                depthFactor
            );

            o.Albedo = c.rgb;
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = c.a * transparency;

            // Apply normal map
            o.Normal = normal;

            // Rim lighting effect (make sure rim color is opaque)
            float rim = 1.0 - saturate(dot(normalize(IN.viewDir), o.Normal));
            fixed4 rimColor = fixed4(_RimColor.rgb, 1.0); // Ensure rim color is opaque
            o.Albedo += rimColor.rgb * pow(rim, _RimPower);
        }
        ENDCG
    }
    FallBack "Transparent/Cutout/VertexLit"
}
