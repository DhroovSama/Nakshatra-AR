Shader "Unlit/PortalDoorShader"
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
    }
}
