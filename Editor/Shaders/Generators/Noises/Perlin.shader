Shader "Przekop/TextureGraph/Perlin"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags
        {
            "RenderType"="Opaque"
        }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            int _seed;
            float4 _tiling;


            float2 RandomGradient(int ix, int iy)
            {
                 uint w = 8 * 8;
                 uint s = w / 2;
                 uint a = ix, b = iy;
                a *= 3284157443;
                b ^= a << s | a >> w - s;
                b *= 1911520717;
                a ^= b << s | b >> w - s;
                a *= 2048419325;
                float random = a * (3.14159265 / ~(~0u >> 1)); // in [0, 2*Pi]
                float2 v;
                v.x = cos(random);
                v.y = sin(random);
                return v;
            }

            float DotGridGradient(int ix, int iy, float x, float y)
            {
                // Get gradient from integer coordinates
                float2 gradient = RandomGradient(ix, iy);

                // Compute the distance vector
                float dx = x - (float)ix;
                float dy = y - (float)iy;

                // Compute the dot-product
                return (dx * gradient.x + dy * gradient.y);
            }

            float smoothstepInterp(float a, float b, float w)
            {
                float v = w*w*w*(w*(w*6-15)+10);
                return a+ v*(b-a);
            }

            float Perlin(float2 uv)
            {
                // Determine grid cell coordinates
                 int x0 = (int)floor(uv.x);
                int x1 = x0 + 1;
                int y0 = (int)floor(uv.y);
                int y1 = y0 + 1;

                // Determine interpolation weights
                // Could also use higher order polynomial/s-curve here
                float sx = uv.x - (float)x0;
                float sy = uv.y - (float)y0;

                // Interpolate between grid point gradients
                float n0, n1, ix0, ix1, value;

                n0 = DotGridGradient(x0, y0, uv.x, uv.y);
                n1 = DotGridGradient(x1, y0, uv.x, uv.y);
                ix0 = smoothstepInterp(n0, n1, sx);

                n0 = DotGridGradient(x0, y1, uv.x, uv.y);
                n1 = DotGridGradient(x1, y1, uv.x, uv.y);
                ix1 = smoothstepInterp(n0, n1, sx);
                value = smoothstepInterp(ix0, ix1, sy);
                return value; // Will return in range -1 to 1. To make it in range 0 to 1, multiply by 0.5 and add 0.5
            }

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                // sample the texture

                // apply fog

                return float4(Perlin(i.uv * _tiling + float2(_seed, _seed * 7)),
                              Perlin(i.uv * _tiling + float2(_seed*13, _seed * 3)),
                              Perlin(i.uv * _tiling + float2(_seed*11, _seed)),
                              Perlin(i.uv * _tiling + float2(_seed*7, _seed * 27)));
            }
            ENDCG
        }
    }
}