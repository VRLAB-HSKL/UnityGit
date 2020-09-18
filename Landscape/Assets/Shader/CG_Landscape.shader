// Location of the Shader in the Menu
Shader "_Shaders/CG/Landscape" {
	Properties 
	{
		// Colors you can choose later in the material
		_MyColor0 ("Farbe0 (Unterste Schicht)", Color) = (0,0,1,1)
		_MyColor1 ("Farbe1", Color) = (.255,.412,.882,1)
		_MyColor2 ("Farbe2", Color) = (.529,.808,.980,1)
		_MyColor3 ("Farbe3", Color) = (.604,.804,.196,1)
		_MyColor4 ("Farbe4", Color) = (0,.502,0,1)
		_MyColor5 ("Farbe5", Color) = (.957,.643,.376,1)
		_MyColor6 ("Farbe6", Color) = (.545, .271, .075,1)
		_MyColor7 ("Farbe7 (Oberste Schicht)", Color) = (1,1,1,1)
		
		// the "size" of the object
		_Size0 ("Größe", int) = 200
	}
	SubShader 
	{
		Tags { "Queue" = "Geometry" }		
		Pass
		{
			// We want a CG Shader. There is also GLSLPROGRAM.
			CGPROGRAM
			// name of vertex shader function
			#pragma vertex vert
			// name of fragment shader function
			#pragma fragment frag
			
			// include some utilities functionality (Constants etc.)
			#include "UnityCG.cginc"
			
			// uniform variables. They need to have the same name as the properties.
			fixed4 _MyColor0;
			fixed4 _MyColor1;
			fixed4 _MyColor2;
			fixed4 _MyColor3;
			fixed4 _MyColor4;
			fixed4 _MyColor5;
			fixed4 _MyColor6;
			fixed4 _MyColor7;
			float _Size0;
			
			// Unity fills this for us
			struct vertexInput
			{
				// position in object coords.
				float4 vertex : POSITION; 
			};

			// Things we pass from frag to vertex
			struct fragmentInput
			{
				float4  vertex : SV_POSITION;
				float4  posWorld: TEXCOORDS;
				fixed4  color : COLOR0;
			};

			// Vertex Shader
			fragmentInput vert( vertexInput i )
			{
				fragmentInput o;
				
				// change object to world coords
				o.posWorld = mul( unity_ObjectToWorld, i.vertex );
				o.vertex = UnityObjectToClipPos(  i.vertex);
				return o;
			}

			// Fragment Shader
			half4 frag( fragmentInput o ) : COLOR
			{
				// just for convinience
				float h = o.posWorld.y;
				// 8 -> number of different colors.
				float steps = _Size0 / 8.0f; 
				
				// Set the color of the pixel depending on the y-value
				if(h <= 0.0f)
					o.color = _MyColor0;
				else if(h > 0.0f && h <= steps)
					o.color = _MyColor1;
				else if(h > steps && h <= 2*steps)
					o.color = _MyColor2;
				else if (h > 2 * steps && h <= 3*steps)
					o.color = _MyColor3;
				else if (h > 3*steps  && h <= 4*steps)
					o.color = _MyColor4;
				else if (h > 4*steps && h <= 5*steps)
					o.color = _MyColor5;
				else if (h > 5*steps && h <= 6*steps)
					o.color = _MyColor6;
				else if ( h>6*steps)
					o.color = _MyColor7;
					
				return o.color;
			}
			ENDCG
		}	//END Pass
		
	} 	// END SubShader
	FallBack "Diffuse"
}
