    Shader "ShineThroughFaces" 
    {
        Properties 
        {
            _Color1 ("Color1", Color) = (1,1,1,1)
        }
        SubShader 
        {
            
            Pass 
            { 
                ZTest Always
                Color [_Color1]
            }

        }
    }