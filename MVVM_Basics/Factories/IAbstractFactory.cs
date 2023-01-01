namespace MVVM_Basics.Factories
{
    public interface IAbstractFactory<T>
    {
        T Create();
    }
}