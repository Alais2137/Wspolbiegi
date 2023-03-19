namespace HelloWorldTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void HelloTest()
        {
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);
            HelloWorld.Main();
            var output = stringWriter.ToString();
            Assert.AreEqual("Hello, World!\r\n", output);
        }

        [TestMethod]
        public void AddingTest()
        {
            int a = 4;
            int b = 3;
            Assert.AreEqual(Mathing.Adding(a,b), a+b, 7);
        }
    }
}