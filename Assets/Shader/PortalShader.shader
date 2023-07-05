Shader "Sprites/CustomDefault"
{
    Properties
    {
        [PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
        _NoiseTex ("Sprite Texture", 2D) = "white" {}
        _NoiseDecalValue ("Noise Decal Value", Float) = 0
        _NoiseDecalOffset ("Noise Decal Offset", Float) = 0
        _Color ("Color", Color) = (1,1,1,1)
        _Speed ("Temps", float) = 0
        _InnerCircle ("InnerCircle", float) = 0
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

             sampler2D _NoiseTex;
             float _NoiseDecalValue;
             float _Speed;
             float _NoiseDecalOffset;
             float _InnerCircle;

            //#define IsInInnerCircle(texCoord) (distance(texCoord, float2(0.5, 0.5)) <= _InnerCircle)
            
            fixed4 SpriteFragment(v2f i) : SV_Target
            {
                fixed4 noiseColor = tex2D(_NoiseTex,i.texcoord * sin(_Time * _Speed));
                
                if(/*IsInInnerCircle(i.texcoord) && */noiseColor.g < _NoiseDecalValue)
                {
                    i.texcoord.y += _NoiseDecalOffset;
                }

                fixed4 spriteFragment = SpriteFrag(i);
                // Fragment Distortion Effect goes here
                return spriteFragment;
            }
        ENDCG
        }
    }
}