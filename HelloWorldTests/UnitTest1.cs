namespace HelloWorldTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);
            HelloWorld.Main();
            var output = stringWriter.ToString();
            Assert.AreEqual("Hello, World!\r\n", output);
        }
    }
}