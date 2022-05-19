using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TimeProject;

namespace TimePeriodTest
{
    [TestClass]
    public class TimePeriodUnitTest
    {
        [TestMethod]
        [DataRow(1, 2, 3)]
        public void TimePeriodConstructor3Param(int a, int b, int c)
        {
            TimePeriod time = new TimePeriod((byte)a, (byte)b, (byte)c);
            Assert.AreEqual($"{a:00}:{b:00}:{c:00}", time.ToString());
        }
        [TestMethod]
        [DataRow(1, 2)]
        public void TimePeriodConstructor2Param(int a, int b)
        {
            Time time = new Time((byte)a, (byte)b);
            Assert.AreEqual($"{a:00}:{b:00}:{0:00}", time.ToString());
        }
        [TestMethod]
        [DataRow(1)]
        public void TimePeriodConstructor1Param(int a)
        {
            Time time = new Time((byte)a);
            Assert.AreEqual($"{a:00}:{0:00}:{0:00}", time.ToString());
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        [DataRow(-111)]
        public void WrongTimePeriodConstructor1Param(int a)
        {
            TimePeriod time = new TimePeriod(a);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        [DataRow(-111)]
        public void WrongTimePeriodConstructor2Param(int a)
        {
            TimePeriod time = new TimePeriod(a, a);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        [DataRow(-111)]
        public void WrongTimePeriodConstructor3Param(int a)
        {
            TimePeriod time = new TimePeriod(a, a, a);
        }
        [TestMethod]
        [DataRow(1, 2, 3)]
        public void TimePeriodConstructorFromString(int a, int b, int c)
        {
            TimePeriod timeExpected = new TimePeriod(a, b, c);
            TimePeriod time = new TimePeriod(timeExpected.ToString());
            Assert.AreEqual(timeExpected.ToString(), time.ToString());
        }
        [TestMethod]
        [DataRow(1, 1, 1, 1, 1, 1, 0, 0, 0)]
        [DataRow(1, 1, 1, 15, 1, 1, 14, 0, 0)]
        [DataRow(1, 5, 1, 1, 35, 1, 0, 30, 0)]
        [DataRow(1, 1, 5, 1, 1, 35, 0, 0, 30)]
        public void TimePeriodFromTime(int a, int b, int c, int a1, int b1, int c1, int expectedA, int expectedB, int expectedC)
        {
            Time time = new Time((byte)a, (byte)b, (byte)c);
            Time time1 = new Time((byte)a1, (byte)b1, (byte)c1);
            TimePeriod timePeriod = new TimePeriod(time, time1);
            string actual = timePeriod.ToString();
            Assert.AreEqual($"{expectedA:00}:{expectedB:00}:{expectedC:00}", actual);
        }

        [TestMethod]
        [DataRow(1, 2, 3)]
        [DataRow(5, 22, 33)]
        [DataRow(11, 22, 33)]
        public void TimePeriodToString(int a, int b, int c)
        {
            TimePeriod time = new TimePeriod(a, b, c);
            string actual = time.ToString();
            Assert.AreEqual($"{a:00}:{b:00}:{c:00}", actual);
        }

        [TestMethod]
        [DataRow(1, 1, 1, 1, 1, 1, 2, 2, 2)]
        [DataRow(15, 1, 1, 15, 1, 1, 30, 2, 2)]
        [DataRow(1, 35, 1, 1, 35, 1, 3, 10, 2)]
        [DataRow(1, 1, 35, 1, 1, 35, 2, 3, 10)]
        public void TimePeriodPlus(int a, int b, int c, int a1, int b1, int c1, int expectedA, int expectedB, int expectedC)
        {
            TimePeriod time = new TimePeriod(a, b, c);
            TimePeriod time1 = new TimePeriod(a1, b1, c1);
            TimePeriod expected = new TimePeriod(expectedA, expectedB, expectedC);
            Assert.AreEqual(expected.ToString(), time.Plus(time1).ToString());
        }
        [TestMethod]
        [DataRow(1, 1, 1, 1, 1, 1, 0, 0, 0)]
        [DataRow(10, 1, 1, 1, 1, 1, 9, 0, 0)]
        [DataRow(1, 10, 1, 1, 1, 1, 0, 9, 0)]
        [DataRow(1, 1, 10, 1, 1, 1, 0, 0, 9)]
        public void TimePeriodSubstraction(int a, int b, int c, int a1, int b1, int c1, int expectedA, int expectedB, int expectedC)
        {
            TimePeriod time = new TimePeriod(a, b, c);
            TimePeriod time1 = new TimePeriod(a1, b1, c1);
            TimePeriod expected = new TimePeriod(expectedA, expectedB, expectedC);
            Assert.AreEqual(expected.ToString(), time.Substract(time1).ToString());
        }
        [TestMethod]
        [DataRow(1, 1, 1, 1, 1, 1, 2, 2, 2)]
        [DataRow(10, 1, 1, 1, 1, 1, 11, 2, 2)]
        [DataRow(1, 10, 1, 1, 1, 1, 2, 11, 2)]
        [DataRow(1, 1, 10, 1, 1, 1, 2, 2, 11)]
        public void StaticTimePeriodPlus(int a, int b, int c, int a1, int b1, int c1, int expectedA, int expectedB, int expectedC)
        {
            TimePeriod time = new TimePeriod(a, b, c);
            TimePeriod time1 = new TimePeriod(a1, b1, c1);
            TimePeriod expected = new TimePeriod(expectedA, expectedB, expectedC);
            Assert.AreEqual(expected.ToString(), TimePeriod.Plus(time, time1).ToString());
        }
        [TestMethod]
        [DataRow(1, 1, 1, 1, 1, 1, 0, 0, 0)]
        [DataRow(10, 1, 1, 1, 1, 1, 9, 0, 0)]
        [DataRow(1, 10, 1, 1, 1, 1, 0, 9, 0)]
        [DataRow(1, 1, 10, 1, 1, 1, 0, 0, 9)]
        public void StaticTimePeriodSubstraction(int a, int b, int c, int a1, int b1, int c1, int expectedA, int expectedB, int expectedC)
        {
            TimePeriod time = new TimePeriod(a, b, c);
            TimePeriod time1 = new TimePeriod(a1, b1, c1);
            TimePeriod expected = new TimePeriod(expectedA, expectedB, expectedC);
            Assert.AreEqual(expected.ToString(), TimePeriod.Substract(time, time1).ToString());
        }
        [TestMethod]
        [DataRow(1, 1, 1, 1, 1, 1, 2, 2, 2)]
        [DataRow(10, 1, 1, 1, 1, 1, 11, 2, 2)]
        [DataRow(1, 10, 1, 1, 1, 1, 2, 11, 2)]
        [DataRow(1, 1, 10, 1, 1, 1, 2, 2, 11)]
        public void OperatorTimePeriodPlus(int a, int b, int c, int a1, int b1, int c1, int expectedA, int expectedB, int expectedC)
        {
            TimePeriod time = new TimePeriod(a, b, c);
            TimePeriod time1 = new TimePeriod(a1, b1, c1);
            TimePeriod expected = new TimePeriod(expectedA, expectedB, expectedC);
            Assert.AreEqual(expected.ToString(), (time + time1).ToString());
        }
        [TestMethod]
        [DataRow(1, 1, 1, 1, 1, 1, 0, 0, 0)]
        [DataRow(10, 1, 1, 1, 1, 1, 9, 0, 0)]
        [DataRow(1, 10, 1, 1, 1, 1, 0, 9, 0)]
        [DataRow(1, 1, 10, 1, 1, 1, 0, 0, 9)]
        public void OperatorTimePeriodSubstraction(int a, int b, int c, int a1, int b1, int c1, int expectedA, int expectedB, int expectedC)
        {
            TimePeriod time = new TimePeriod(a, b, c);
            TimePeriod time1 = new TimePeriod(a1, b1, c1);
            TimePeriod expected = new TimePeriod(expectedA, expectedB, expectedC);
            Assert.AreEqual(expected.ToString(), (time - time1).ToString());
        }
        [TestMethod]
        [DataRow(15, 1, 1, 18, 1, 1)]
        [DataRow(1, 35, 1, 1, 45, 1)]
        [DataRow(1, 1, 35, 1, 1, 45)]
        public void OperatorTimePeriodGreater(int a, int b, int c, int a1, int b1, int c1)
        {
            TimePeriod time = new TimePeriod(a, b, c);
            TimePeriod time1 = new TimePeriod(a1, b1, c1);
            Assert.IsTrue(time1 > time);
        }
        [TestMethod]
        [DataRow(15, 1, 1, 17, 1, 1)]
        [DataRow(1, 35, 1, 1, 45, 1)]
        [DataRow(1, 1, 35, 1, 1, 45)]
        public void OperatorTimePeriodLesser(int a, int b, int c, int a1, int b1, int c1)
        {
            TimePeriod time = new TimePeriod(a, b, c);
            TimePeriod time1 = new TimePeriod(a1, b1, c1);
            Assert.IsTrue(time < time1);
        }
        [TestMethod]
        [DataRow(1, 1, 1, 1, 1, 1)]
        [DataRow(15, 1, 1, 18, 1, 1)]
        [DataRow(1, 35, 1, 1, 45, 1)]
        [DataRow(1, 1, 35, 1, 1, 45)]
        public void OperatorTimePeriodGreaterOrEqual(int a, int b, int c, int a1, int b1, int c1)
        {
            TimePeriod time = new TimePeriod(a, b, c);
            TimePeriod time1 = new TimePeriod(a1, b1, c1);
            Assert.IsTrue(time1 >= time);
        }
        [TestMethod]
        [DataRow(1, 1, 1, 1, 1, 1)]
        [DataRow(15, 1, 1, 17, 1, 1)]
        [DataRow(1, 35, 1, 1, 45, 1)]
        [DataRow(1, 1, 35, 1, 1, 45)]
        public void OperatorTimePeriodLesserOrEqual(int a, int b, int c, int a1, int b1, int c1)
        {
            TimePeriod time = new TimePeriod(a, b, c);
            TimePeriod time1 = new TimePeriod(a1, b1, c1);
            Assert.IsTrue(time <= time1);
        }
        [TestMethod]
        [DataRow(1, 1, 1, 1, 1, 1)]
        [DataRow(15, 1, 1, 15, 1, 1)]
        [DataRow(1, 35, 1, 1, 35, 1)]
        [DataRow(1, 1, 35, 1, 1, 35)]
        public void OperatorTimeEqual(int a, int b, int c, int a1, int b1, int c1)
        {
            TimePeriod time = new TimePeriod(a, b, c);
            TimePeriod time1 = new TimePeriod(a1, b1, c1);
            Assert.IsTrue(time1 == time);
        }
        [TestMethod]
        [DataRow(1, 1, 1, 1, 2, 1)]
        [DataRow(15, 1, 1, 17, 1, 1)]
        [DataRow(1, 35, 1, 1, 45, 1)]
        [DataRow(1, 1, 35, 1, 1, 45)]
        public void OperatorTimePeriodNotEqual(int a, int b, int c, int a1, int b1, int c1)
        {
            TimePeriod time = new TimePeriod(a, b, c);
            TimePeriod time1 = new TimePeriod(a1, b1, c1);
            Assert.IsTrue(time != time1);
        }
    }
}
