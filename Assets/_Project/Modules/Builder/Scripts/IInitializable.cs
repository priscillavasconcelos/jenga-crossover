
namespace Jenga.Builder
{
    public interface IInitializable<T>
    {
        public void Initialize(T obj);
    }

}
