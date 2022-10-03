Shader "Unlit/Temp"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
        _AlphaValue("Alpha Value", Range(0, 1)) = 1
    }
        SubShader
        {
            Tags { "RenderType" = "Transparent" "Queue" = "Transparent" } //
            blend SrcAlpha OneMinusSrcAlpha //
            LOD 100

            Pass
            {
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

                sampler2D _MainTex;
                float4 _MainTex_ST;
                float1 _AlphaValue;

                v2f vert(appdata v)
                {
                    v2f o;
                    o.vertex = UnityObjectToClipPos(v.vertex);
                    o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                    UNITY_TRANSFER_FOG(o,o.vertex);
                    return o;
                }

                fixed4 frag(v2f i) : SV_Target
                {
                      float1 a = 1; // _AlphaValue;
                      float2 uv = float2(1, 1) - i.uv;

                      a -= _Time.x;

                      float4 temp = float4(1, 1, 1, a);
                      float4 brick = tex2D(_MainTex, uv);

                      return temp * brick;
                }
                    ENDCG
                }
        }
}
