using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace TestProject
{
    [TestClass]
    public class UnitTest
    {
        [TestMethod]
        public void Test1()
        {
            string input = "" +
                "1 1 1 1\r\n" +
                "0 0 0 0\r\n" +
                "0 1 1 0\r\n" +
                "0 0 0 0";
            List<string> dirs_list = new List<string> { "R", "D", "L", "U" };

            string expected = "" +
                "2 4 0 0\r\n" +
                "0 0 0 0\r\n" +
                "0 0 0 0\r\n" +
                "0 0 0 0";

            ShiftableField field = new ShiftableField(Converter.GetTextToShiftableFieldField(input.Split("\r\n")));
            field.Field = field.ShiftFieldInDirections(field.Field, dirs_list);

            string actual = Converter.GetMatrixInText(field.Field);

            Assert.AreEqual(expected, actual, "TEST 1 FAILED");
        }

        [TestMethod]
        public void Test2()
        {
            string input = "" +
                "1 1 1 1\r\n" +
                "0 0 0 0\r\n" +
                "0 0 0 0\r\n" +
                "0 0 0 0";
            List<string> dirs_list = new List<string> { "R", "D", "L", "U" };

            string expected = "" +
                "4 0 0 0\r\n" +
                "0 0 0 0\r\n" +
                "0 0 0 0\r\n" +
                "0 0 0 0";

            ShiftableField field = new ShiftableField(Converter.GetTextToShiftableFieldField(input.Split("\r\n")));
            field.Field = field.ShiftFieldInDirections(field.Field, dirs_list);

            string actual = Converter.GetMatrixInText(field.Field);

            Assert.AreEqual(expected, actual, "TEST 2 FAILED");
        }

        [TestMethod]
        public void Test3()
        {
            string input = "" +
                "0 0 0 0\r\n" +
                "2 0 2 0\r\n" +
                "0 0 0 0\r\n" +
                "0 4 0 8";
            List<string> dirs_list = new List<string> { "R", "U", "R"};

            string expected = "" +
                "0 0 0 8\r\n" +
                "0 0 0 8\r\n" +
                "0 0 0 0\r\n" +
                "0 0 0 0";

            ShiftableField field = new ShiftableField(Converter.GetTextToShiftableFieldField(input.Split("\r\n")));
            field.Field = field.ShiftFieldInDirections(field.Field, dirs_list);

            string actual = Converter.GetMatrixInText(field.Field);

            Assert.AreEqual(expected, actual, "TEST 3 FAILED");
        }

        [TestMethod]
        public void Test4()
        {
            string input = "" +
                "0 0 0 0\r\n" +
                "2 0 2 0\r\n" +
                "1 1 1 1\r\n" +
                "0 4 0 8";
            List<string> dirs_list = new List<string> { "R"};

            string expected = "" +
                "0 0 0 0\r\n" +
                "0 0 0 4\r\n" +
                "0 0 2 2\r\n" +
                "0 0 4 8";

            ShiftableField field = new ShiftableField(Converter.GetTextToShiftableFieldField(input.Split("\r\n")));
            field.Field = field.ShiftFieldInDirections(field.Field, dirs_list);

            string actual = Converter.GetMatrixInText(field.Field);

            Assert.AreEqual(expected, actual, "TEST 4 FAILED");
        }
    }
}