using Moq.AutoMock;

namespace Recruitment.Tests
{
    public abstract class MockerBase<T> 
        where T : class
    {
        public AutoMocker AutoMocker;

        protected MockerBase() => 
            AutoMocker = new AutoMocker();

        protected T GetInstance() => 
            AutoMocker.CreateInstance<T>();
    }
}
