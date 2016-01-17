using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using MathInterpreter;
using MathInterpreter.Arithmetic;

namespace MathInterpreterTests
{
    [TestClass]
    public class EvaluationArithmeticTests
    {
        [TestMethod]
        public void EvaluateSimpleAdditionValueResult()
        {
            var addition = new Addition(new Number("2.6"), new Number("4.8"));
            Assert.AreEqual("7.4", ((Number)addition.Evaluate()).ToString());
        }

        [TestMethod]
        public void EvaluateSimpleAdditionSymbolicResult()
        {
            var addition = new Addition(new Number("a"), new Number("b"));
            Assert.AreEqual("a+b", ((Number)addition.Evaluate()).ToString());
        }

        [TestMethod]
        public void EvaluateSimpleAdditionSymbolicAndValueResult()
        {
            var addition = new Addition(new Number("a"), new Number("4.6"));
            Assert.AreEqual("a+4.6", ((Number)addition.Evaluate()).ToString());
        }

        [TestMethod]
        public void EvaluateSimpleSubtractionValueResult()
        {
            var operat = new Subtraction(new Number("2.6"), new Number("4.8"));
            Assert.AreEqual("-2.2", ((Number)operat.Evaluate()).ToString());
        }

        [TestMethod]
        public void EvaluateSimpleSubtractionSymbolicResult()
        {
            var operat = new Subtraction(new Number("a"), new Number("b"));
            Assert.AreEqual("a-b", ((Number)operat.Evaluate()).ToString());
        }

        [TestMethod]
        public void EvaluateSimpleSubtractionSymbolicAndValueResult()
        {
            var operat = new Subtraction(new Number("a"), new Number("4.6"));
            Assert.AreEqual("a-4.6", ((Number)operat.Evaluate()).ToString());
        }

        [TestMethod]
        public void EvaluateSimpleMultiplicationValueResult()
        {
            var addition = new Multiplication(new Number("2.6"), new Number("4.8"));
            Assert.AreEqual("12.48", ((Number)addition.Evaluate()).ToString());
        }

        [TestMethod]
        public void EvaluateSimpleMultiplicationSymbolicResult()
        {
            var addition = new Multiplication(new Number("a"), new Number("b"));
            Assert.AreEqual("a*b", ((Number)addition.Evaluate()).ToString());
        }

        [TestMethod]
        public void EvaluateSimpleMultiplicationSymbolicAndValueResult()
        {
            var addition = new Multiplication(new Number("a"), new Number("4.6"));
            Assert.AreEqual("a*4.6", ((Number)addition.Evaluate()).ToString());
        }

        [TestMethod]
        public void EvaluateSimpleDivisionValueResult()
        {
            var addition = new Division(new Number("2.6"), new Number("4.8"));
            Assert.AreEqual("0.541666666666667", ((Number)addition.Evaluate()).ToString());
        }

        [TestMethod]
        public void EvaluateSimpleDivisionSymbolicResult()
        {
            var addition = new Division(new Number("a"), new Number("b"));
            Assert.AreEqual("a/b", ((Number)addition.Evaluate()).ToString());
        }

        [TestMethod]
        public void EvaluateSimpleDivisionSymbolicAndValueResult()
        {
            var addition = new Division(new Number("a"), new Number("4.6"));
            Assert.AreEqual("a/4.6", ((Number)addition.Evaluate()).ToString());
        }

        [TestMethod]
        public void EvaluateNestedExpressionSymbolic()
        {
            var symbol = new Addition(new Multiplication(new Number("a"), new Number("b")), new Number("c"));
            Assert.AreEqual("a*b+c", ((Number)symbol.Evaluate()).ToString());

            symbol = new Addition(new Multiplication(new Number("4"), new Number("5.6")), new Number("c"));
            Assert.AreEqual("22.4+c", ((Number)symbol.Evaluate()).ToString());

            symbol = new Addition(new Multiplication(new Number("a"), new Number("b")), new Number("c"));
            Assert.AreEqual("a*b+c", ((Number)symbol.Evaluate()).ToString());

            symbol = new Addition(new Multiplication(new Number("a"), new Number("5")), new Number("c"));
            Assert.AreEqual("a*5+c", ((Number)symbol.Evaluate()).ToString());

            symbol = new Addition(new Multiplication(new Number("a"), new Number("5")), new Number("c"));
            Assert.AreEqual("a*5+c", ((Number)symbol.Evaluate()).ToString());

            var symbolNestedFunction = new Subtraction(new Addition(new Division(new Number("a"), new Number("b")), 
                                    new Multiplication(new Number("c"), new Number("d"))),
               new Addition(new Number("e"), new Number("f")));
            Assert.AreEqual("a/b+c*d-e+f", ((Number)symbolNestedFunction.Evaluate()).ToString());
        }
    }
}
