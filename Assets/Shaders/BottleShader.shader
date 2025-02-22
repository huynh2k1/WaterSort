Shader "Unlit/Water"
{
    Properties
    {
        _MainTex ("Sprite Texture", 2D) = "white" {}
        _Color1("Color1", Color) = (1, 1, 1, 1)
        _Color2("Color2", Color) = (1, 1, 1, 1)
        _Color3("Color3", Color) = (1, 1, 1, 1)
        _Color4("Color4", Color) = (1, 1, 1, 1)
        _Amount1("Amount1", Float) = -0.29
        _Amount2("Amount2", Float) = -0.08
        _Amount3("Amount3", Float) = 0.13
        _Amount4("Amount4", Float) = 0.34
        _FillAmount("FillAmount", Range(-0.5, 0.34)) = 0
        _PosWorld("PosWorld", Vector) = (0, 0, 0, 0)
        _ObjectScale("ObjectScale", Vector) = (0, 0, 0, 0)
        _ScaleOffset("ScaleOffset", Float) = 1
    }
    SubShader
    {
        // Tags { "RenderType"="Opaque" }
        // LOD 100

        Pass
        {
            Tags
            {
                "Queue" = "Transparent"
            }
            Blend One OneMinusSrcAlpha
            Cull Off

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv1 : TEXCOORD0;
            };

            struct v2f
            {
                float4 vertex : POSITION;
                float2 uv1 : TEXCOORD0;
                float3 world_Pos : TEXCOORD1;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            float4 _Color1;
            float4 _Color2;
            float4 _Color3;
            float4 _Color4;

            float _Amount1;
            float _Amount2;
            float _Amount3;

            float _FillAmount;
            float4 _PosWorld;
            float4 _ObjectScale;
            float _ScaleOffset;

            v2f vert (appdata i)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(i.vertex);
                o.uv1 = i.uv1;
                o.world_Pos = mul(unity_ObjectToWorld, i.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col;
                float2 diff = (i.world_Pos - _PosWorld.xy)/_ObjectScale;
                float uv = step(diff.y, _FillAmount*_ScaleOffset);

                float s1 = step(diff.y, _Amount1*_ScaleOffset);
                float s2 = step(diff.y, _Amount2*_ScaleOffset);
                float s3 = step(diff.y, _Amount3*_ScaleOffset);

                float4 c1 = lerp(_Color2, _Color1, s1);
                float4 c2 = lerp(_Color3, c1, s2);
                float4 c3 = lerp(_Color4, c2, s3);
                col = c3 * uv;
                return col;
            }
            ENDCG
        }
    }
}
