using System;
using System.Text;
using System.Collections.Generic;
using FluentAssertions;
using MDXEngine;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpDX.Direct3D11;
using SharpDX.DXGI;

namespace UnitTests
{
    /// <summary>
    /// Summary description for HLSLProgramUnitTest
    /// </summary>
    [TestClass]
    public class HLSLProgramUnitTest
    {
        private HLSLProgram _program;

        public String code = @"

// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
struct VS_IN
{
	float4 pos : POSITION;
	float4 col : COLOR;
};

struct PS_IN
{
	float4 pos : SV_POSITION;
	float4 col : COLOR;
};

Texture2D texture1;
Texture2D texture2;

SamplerState samAnisotropic
{
	Filter = ANISOTROPIC;
	MaxAnisotropy = 4;
	AdressU = WRAP;
	AdressV = WRAP;
};

PS_IN VS(VS_IN input)
{
	PS_IN output = (PS_IN)0;

	output.pos = input.pos;
	output.col = input.col;

	return output;
}

float4 PS(PS_IN input) : SV_Target
{
    float4 result = texture1.Sample(samAnisotropic,float2(0.0,1.0));
    result+=texture2.Sample(samAnisotropic,float2(0.0,1.0));
    return result;
}

";


        public HLSLProgramUnitTest()
        {
             _program = new HLSLProgram(Utilities.GetDxContext(), code, new[]
                    {
                        new InputElement("POSITION", 0, Format.R32G32B32A32_Float, 0, 0),
                        new InputElement("COLOR", 0, Format.R32G32B32A32_Float, 16, 0)
                    });
        }

        
       

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        
        
        [TestMethod]
        public void HLSLProgram_CompilationOfAValidProgramShouldNotTrhowException()
        {
            

        }

        [TestMethod]
        public void HLSLProgram_InvalidVariableNameShouldNotReturnAVariable()
        {
            var result = _program.ProgramResourceSlots["NonExistentVariable"];
            result.Exists.Should().BeFalse();


        }

        [TestMethod]
        public void HLSLProgram_ValidVariableNameShouldReturnVariable()
        {
            var result1 = _program.ProgramResourceSlots["texture1"];
            var result2 = _program.ProgramResourceSlots["texture2"]; 

            result1.Exists.Should().BeTrue();
            result2.Exists.Should().BeTrue();

            result1.Value.Name.Should().Be("texture1");
            result2.Value.Name.Should().Be("texture2");

        }

        [TestMethod]
        public void HLSLProgram_SearchForVariableNameOrSlotIdShouldBeConsistent()
        {
            _program.ProgramResourceSlots["texture1"].Value.Name.Should().Be("texture1");
            _program.ProgramResourceSlots["texture2"].Value.Name.Should().Be("texture2");
        }





    }
}
