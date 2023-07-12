Shader "Sprites/CustomDefault"
{
    Properties
    {
        [PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
        _Color ("Color", Color) = (1,1,1,1)
        _Speed ("Speed", Float) = 10
        _Decal ("Decal", Float) = 0
        _CircleCount ("Circle Count", Float) = 8
        _StepTreshold (" Step Treshold",Float) = 0.1
        _InnerCircle (" Inner Circle",Float) = 0.1
        [MaterialToggle] PixelSnap ("Pixel snap", Float) = 0
        [HideInInspector] _RendererColor ("RendererColor", Color) = (1,1,1,1)
        [HideInInspector] _Flip ("Flip", Vector) = (1,1,1,1)
    }
    
    

    SubShader
    {
        Tags
        {
            "Queue"="Transparent"
            "IgnoreProjector"="True"
            "RenderType"="Transparent"
            "PreviewType"="Plane"
            "CanUseSpriteAtlas"="True"
        }

        Cull Off
        Lighting Off
        ZWrite Off
        Blend One OneMinusSrcAlpha


        Pass
        {
            CGPROGRAM
            #pragma vertex SpriteVert
            #pragma fragment SpriteFragment
            #pragma target 2.0
            #pragma multi_compile_instancing
            #include "UnitySprites.cginc"

            fixed _InnerCircle;
            fixed _Speed;
            fixed _CircleCount;
            fixed _Decal;
            fixed _StepTreshold;

            #define IsInInnerCircle(texCoord) (distance(texCoord, float2(0, 0)) <= _InnerCircle)

            fixed4 SpriteFragment(v2f i) : SV_Target
            {
                fixed2 centerUV = i.texcoord * 2 - 1;
                
                if(!IsInInnerCircle(centerUV))
                {
                    return SpriteFrag(i);
                }
                float length = distance(fixed2(0,0),centerUV);
                length = sin(length * _CircleCount + _Time * _Speed);
                length = abs(length);

                //length = step(_StepTreshold,length);

                if(length >= _StepTreshold)
                {
                    i.texcoord += normalize(centerUV) * _Decal;
                }
                //fixed4 color = fixed4(centerUV,0,0);
                //fixed4 color = fixed4(length,length,length,1);
                fixed4 color = SpriteFrag(i);
                return color;
            }
        ENDCG
        }
    }
}