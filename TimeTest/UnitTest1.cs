using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TimeProject;

namespace TimeTest
{
    [TestClass]
    public class TimeUnitTest
    {
        [TestMethod]
        [DataRow(1, 2, 3)]
        public void TimeConstructor3Param(int a, int b, int c)
        {
            Time time = new Time((byte)a, (byte)b, (byte)c);
            Assert.AreEqual(time.Hours, a);
            Assert.AreEqual(time.Minutes, b);
            Assert.AreEqual(time.Seconds, c);
        }
        [TestMethod]
        [DataRow(1, 2)]
        public void TimeConstructor2Param(int a, int b)
        {
            Time time = new Time((byte)a, (byte)b);
            Assert.AreEqual(time.Hours, a);
            Assert.AreEqual(time.Minutes, b);
            Assert.AreEqual(time.Seconds, 0);
        }
        [TestMethod]
        [DataRow(1)]
        public void TimeConstructor1Param(int a)
        {
            Time time = new Time((byte)a);
            Assert.AreEqual(time.Hours, a);
            Assert.AreEqual(time.Minutes, 0);
            Assert.AreEqual(time.Seconds, 0);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        [DataRow(111)]
        public void WrongTimeConstructor1Param(int a)
        {
            Time time = new Time((byte)a);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        [DataRow(111)]
        public void WrongTimeConstructor2Param(int a)
        {
            Time time = new Time((byte)a , (byte)a);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        [DataRow(111)]
        public void WrongTimeConstructor3Param(int a)
        {
            Time time = new Time((byte)a, (byte)a, (byte)a);
        }
        [TestMethod]
        [DataRow(1, 2, 3)]
        public void TimeConstructorFromString(int a, int b, int c)
        {
            Time timeExpected = new Time((byte)a, (byte)b, (byte)c);
            Time time = new Time(timeExpected.ToString());
            Assert.AreEqual(time.Hours, a);
            Assert.AreEqual(time.Minutes, b);
            Assert.AreEqual(time.Seconds, c);
        }
        [TestMethod]
        [DataRow(1, 2, 3)]
        [DataRow(5, 22, 33)]
        [DataRow(11, 22, 33)]
        public void TimeToString(int a, int b, int c)
        {
            Time time = new Time((byte)a, (byte)b, (byte)c);
            string actual = time.ToString();
            Assert.AreEqual(actual, $"{a:00}:{b:00}:{c:00}");
        }
        [TestMethod]
        [DataRow(1, 1, 1, 1, 1, 1, 2, 2, 2)]
        [DataRow(15, 1, 1, 15, 1, 1, 6, 2, 2)]
        [DataRow(1, 35, 1, 1, 35, 1, 3, 10, 2)]
        [DataRow(1, 1, 35, 1, 1, 35, 2, 3, 10)]
        public void TimePlus(int a, int b, int c, int a1, int b1, int c1, int expectedA, int expectedB, int expectedC)
        {
            Time time = new Time((byte)a, (byte)b, (byte)c);
            Time time1 = new Time((byte)a1, (byte)b1, (byte)c1);
            Time expected = new Time((byte)expectedA, (byte)expectedB, (byte)expectedC);
            Assert.AreEqual(expected, time.TimePlus(time1));
        }
        [TestMethod]
        [DataRow(1, 1, 1, 1, 1, 1, 0, 0, 0)]
        [DataRow(1, 1, 1, 10, 1, 1, 15, 0, 0)]
        [DataRow(1, 1, 1, 1, 10, 1, 23, 51, 0)]
        [DataRow(1, 1, 1, 1, 1, 10, 23, 59, 51)]
        public void TimeSubstraction(int a, int b, int c, int a1, int b1, int c1, int expectedA, int expectedB, int expectedC)
        {
            Time time = new Time((byte)a, (byte)b, (byte)c);
            Time time1 = new Time((byte)a1, (byte)b1, (byte)c1);
            Time expected = new Time((byte)expectedA, (byte)expectedB, (byte)expectedC);
            Assert.AreEqual(expected, time.TimeSubstract(time1));
        }
        [TestMethod]
        [DataRow(1, 1, 1, 1, 1, 1, 2, 2, 2)]
        [DataRow(15, 1, 1, 15, 1, 1, 6, 2, 2)]
        [DataRow(1, 35, 1, 1, 35, 1, 3, 10, 2)]
        [DataRow(1, 1, 35, 1, 1, 35, 2, 3, 10)]
        public void StaticTimePlus(int a, int b, int c, int a1, int b1, int c1, int expectedA, int expectedB, int expectedC)
        {
            Time time = new Time((byte)a, (byte)b, (byte)c);
            Time time1 = new Time((byte)a1, (byte)b1, (byte)c1);
            Time expected = new Time((byte)expectedA, (byte)expectedB, (byte)expectedC);
            Assert.AreEqual(expected, Time.TimePlus(time, time1));
        }
        [TestMethod]
        [DataRow(1, 1, 1, 1, 1, 1, 0, 0, 0)]
        [DataRow(1, 1, 1, 10, 1, 1, 15, 0, 0)]
        [DataRow(1, 1, 1, 1, 10, 1, 23, 51, 0)]
        [DataRow(1, 1, 1, 1, 1, 10, 23, 59, 51)]
        public void StaticTimeSubstraction(int a, int b, int c, int a1, int b1, int c1, int expectedA, int expectedB, int expectedC)
        {
            Time time = new Time((byte)a, (byte)b, (byte)c);
            Time time1 = new Time((byte)a1, (byte)b1, (byte)c1);
            Time expected = new Time((byte)expectedA, (byte)expectedB, (byte)expectedC);
            Assert.AreEqual(expected, Time.TimeSubstract(time, time1));
        }
        [TestMethod]
        [DataRow(1, 1, 1, 1, 1, 1, 2, 2, 2)]
        [DataRow(15, 1, 1, 15, 1, 1, 6, 2, 2)]
        [DataRow(1, 35, 1, 1, 35, 1, 3, 10, 2)]
        [DataRow(1, 1, 35, 1, 1, 35, 2, 3, 10)]
        public void OperatorTimePlus(int a, int b, int c, int a1, int b1, int c1, int expectedA, int expectedB, int expectedC)
        {
            Time time = new Time((byte)a, (byte)b, (byte)c);
            Time time1 = new Time((byte)a1, (byte)b1, (byte)c1);
            Time expected = new Time((byte)expectedA, (byte)expectedB, (byte)expectedC);
            Assert.AreEqual(expected, time + time1);
        }
        [TestMethod]
        [DataRow(1, 1, 1, 1, 1, 1, 0, 0, 0)]
        [DataRow(1, 1, 1, 10, 1, 1, 15, 0, 0)]
        [DataRow(1, 1, 1, 1, 10, 1, 23, 51, 0)]
        [DataRow(1, 1, 1, 1, 1, 10, 23, 59, 51)]
        public void OperatorTimeSubstraction(int a, int b, int c, int a1, int b1, int c1, int expectedA, int expectedB, int expectedC)
        {
            Time time = new Time((byte)a, (byte)b, (byte)c);
            Time time1 = new Time((byte)a1, (byte)b1, (byte)c1);
            Time expected = new Time((byte)expectedA, (byte)expectedB, (byte)expectedC);
            Assert.AreEqual(expected, time - time1);
        }
        [TestMethod]
        [DataRow(15, 1, 1, 18, 1, 1)]
        [DataRow(1, 35, 1, 1, 45, 1)]
        [DataRow(1, 1, 35, 1, 1, 45)]
        public void OperatorTimeGreater(int a, int b, int c, int a1, int b1, int c1)
        {
            Time time = new Time((byte)a, (byte)b, (byte)c);
            Time time1 = new Time((byte)a1, (byte)b1, (byte)c1);
            Assert.IsTrue( time1 > time);
        }
        [TestMethod]
        [DataRow(15, 1, 1, 17, 1, 1)]
        [DataRow(1, 35, 1, 1, 45, 1)]
        [DataRow(1, 1, 35, 1, 1, 45)]
        public void OperatorTimeLesser(int a, int b, int c, int a1, int b1, int c1)
        {
            Time time = new Time((byte)a, (byte)b, (byte)c);
            Time time1 = new Time((byte)a1, (byte)b1, (byte)c1);
            Assert.IsTrue(time < time1);
        }
        [TestMethod]
        [DataRow(1, 1, 1, 1, 1, 1)]
        [DataRow(15, 1, 1, 18, 1, 1)]
        [DataRow(1, 35, 1, 1, 45, 1)]
        [DataRow(1, 1, 35, 1, 1, 45)]
        public void OperatorTimeGreaterOrEqual(int a, int b, int c, int a1, int b1, int c1)
        {
            Time time = new Time((byte)a, (byte)b, (byte)c);
            Time time1 = new Time((byte)a1, (byte)b1, (byte)c1);
            Assert.IsTrue(time1 >= time);
        }
        [TestMethod]
        [DataRow(1, 1, 1, 1, 1, 1)]
        [DataRow(15, 1, 1, 17, 1, 1)]
        [DataRow(1, 35, 1, 1, 45, 1)]
        [DataRow(1, 1, 35, 1, 1, 45)]
        public void OperatorTimeLesserOrEqual(int a, int b, int c, int a1, int b1, int c1)
        {
            Time time = new Time((byte)a, (byte)b, (byte)c);
            Time time1 = new Time((byte)a1, (byte)b1, (byte)c1);
            Assert.IsTrue(time <= time1);
        }
        [TestMethod]
        [DataRow(1, 1, 1, 1, 1, 1)]
        [DataRow(15, 1, 1, 15, 1, 1)]
        [DataRow(1, 35, 1, 1, 35, 1)]
        [DataRow(1, 1, 35, 1, 1, 35)]
        public void OperatorTimeEqual(int a, int b, int c, int a1, int b1, int c1)
        {
            Time time = new Time((byte)a, (byte)b, (byte)c);
            Time time1 = new Time((byte)a1, (byte)b1, (byte)c1);
            Assert.IsTrue(time1 == time);
        }
        [TestMethod]
        [DataRow(1, 1, 1, 1, 2, 1)]
        [DataRow(15, 1, 1, 17, 1, 1)]
        [DataRow(1, 35, 1, 1, 45, 1)]
        [DataRow(1, 1, 35, 1, 1, 45)]
        public void OperatorTimeNotEqual(int a, int b, int c, int a1, int b1, int c1)
        {
            Time time = new Time((byte)a, (byte)b, (byte)c);
            Time time1 = new Time((byte)a1, (byte)b1, (byte)c1);
            Assert.IsTrue(time != time1);
        }
    }
}