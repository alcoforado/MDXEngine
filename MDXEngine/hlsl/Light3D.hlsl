// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
struct VS_IN
{
	float4 pos : POSITION;
	float4 normal : NORMAL;
};

struct PS_IN
{
	float4 pos : SV_POSITION;
	float4 normal: NORMAL;
};


struct DirectionalLight
{
	public float4 Ambient;
	public float4 Diffuse;
	public float4 Specular;
	public float3 Direction;
	public float Padding;
};

struct  Material
{
	float4 Ambient;
	float4 Diffuse;
	float4 Specular; //w=SpecPower
	float4 Reflect;
};


cbuffer OneTime
{
	DirectionalLight dlight;
};

cbuffer ManyPerFrame
{
	Material mat;
};

cbuffer TViewChange : register(b2)
{
	float4x4 worldViewProj;
	float4 eyePos;
	
};


void ComputeDirectionalLight(
	Material mat,
	DirectionalLight L,
	float3 normal, //Normalized surface normal
	float3 toEye,  //Normalized vector pointing from the surface to the camera eye
	out float4 ambient,
	out float4 diffuse,
	out float4 spec)
{
	ambient = float4(0, 0, 0, 0);
	diffuse = float4(0, 0, 0, 0);
	spec = float4(0, 0, 0, 0);

	//Ambient Term
	ambient = mat.Ambient*L.Ambient;

	//lightVector is opposite to the L.Direction
	float3 lightVector = -L.Direction;

	//diffuse Factor
	float diffuseFactor = dot(lightVec, normal);


	if (diffuseFactor > 0.0f)
	{
		float3 v = reflect(L.Direction, normal);
		float specFactor = pow(max(dot(v, toEye), 0.0f), mat.Specular.W);
		diffuse = diffuseFactor*mat.Diffuse*L.Diffuse;
		spec = specFactor*mat.Specular*L.Specular;
	}
}




PS_IN VS(VS_IN input)
{
	PS_IN output = (PS_IN)0;

	output.pos = mul(worldViewProj, input.pos);
	output.normal = input.normal;

	return output;
}

float4 PS(PS_IN input) : SV_Target
{
	//normal interpolation can unormalize normal.
	//Normalize it now
	input.normal = normalize(input.normal);
	float3 toEyeW = normalize(eyePos - input.pos);

	ambient = float4(0, 0, 0, 0);
	diffuse = float4(0, 0, 0, 0);
	spec = float4(0, 0, 0, 0);

	float4 A, D, S;

	ComputeDirectionalLight(mat, dlight, input.normal, toEyeW, , A, D, S);
		
	float4 litColor = A + D + S;
	return litColor;

}

