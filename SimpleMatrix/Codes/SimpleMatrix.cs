using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4 {
    using Element = System.Int32;
    /// <summary>
    /// 简单的矩阵类，该矩阵被初始化后行列值将无法变更
    /// </summary>
    public class SimpleMatrix {
        public readonly Element[,] Matrix;
        public int Row {
            get {
                return this.Matrix.GetLength(0);
            }
        }
        public int Column {
            get {
                return this.Matrix.GetLength(1);
            }
        }
        public Element this[int row, int col] {
            get {
                return this.Matrix[row, col];
            }
            set {
                this.Matrix[row, col] = value;
            }
        }
        /// <summary>
        /// 默认构造函数，生成一个1x1的矩阵，内部用
        /// </summary>
        private SimpleMatrix() {
            this.Matrix = new Element[1, 1];
        }
        /// <summary>
        /// 构造函数，传入矩阵的行，列数以及元素初始值
        /// </summary>
        /// <param name="rowSize">行数</param>
        /// <param name="colSize">列数</param>
        /// <param name="initValue">元素初始值</param>
        public SimpleMatrix(int rowSize, int colSize, Element initValue = 0) {
            this.Matrix = new Element[rowSize, colSize];
            for (int row = 0; row < rowSize; row++) {
                for (int col = 0; col < colSize; col++) {
                    this.Matrix[row, col] = initValue;
                }
            }
        }
        /// <summary>
        /// 构造函数，传入一个二维数组，将内部矩阵设置为该二位数值，只是深度复制
        /// </summary>
        /// <param name="matrix"></param>
        public SimpleMatrix(Element[,] matrix) : this() {
            this.Matrix = new Element[matrix.GetLength(0), matrix.GetLength(1)];
            for (int row = 0; row < this.Row; row++) {
                for (int col = 0; col < this.Column; col++) {
                    this.Matrix[row, col] = matrix[row, col];
                }
            }
        }
        /// <summary>
        /// 转置矩阵
        /// </summary>
        /// <returns></returns>
        public SimpleMatrix Transply() {
            SimpleMatrix tMatrix = new SimpleMatrix(this.Column, this.Row);
            for (int row = 0; row < this.Column; row++) {
                for (int col = 0; col < this.Row; col++) {
                    tMatrix[row, col] = this.Matrix[col, row];
                }
            }
            return tMatrix;
        }
        /// <summary>
        /// 行列式值
        /// </summary>
        /// <returns></returns>
        public Element Determinant() {
            return 1;
        }
        /// <summary>
        /// 取反
        /// </summary>
        /// <param name="matrix"></param>
        /// <returns></returns>
        public static SimpleMatrix operator -(SimpleMatrix matrix) {
            SimpleMatrix nMatrix = new SimpleMatrix(matrix.Row, matrix.Column);
            for (int row = 0; row < matrix.Row; row++) {
                for (int col = 0; col < matrix.Column; col++) {
                    nMatrix[row, col] = -matrix[row, col];
                }
            }
            return nMatrix;
        }
        /// <summary>
        /// 矩阵加法
        /// </summary>
        /// <param name="firstMatrix">矩阵1</param>
        /// <param name="secondMatrix">矩阵2</param>
        /// <returns></returns>
        public static SimpleMatrix operator +(SimpleMatrix firstMatrix, SimpleMatrix secondMatrix) {
            if (!(firstMatrix.Row == secondMatrix.Row && firstMatrix.Column == secondMatrix.Column)) {
                throw new ArgumentException("相加的两个矩阵行数列数应相同");
            }
            int rowSize = firstMatrix.Row;
            int columnSize = firstMatrix.Column;
            SimpleMatrix addedMatrix = new SimpleMatrix(rowSize, columnSize);
            for (int row = 0; row < rowSize; row++) {
                for (int col = 0; col < columnSize; col++) {
                    addedMatrix[row, col] = firstMatrix[row, col] + secondMatrix[row, col];
                }
            }
            return addedMatrix;

        }
        /// <summary>
        /// 矩阵减法
        /// </summary>
        /// <param name="firstMatrix">矩阵1</param>
        /// <param name="secondMatrix">矩阵2</param>
        /// <returns></returns>
        public static SimpleMatrix operator -(SimpleMatrix firstMatrix, SimpleMatrix secondMatrix) {
            SimpleMatrix nMatrix = -secondMatrix;
            return firstMatrix + nMatrix;
        }
        /// <summary>
        /// 矩阵乘法
        /// </summary>
        /// <param name="firstMatrix">矩阵1</param>
        /// <param name="secondMatrix">矩阵2</param>
        /// <returns></returns>
        public static SimpleMatrix operator *(SimpleMatrix firstMatrix, SimpleMatrix secondMatrix) {
            if (!(firstMatrix.Column == secondMatrix.Row)) {
                throw new ArgumentException("进行乘法运算的矩阵的第一个矩阵的列应等于第二个矩阵的行数");
            }
            int rowSize = firstMatrix.Row;
            int columnSize = secondMatrix.Column;
            SimpleMatrix mMatrix = new SimpleMatrix(rowSize, columnSize);
            for (int i3 = 0; i3 < firstMatrix.Row; i3++) {
                for (int i2 = 0; i2 < secondMatrix.Column; i2++) {
                    //行列对乘
                    Element sum = 0;
                    for (int i = 0; i < firstMatrix.Column; i++) {
                        sum += firstMatrix[i3, i] * secondMatrix[i, i2];
                    }
                    mMatrix[i3, i2] = sum;
                }
            }
            return mMatrix;
        }
        /// <summary>
        /// 数乘矩阵
        /// </summary>
        /// <param name="number">倍数</param>
        /// <param name="matrix">矩阵</param>
        /// <returns></returns>
        public static SimpleMatrix operator *(Element number, SimpleMatrix matrix) {
            SimpleMatrix mMatrix = new SimpleMatrix(matrix.Row, matrix.Column);
            for (int row = 0; row < matrix.Row; row++) {
                for (int col = 0; col < matrix.Column; col++) {
                    mMatrix[row, col] = number * matrix[row, col];
                }
            }
            return mMatrix;
        }
        /// <summary>
        /// 指数运算，暂只支持整数
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="pow"></param>
        /// <returns></returns>
        public static SimpleMatrix Pow(SimpleMatrix matrix, int pow) {
            SimpleMatrix nMatrix = new SimpleMatrix(matrix.Matrix);
            for (int i = 0; i < pow; i++) {
                nMatrix *= nMatrix;
            }
            return nMatrix;
        }

        #region 矩阵变换
        /// <summary>
        /// 行变换，改变换不会影响原矩阵，而是返回一个变换后的新矩阵
        /// </summary>
        /// <param name="firstRowIndex">交换的第一行</param>
        /// <param name="secondeRowIndex">交换的第二行</param>
        /// <returns></returns>
        public SimpleMatrix RowSwitch(int firstRowIndex, int secondeRowIndex) {
            SimpleMatrix nMatrix = new SimpleMatrix(this.Matrix);
            for (int col = 0; col < this.Column; col++) {
                Element t = nMatrix[firstRowIndex, col];
                nMatrix[firstRowIndex, col] = nMatrix[secondeRowIndex, col];
                nMatrix[secondeRowIndex, col] = t;
            }
            return nMatrix;
        }
        /// <summary>
        ///  倍法变换，改变换不会影响原矩阵，而是返回一个变换后的新矩阵
        /// </summary>
        /// <param name="rowIndex">被乘的行</param>
        /// <param name="times">放大倍数</param>
        /// <returns></returns>
        public SimpleMatrix RowMulitySwitch(int rowIndex, Element times) {
            SimpleMatrix nMatrix = new SimpleMatrix(this.Matrix);
            for (int col = 0; col < this.Column; col++) {
                nMatrix[rowIndex, col] *= times;
            }
            return nMatrix;
        }
        /// <summary>
        /// 消法变换，改变换不会影响原矩阵，而是返回一个变换后的新矩阵
        /// </summary>
        /// <param name="targetRowIndex">目标行</param>
        /// <param name="timesRowIndex">加行</param>
        /// <param name="times">加行所乘倍数·</param>
        /// <returns></returns>
        public SimpleMatrix DeRowSwitch(int targetRowIndex, int timesRowIndex, Element times) {
            SimpleMatrix nMatrix = new SimpleMatrix(this.Matrix);
            for (int col = 0; col < this.Column; col++) {
                nMatrix[targetRowIndex, col] += times * nMatrix[timesRowIndex, col];
            }
            return nMatrix;
        }
        #endregion
        /// <summary>
        /// 字符串格式化
        /// </summary>
        /// <returns></returns>
        public override string ToString() {
            StringBuilder matrixString = new StringBuilder();
            for (int row = 0; row < this.Row; row++) {
                for (int col = 0; col < this.Column; col++) {
                    matrixString.Append($"{this.Matrix[row, col]} ");
                }
                matrixString.Append("\n");
            }
            return matrixString.ToString();
        }

    }
}
