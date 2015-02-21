namespace MDXEngine
{
    internal interface IDrawTreeAction
    {
        void Execute();
        bool TryMerge(IDrawTreeAction action);
    }



   
}
