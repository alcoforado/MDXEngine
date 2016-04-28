using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MDXEngine;
using SharpDX;




public class OrthoMesh2D : ITopology 
{

    public uint NumElemsX { get; private set; }
    public uint NumElemsY { get; private set; }
    public uint NumCells { get; private set; }
    public uint NumVertices { get; private set; }
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
        NumCells = numElemsX*numElemsY;
        _numVerticesX = numElemsX + 1;
        _numVerticesY = numElemsY + 1;
        NumVertices = _numVerticesX*_numVerticesY;
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

    public const uint InvalidIndex = UInt32.MaxValue;


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
        public uint _index;

        public VerticeIt(OrthoMesh2D mesh)
        {
            _mesh = mesh;

            _i = _j = _index=0;
        }

        public bool Next()
        {

            if (++_i < _mesh._numVerticesX)
            {
                _index++;
                return true;
            }
            if (_j == _mesh.NumElemsY)
            {
                _j = _mesh._numVerticesY;
                _i = _mesh._numVerticesX;
                _index = uint.MaxValue;
                return false;
            }
            _j++;
            _i = 0;
            _index++;
            return true;

        }


        public uint Index()
        {
            return _index;
        }

        public Vector2 Vertice()
        {
            if (_index == uint.MaxValue)
                throw new Exception("OrthoMesh2D: Vertice past the end");
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
                _index =  OrthoMesh2D.InvalidIndex;
                return false;
            }
            _j++;
            _i = 0;
            _index++;
            return true;
        }

        public uint VertexIndex(CellVertice v)
        {
            return _j*_mesh._numVerticesX + _i + _mesh._cellVerticeOffset[(int) v];
        }


        public uint Index()
        {

            return _index;
        }
    }


    public int NIndices()
    {
       return  (int) (this.NumCells*6);
    }

    public int NVertices()
    {
        return (int) (this.NumVertices);
    }

    public void Write(IArray<Vector3> vV, IArray<int> vI)
    {
        int i = 0;
        for (var v = BeginVertice(); v.Next();)
        {
            vV[i++]=new Vector3(v.Vertice(),0.0f);
        }
        i = 0;
        for (var it = BeginCell(); it.Next();)
        {
            vI[i++] = (int) it.VertexIndex(CellVertice.BottomLeft);
            vI[i++] = (int) it.VertexIndex(CellVertice.BottomRight);
            vI[i++] = (int) it.VertexIndex(CellVertice.UpLeft);
            vI[i++] = (int) it.VertexIndex(CellVertice.BottomRight);
            vI[i++] = (int) it.VertexIndex(CellVertice.UpRight);
            vI[i++] = (int) it.VertexIndex(CellVertice.UpLeft);
        }
    }

    public TopologyType GetTopologyType()
    {
        return TopologyType.TRIANGLES;
    }
}
