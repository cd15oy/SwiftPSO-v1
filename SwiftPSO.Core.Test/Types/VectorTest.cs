/*
   ____         _  ___ __   ___   ____ ____ 
  / __/_    __ (_)/ _// /_ / _ \ / __// __ \
 _\ \ | |/|/ // // _// __// ___/_\ \ / /_/ /
/___/ |__,__//_//_/  \__//_/   /___/ \____/    

 */
using NUnit.Framework;
using SwiftPSO.Core.Types;

namespace SwiftPSO.Core.Test.Types
{
    [TestFixture]
    public class VectorTest
    {
        [Test]
        public void GetDoubleValue()
        {
            Real[] values = new Real[3];
            values[0] = new Real(1);
            values[1] = new Real(2);
            values[2] = new Real(3);

            Vector vector = new Vector(new double[] { 1, 2, 3 });

            Assert.AreEqual(1, vector[0]);
            Assert.AreEqual(2, vector[1]);
            Assert.AreEqual(3, vector[2]);
        }

        [Test]
        public void Add()
        {
            const int size = 10;
            double[] values1 = new double[size];
            double[] values2 = new double[size];
            for (int i = 0; i < size; i++)
            {
                values1[i] = i;
                values2[i] = 2 * i;
            }

            Vector op1 = new Vector(values1);
            Vector op2 = new Vector(values2);
            Vector result = op1 + op2;

            for (int i = 0; i < size; i++)
            {
                Assert.AreEqual(i + (2 * i), result[i]);
                //ensure originals do not change
                Assert.AreEqual(i, op1[i]);
                Assert.AreEqual(2 * i, op2[i]);
            }
        }

        [Test]
        public void Subtract()
        {
            const int size = 10;
            double[] values1 = new double[size];
            double[] values2 = new double[size];
            for (int i = 0; i < size; i++)
            {
                values1[i] = i;
                values2[i] = 2 * i;
            }

            Vector op1 = new Vector(values1);
            Vector op2 = new Vector(values2);
            Vector result = op1 - op2;

            for (int i = 0; i < size; i++)
            {
                Assert.AreEqual(i - (2 * i), result[i]);
                //ensure originals do not change
                Assert.AreEqual(i, op1[i]);
                Assert.AreEqual(2 * i, op2[i]);
            }
        }

        [Test]
        public void VectorMultiplication()
        {
            const int size = 10;
            double[] values1 = new double[size];
            double[] values2 = new double[size];
            for (int i = 0; i < size; i++)
            {
                values1[i] = i;
                values2[i] = 2 * i;
            }

            Vector op1 = new Vector(values1);
            Vector op2 = new Vector(values2);
            Vector result = op1 * op2;

            for (int i = 0; i < size; i++)
            {
                Assert.AreEqual(i * (2 * i), result[i]);
                //ensure originals do not change
                Assert.AreEqual(i, op1[i]);
                Assert.AreEqual(2 * i, op2[i]);
            }
        }

        [Test]
        public void ScalarMultiplication()
        {
            const int size = 10;
            double[] values = new double[size];
            for (int i = 0; i < size; i++)
            {
                values[i] = i;
            }

            Vector vector = new Vector(values);
            Vector result = vector * 3;

            for (int i = 0; i < size; i++)
            {
                Assert.AreEqual(i, vector[i]);     //ensure original does not change
                Assert.AreEqual(i * 3, result[i]);
            }

        }
    }
}