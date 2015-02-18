using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MDXEngine.Textures;

namespace MDXEngine
{
    internal interface IDrawTreeAction
    {
        void Execute();
        bool TryMerge(IDrawTreeAction action);
    }

    internal class DrawTreeActionLoadTexture
    {
        private Texture _texture;
        private int _textureSlot;
        private HLSLProgram _program;

        public DrawTreeActionLoadTexture(HLSLProgram program,String name,Texture texture)
        {
            _program = program;
            _texture = texture;
            _textureSlot=_program.GetTextureSlot(name);

        }

        public void Execute()
        {
            /*
            _program.LoadTexture(_textureSlot, _texture);*/
        }


    }


   
}
