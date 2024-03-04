Shader "Sprites/CustomDefault"
{
    Properties
    {
        [PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
        _DecalTex ("DecalTex",2D) = "white"{}
        _Color ("Color", Color) = (1,1,1,1)
        _Speed ("Speed", Float) = 10
        _Decal ("Decal", Float) = 0
        _Split ("Split", Float) = 0
        _Threshold ("Threshold", Float) = 0
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
            
            fixed _Speed;
            fixed _Decal;
            sampler2D _DecalTex;
            fixed _Threshold;
            fixed _Split;
            
            fixed4 SpriteFragment(v2f i) : SV_Target
            {
                float2 texCoord = i.texcoord;
                texCoord.x += _Time * _Speed;
                fixed4 decalColor = tex2D(_DecalTex,texCoord);
                
                if (decalColor.r > _Threshold)
                {
                    if(i.texcoord.x > _Split)
                    {
                        i.texcoord.x -= _Decal;
                    }
                    else
                    {
                        i.texcoord.x += _Decal;
                    }
                }
                
                fixed4 color = SpriteFrag(i);
                return color;
            }
        ENDCG
        }
    }
}