Shader "Portal/Unlit_URP"
{
    
    SubShader
    {
        Zwrite off
        ColorMask 0

        Stencil
        {
            Ref 1
            Pass replace
        }
            
        Pass
        {

        }

        Pass
        {
            
        }

        Pass
        {
            
        }
    }
}
