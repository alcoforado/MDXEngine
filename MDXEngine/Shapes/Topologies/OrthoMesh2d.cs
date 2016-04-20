using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpDX;




public class OrthoMesh2D
{

    public uint NumElemsX;
    public uint NumElemsY;
    private uint _lastY;
    private uint _lastX;
    private uint _numVerticesX;
    private uint _numVerticesY;

    private double _dx;
    private double _dy;
    public Vector2 O;
    public Vector2 P;
    private readonly uint[] _cellVerticeOffset;

    public OrthoMesh2D(uint numElemsX, uint numElemsY, Vector2 o, Vector2 p)
    {
        NumElemsX = numElemsX;
        NumElemsY = numElemsY;

        _numVerticesX = numElemsX + 1;
        _numVerticesY = numElemsY + 1;

        O = o;
        P = p;

        if (o.X > p.X || o.Y > p.Y)
            throw new Exception("Expect Origin to have coordinates smaller then extreme point");

        _lastY = numElemsY - 1;
        _lastX = numElemsX - 1;
        _dx = (double) (P.X - O.X)/(double) numElemsX;
        _dy = (double) (P.Y - O.Y)/(double) numElemsY;

        _cellVerticeOffset = new uint[4];
        _cellVerticeOffset[(int) CellVertice.BottomLeft] = 0;
        _cellVerticeOffset[(int) CellVertice.BottomRight] = 1;
        _cellVerticeOffset[(int) CellVertice.UpLeft] = _numVerticesX;
        _cellVerticeOffset[(int) CellVertice.UpRight] = _numVerticesX+1;
    }


    public CellIt BeginCell()
    {
        return new CellIt(this);
    }

    public VerticeIt BeginVertice()
    {
        return new VerticeIt(this);
    }


    public enum CellVertice
    {
        BottomLeft=0,
        BottomRight=1,
        UpRight=2,
        UpLeft=3
    };

    public class VerticeIt
    {
        private readonly OrthoMesh2D _mesh;
        private uint _i;
        private uint _j;

        public VerticeIt(OrthoMesh2D mesh)
        {
            _mesh = mesh;

            _i = _j = 0;
        }

        public bool Next()
        {

            if (++_i < _mesh._numVerticesX)
                return true;
            if (_j == _mesh.NumElemsY)
            {
                _i = _mesh._numVerticesX;
                return false;
            }
            _j++;
            _i = 0;
            return true;

        }

        public Vector2 Vertice()
        {
            return new Vector2((float) _mesh._dx*_i + _mesh.O.X, (float) _mesh._dy*_j + _mesh.O.Y);
        }


    }


    public class CellIt
    {
        uint _i;
        uint _j;
        private uint _index;
        private readonly OrthoMesh2D _mesh;

        public CellIt(OrthoMesh2D mesh)
        {
            _i = _j = 0;
            _mesh = mesh;
        }

        public bool Next()
        {

            if (++_i < _mesh.NumElemsX)
            {
                _index++;
                return true;
            }
            if (_j == _mesh._lastY)
            {
                _i = _mesh.NumElemsX;
                return false;
            }
            _j++;
            _i = 0;
            _index++;
            return true;
        }

        public uint vertex_index(CellVertice v)
        {
            return _j*_mesh.NumElemsX + _i + _mesh._cellVerticeOffset[(int) v];
        }



    }



}
