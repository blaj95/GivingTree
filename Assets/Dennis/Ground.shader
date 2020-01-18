Shader "Custom/Ground"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _GrassTex ("Albedo (RGB)", 2D) = "white" {}
        _Tiling ("Tiling", Range(1, 5)) = 1.0
        _Saturation ("Saturation", Range(1, 2)) = 1.0
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
    }
    SubShader
    {
        Tags { "RenderQueue"="Transparent" "RenderType"="Transparent" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows alpha:fade

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;
        sampler2D _GrassTex;
        float _Tiling;

        struct Input
        {
            float2 uv_MainTex;
        };

        half _Glossiness;
        half _Metallic;
        half _Saturation;
        fixed4 _Color;

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        //UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        //UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            // Albedo comes from a texture tinted by color
            fixed4 dirt = tex2D (_MainTex, (IN.uv_MainTex * _Tiling) % 1.0) * _Color;
            fixed4 grass = tex2D (_GrassTex, (IN.uv_MainTex * _Tiling) % 1.0) * _Color;
            
            float dirtFactor = smoothstep(0.35, 0.1, distance(IN.uv_MainTex, float2(0.5, 0.5)));

            o.Alpha = smoothstep(0.5, 0.25, distance(IN.uv_MainTex, float2(0.5, 0.5)));
            
            dirt = fixed4(dirt.r * _Saturation, dirt.g, dirt.b, dirt.a); //saturate the redness in dirt
            grass = fixed4(grass.r, grass.g * _Saturation, grass.b, grass.a); //saturate the green in grass


            o.Albedo = (dirt.rgb * dirtFactor) + (grass.rgb * (1.0-dirtFactor));
            
            //clip(0.45 - distance(IN.uv_MainTex, float2(0.5, 0.5)));

            // Metallic and smoothness come from slider variables
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
