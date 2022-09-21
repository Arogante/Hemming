namespace Services.Interfaces
{
    interface ICoder<T>
    {
        public T[] Code(string text);
        public string Decode(T[] code);
    }
}