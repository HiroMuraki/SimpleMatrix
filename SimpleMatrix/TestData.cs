using System;
using System.Text;
using static System.Console;


namespace ConsoleApp4 {
    using Element = System.Int32;
    public class TestData {
        public static void RationalNumberTest() {
            RationalNumber firstNumber = new RationalNumber("2/5");
            RationalNumber secondNumber = new RationalNumber("2/5");
            WriteLine($"Number 1: {firstNumber}, Number 2: {secondNumber}");
            WriteLine("倒数");
            WriteLine(firstNumber.GetReciprocal());
            WriteLine("取反");
            WriteLine(-firstNumber);
            WriteLine("加法");
            WriteLine(firstNumber + secondNumber);
            WriteLine("减法");
            WriteLine(firstNumber - secondNumber);
            WriteLine("乘法");
            WriteLine(firstNumber * secondNumber);
            WriteLine("除法");
            WriteLine(firstNumber / secondNumber);
            WriteLine("----------------");
            WriteLine($"大于: {firstNumber > secondNumber}");
            WriteLine($"小于: {firstNumber < secondNumber}");
            WriteLine($"小于等于: {firstNumber <= secondNumber}");
            WriteLine($"大于等于: {firstNumber >= secondNumber}");
            WriteLine($"等于: {firstNumber == secondNumber}");
            WriteLine($"不等于: {firstNumber != secondNumber}");
            WriteLine($"Number 1: {firstNumber}, Number 2: {secondNumber}");
        }
        public static void MatrixTest() {
            int[,] matrixA = new int[2, 3] {
                {1,2,3},
                {5,3,2},
            };
            int[,] matrixB = new int[2, 3] {
                {3,1,1},
                {2,5,3},
            };
            SimpleMatrix firstMatrix = new SimpleMatrix(matrixA);
            SimpleMatrix secondMatrix = new SimpleMatrix(matrixB);
            try {
                WriteLine("Matrix A: ");
                WriteLine(firstMatrix);
                WriteLine("Matrix B: ");
                WriteLine(secondMatrix);
                WriteLine("----------");
                WriteLine($"mA row={firstMatrix.Row}, col={firstMatrix.Column}");
                WriteLine($"mB row={secondMatrix.Row}, col={secondMatrix.Column}");
                WriteLine("转置");
                WriteLine(firstMatrix.Transply());
                WriteLine("加法");
                WriteLine(firstMatrix + secondMatrix);
                WriteLine("减法");
                WriteLine(firstMatrix - secondMatrix);
                WriteLine("取负");
                WriteLine(-firstMatrix);
                WriteLine("数乘:");
                WriteLine(2 * firstMatrix);
                WriteLine("乘法");
                WriteLine(firstMatrix * secondMatrix);
            }
            catch (ArgumentException e) {
                WriteLine(e.Message);
            }
            finally {
                WriteLine("----------");
                WriteLine("行变换，交换第一行和第二行");
                WriteLine(firstMatrix.RowSwitch(0, 1));
                WriteLine("行变换，第一行乘以-2");
                WriteLine(firstMatrix.RowMulitySwitch(0, -2));
                WriteLine("倍法变化，第3行+第2行*-1");
                WriteLine(firstMatrix.DeRowSwitch(1, 0, -1));
            }

            SimpleMatrix nMatrix = new SimpleMatrix(3, 3, 2);
            WriteLine(SimpleMatrix.Pow(nMatrix, 5));
        }
        public static void DeterminantTest() {
            int[,] t = new int[5, 5] {
                {2,3,5,2,5 },
                {3,2,2,2,2 },
                {5,2,1,3,1 },
                {1,2,3,2,1 },
                {5,2,7,1,2, }
            };
            Determinant determinant = new Determinant(t);
            Determinant d = determinant;
            WriteLine(d);
            WriteLine(d.GetValue());

        }
        public static void GetString(Element[,] matrix) {
            StringBuilder matrixString = new StringBuilder();
            for (int row = 0; row < matrix.GetLength(0); row++) {
                for (int col = 0; col < matrix.GetLength(1); col++) {
                    matrixString.Append($"{matrix[row, col]} ");
                }
                matrixString.Append("\n");
            }
            Console.WriteLine(matrixString.ToString());
        }
    }
}
