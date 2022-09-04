Shader "Przekop/TextureGraph/Split"
{
    Properties
    {
        _input ("input", 2D) = "black" {}
    }
    SubShader
    {

        LOD 100

        Pass
        {
            CULL off
            CGPROGRAM
            
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _input;
            float4 _input_ST;

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _input);
                return o;
            }

            void frag(v2f i, out half4 r:SV_Target0, out half4 g:SV_Target1, out half4 b:SV_Target2,
                      out half4 a:SV_Target3)
            {
                // sample the texture
                fixed4 col = tex2D(_input, i.uv);
                r = col.r;
                g = col.g;
                b = col.b;
                a = col.a;
                // apply fog
            }
            ENDCG
        }
    }
}